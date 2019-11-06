using System.Collections.Generic;
using CookBook.Common.Models;

namespace CookBook.DAL.Repositories
{
    public interface IIngridientRepository
    {
        List<Ingridient> GetIngridients();
        void InsertInrgridient(Ingridient ingridient);
        void UpdateIngridient(Ingridient ingridient);
        void DeleteIngridient(int ingridientId);
        Ingridient GetIngridientById(int ingridientId);
    }
}
