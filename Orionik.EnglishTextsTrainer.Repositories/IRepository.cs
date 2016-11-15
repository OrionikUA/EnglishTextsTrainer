using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orionik.EnglishTextsTrainer.Models;

namespace Orionik.EnglishTextsTrainer.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetList();
        T Insert(T item);
        void Update(T item);
        void Delete(int id);
    }
}
