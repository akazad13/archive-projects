using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public interface IRecordRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        IEnumerable<Record> Get();
        Record Get(int id);
        bool SaveAll();
    }
}
