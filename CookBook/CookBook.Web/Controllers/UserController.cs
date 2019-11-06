using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CookBook.Business.Providers;
using CookBook.Common.Models;
using CookBook.Common.Enums;
using CookBook.Web.Models;
using CookBook.Web.AuthAttributes;

namespace CookBook.Web.Controllers
{
    [Admin]
    public class UserController : Controller
    {
        private readonly IUserProvider _userProvider;

        public UserController(IUserProvider userProvider)
        {
            if (userProvider != null)
            {
                _userProvider = userProvider;
            }
            else
            {
                ProvidersFactory factory = new ProvidersFactory();
                _userProvider = factory.GetUserProvider();
            }            
        }

        public ActionResult ShowUsers()
        {
            List<User> users = _userProvider.GetUsers();
            IEnumerable<ShowUsersViewModel> model = ParseUserViews(users);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ShowUsers", model);
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]        
        public ActionResult AddOrEditUser(int userId)
        {
            User user = _userProvider.GetUserById(userId);
            EditUserViewModel model = ParseUser(user);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_AddOrEditUser", model);
            }
            else
            {
                return View(model);
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddOrEditUser(EditUserViewModel editUserViewModel)
        {            
            editUserViewModel.LoginUniqueness = _userProvider.IsUniqueLogin(editUserViewModel.UserId, editUserViewModel.Login);
            editUserViewModel.EmailUniqueness = _userProvider.IsUniqueEmail(editUserViewModel.UserId, editUserViewModel.Email);

            if (editUserViewModel.LoginUniqueness == UniqueValidation.Dublicate)
            {
                ModelState.AddModelError("Login", "This login is already exists");
            }
            if (editUserViewModel.LoginUniqueness == UniqueValidation.Error)
            {
                ModelState.AddModelError("Login", "Login is required");
            }

            if (editUserViewModel.EmailUniqueness == UniqueValidation.Dublicate)
            {
                ModelState.AddModelError("Email", "This email is already exists");
            }
            if (editUserViewModel.EmailUniqueness == UniqueValidation.Error)
            {
                ModelState.AddModelError("Email", "Email is required");
            }

            if (editUserViewModel.SelectedUserRoles==null)
            {
                editUserViewModel.SelectedUserRoles = new List<int>();
                UserRole userRole = _userProvider.GetUserRoles().FirstOrDefault(u => u.Name == "Guest");                
                editUserViewModel.SelectedUserRoles.Add(userRole.UserRoleId);
            }
            else
            {
                UserRole userRole = _userProvider.GetUserRoles().FirstOrDefault(u => u.Name == "Guest");
                editUserViewModel.SelectedUserRoles.Add(userRole.UserRoleId);
                UserRole userRoleAdmin = _userProvider.GetUserRoles().FirstOrDefault(u => u.Name == "Admin");
                UserRole userRoleEditor = _userProvider.GetUserRoles().FirstOrDefault(u => u.Name == "Editor");
                if (editUserViewModel.SelectedUserRoles.FirstOrDefault(i => i == userRoleAdmin.UserRoleId) != 0)
                {
                    editUserViewModel.SelectedUserRoles.Add(userRoleEditor.UserRoleId);
                }
            }

            if (ModelState.IsValid)
            {
                User user = ParseUser(editUserViewModel);
                if (editUserViewModel.UserId == 0)
                {                    
                    _userProvider.InsertUser(user, editUserViewModel.SelectedUserRoles);
                }
                else
                {                    
                    _userProvider.UpdateUser(user, editUserViewModel.SelectedUserRoles);
                }

                return RedirectToAction("ShowUsers");
            }
            else
            {
                User user = ParseUser(editUserViewModel);
                editUserViewModel = ParseUser(user);
                return View("AddOrEditUser", editUserViewModel);
            }
        }
                
        [HttpGet]
        public ActionResult DeleteUser(int userId)
        {
            _userProvider.DeleteUser(userId);

            return RedirectToAction("ShowUsers");
        }

        #region Helpers

        private IEnumerable<ShowUsersViewModel> ParseUserViews(List<User> users)
        {
            List<ShowUsersViewModel> returnedModel = new List<ShowUsersViewModel>();
            foreach(User item in users)
            {
                ShowUsersViewModel itemViewModel = new ShowUsersViewModel();
                itemViewModel.UserId = item.UserId;
                itemViewModel.Login = item.Login;
                itemViewModel.Password = item.Password;
                itemViewModel.Email = item.Email;
                itemViewModel.UserRoles = "";
                List<UserRole> userRoles = _userProvider.GetUserRolesByUserId(itemViewModel.UserId);
                foreach(UserRole role in userRoles)
                {
                    itemViewModel.UserRoles = itemViewModel.UserRoles  + role.Name + ", ";
                }
                itemViewModel.UserRoles = itemViewModel.UserRoles.TrimEnd();
                int ind = itemViewModel.UserRoles.LastIndexOf(",");
                if(ind==itemViewModel.UserRoles.Length-1)
                {
                    itemViewModel.UserRoles = itemViewModel.UserRoles.Remove(itemViewModel.UserRoles.Length - 1, 1);
                }
                returnedModel.Add(itemViewModel);
            }
            return returnedModel;
        }

        private User ParseUser(EditUserViewModel editUserViewModel)
        {
            User user = new User();
            user.UserId = editUserViewModel.UserId;
            user.Login = editUserViewModel.Login;
            user.Password = editUserViewModel.Password;
            user.Email = editUserViewModel.Email;            
            return user;
        }

        private EditUserViewModel ParseUser(User user)
        {
            EditUserViewModel editUserViewModel = new EditUserViewModel();
            IEnumerable<UserRole> userRoles = _userProvider.GetUserRoles();            
            editUserViewModel.EmailUniqueness = UniqueValidation.Unique;
            editUserViewModel.LoginUniqueness = UniqueValidation.Unique;
            if (user!=null)
            {
                List<UserRole> userRolesForUser = _userProvider.GetUserRolesByUserId(user.UserId);
                editUserViewModel.UserId = user.UserId;
                editUserViewModel.Login = user.Login;
                editUserViewModel.Password = user.Password;
                editUserViewModel.Email = user.Email;                
                List<SelectListItem> selectListItems = new List<SelectListItem>();
                foreach(UserRole item in userRoles)
                {                    
                    SelectListItem listItem = new SelectListItem();
                    listItem.Value = item.UserRoleId.ToString();
                    listItem.Text = item.Name;
                    if(item.Name=="Guest")
                    {
                        listItem.Selected = true;
                        listItem.Disabled = true;
                    }
                    else
                    {
                        listItem.Selected = userRolesForUser.FirstOrDefault(u => u.UserRoleId == item.UserRoleId && u.Name == item.Name) != null;
                    }
                    selectListItems.Add(listItem);
                }
                editUserViewModel.UserRoles = selectListItems;
            }
            else
            {
                List<SelectListItem> selectListItems = new List<SelectListItem>();
                foreach (UserRole item in userRoles)
                {
                    SelectListItem listItem = new SelectListItem();
                    listItem.Value = item.UserRoleId.ToString();
                    listItem.Text = item.Name;
                    if (item.Name == "Guest")
                    {
                        listItem.Selected = true;
                        listItem.Disabled = true;
                    }
                    selectListItems.Add(listItem);
                }
                editUserViewModel.UserRoles = selectListItems;
            }

            return editUserViewModel;
        }

        #endregion
    }
}