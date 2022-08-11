using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public interface IChatRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        IEnumerable<Chat> Get();
        Chat Get(int id);
        bool SaveAll();
    }
}
