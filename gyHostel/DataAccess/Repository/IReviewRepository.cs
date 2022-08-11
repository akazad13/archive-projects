using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public interface IReviewRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        IEnumerable<Review> Get();
        Review Get(int id);
        bool SaveAll();
    }
}
