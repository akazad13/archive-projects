using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public interface IRoomRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        IEnumerable<Room> Get(string sort, string type);
        Room Get(int id);
        bool SaveAll();
    }
}
