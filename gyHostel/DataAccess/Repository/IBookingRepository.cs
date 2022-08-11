using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public interface IBookingRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        IEnumerable<Booking> Get();
        Booking Get(int id);
        bool SaveAll();
    }
}
