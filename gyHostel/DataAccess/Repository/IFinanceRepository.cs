using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public interface IFinanceRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        IEnumerable<Finance> Get();
        Finance Get(int id);
        bool SaveAll();
    }
}
