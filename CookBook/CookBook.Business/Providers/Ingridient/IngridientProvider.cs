using System.Linq;
using System.Collections.Generic;
using CookBook.Common.Models;
using CookBook.DAL.Repositories;
using CookBook.Common.Enums;

namespace CookBook.Business.Providers
{
    public class IngridientProvider : IIngridientProvider
    {
        private readonly IIngridientRepository _ingridientrepository;
        
        public IngridientProvider(IIngridientRepository ingridientrepository)
        {
            if (ingridientrepository != null)
            {
                _ingridientrepository = ingridientrepository;
            }
            else
            {
                RepositoriesFactory factory = new RepositoriesFactory();
                _ingridientrepository = factory.GetIngridientRepository();
            }
        }

        public void InsertInrgridient(Ingridient ingridient)
        {
            if (ingridient != null)
            {
                _ingridientrepository.InsertInrgridient(ingridient);
            }            
        }

        public void DeleteIngridient(int ingridientId)
        {
            if (ingridientId > 0)
            {
                _ingridientrepository.DeleteIngridient(ingridientId);
            }            
        }

        public List<Ingridient> GetIngridients()
        {
            return _ingridientrepository.GetIngridients();
        }

        public void UpdateIngridient(Ingridient ingridient)
        {
            if (ingridient != null)
            {
                _ingridientrepository.UpdateIngridient(ingridient);
            }            
        }

        public Ingridient GetIngridientById(int ingridientId)
        {
            if (ingridientId > 0)
            {
                return _ingridientrepository.GetIngridientById(ingridientId);
            }
            else
            {
                return null;
            }
            
        }

        public UniqueValidation IsUnique(string ingridientName)
        {
            if(string.IsNullOrEmpty(ingridientName))
            {
                return UniqueValidation.Error;
            }
            else
            {
                List<Ingridient> ingridients = GetIngridients();
                Ingridient checkedIngridient = ingridients.FirstOrDefault(c => c.Name == ingridientName);
                if (checkedIngridient == null)
                {
                    return UniqueValidation.Unique;
                }
                else
                {
                    return UniqueValidation.Dublicate;
                }
            }
        }
    }
}
