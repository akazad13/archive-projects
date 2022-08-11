using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public interface IHostelRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        IEnumerable<Hostel> Get();
        Hostel Get(int id);
        bool SaveAll();
    }
}
