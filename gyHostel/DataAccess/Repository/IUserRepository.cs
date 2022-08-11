using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repository
{
    public interface IUserRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        IEnumerable<User> Get();
        User Get(int id);
        User GetUserByAccount(string account);
        bool SaveAll();
    }
}
