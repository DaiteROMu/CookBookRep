using CookBook.Common.Models;
using CookBook.Business.Providers;
using CookBook.DAL.Repositories;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CookBook.Business.Tests
{
    [TestClass]
    public class IngridientProviderTests
    {
        [TestMethod]
        public void GetIngridientById_InvalidId_Null()
        {
            Mock<IIngridientRepository> mock = new Mock<IIngridientRepository>();
            mock.Setup(i => i.GetIngridientById(It.IsAny<int>())).Returns(new Ingridient());

            IngridientProvider ingridientProvider = new IngridientProvider(mock.Object);
            Ingridient ingridient = ingridientProvider.GetIngridientById(0);

            Assert.IsNull(ingridient);
        }

        [TestMethod]
        public void GetIngridientById_ValidId_NotNull()
        {
            Mock<IIngridientRepository> mock = new Mock<IIngridientRepository>();
            mock.Setup(i => i.GetIngridientById(It.IsAny<int>())).Returns(new Ingridient());

            IngridientProvider ingridientProvider = new IngridientProvider(mock.Object);
            Ingridient ingridient = ingridientProvider.GetIngridientById(1);

            Assert.IsNotNull(ingridient);
        }
    }
}
