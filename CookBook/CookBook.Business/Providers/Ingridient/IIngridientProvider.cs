using System.Collections.Generic;
using CookBook.Common.Models;
using CookBook.Common.Enums;

namespace CookBook.Business.Providers
{
    public interface IIngridientProvider
    {
        List<Ingridient> GetIngridients();
        void InsertInrgridient(Ingridient ingridient);
        void UpdateIngridient(Ingridient ingridient);
        void DeleteIngridient(int ingridientId);
        Ingridient GetIngridientById(int ingridientId);
        UniqueValidation IsUnique(string ingridientName);
    }
}
