using System.Collections.Generic;
using CookBook.Common.Models;
using CookBook.Business.Providers;
using CookBook.DAL.Repositories;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CookBook.Business.Tests
{
    [TestClass]
    public class CategoryProviderTests
    {
        [TestMethod]
        public void GetCategoryById_InvalidId_Null()
        {
            Mock<ICategoryRepository> mockCategory = new Mock<ICategoryRepository>();
            mockCategory.Setup(cat => cat.GetCategoryById(It.IsAny<int>())).Returns(new Category());
            Mock<ICacheProvider> mockCache = new Mock<ICacheProvider>();

            CategoryProvider categoryProvider = new CategoryProvider(mockCategory.Object, mockCache.Object);
            Category category = categoryProvider.GetCategoryById(0);

            Assert.IsNull(category);
        }

        [TestMethod]
        public void GetCategoryById_ValidId_NotNull()
        {
            Mock<ICategoryRepository> mockCategory = new Mock<ICategoryRepository>();
            mockCategory.Setup(cat => cat.GetCategoryById(It.IsAny<int>())).Returns(new Category());
            Mock<ICacheProvider> mockCache = new Mock<ICacheProvider>();

            CategoryProvider categoryProvider = new CategoryProvider(mockCategory.Object, mockCache.Object);
            Category category = categoryProvider.GetCategoryById(1);

            Assert.IsNotNull(category);
        }

        [TestMethod]
        public void GetChildCategoriesById_InvalidId_Null()
        {
            Mock<ICategoryRepository> mockCategory = new Mock<ICategoryRepository>();
            mockCategory.Setup(cat => cat.GetChildCategoriesById(It.IsAny<int>())).Returns(new List<Category>());
            Mock<ICacheProvider> mockCache = new Mock<ICacheProvider>();

            CategoryProvider categoryProvider = new CategoryProvider(mockCategory.Object, mockCache.Object);
            List<Category> categories = categoryProvider.GetChildCategoriesById(0);
            
            Assert.IsNull(categories);
        }

        [TestMethod]
        public void GetChildCategoriesById_ValidId_NotNull()
        {
            Mock<ICategoryRepository> mockCategory = new Mock<ICategoryRepository>();
            mockCategory.Setup(cat => cat.GetChildCategoriesById(It.IsAny<int>())).Returns(new List<Category>());
            Mock<ICacheProvider> mockCache = new Mock<ICacheProvider>();

            CategoryProvider categoryProvider = new CategoryProvider(mockCategory.Object, mockCache.Object);
            List<Category> categories = categoryProvider.GetChildCategoriesById(1);

            Assert.IsNotNull(categories);
        }                
    }
}
