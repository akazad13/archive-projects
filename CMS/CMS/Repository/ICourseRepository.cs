using CMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Repository
{
    public interface ICourseRepository
    {
        void Add<T>(T entity) where T : class;
        void AddRange<T>(List<T> entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<IEnumerable<Course>> GetAll();
        Task<IEnumerable<Course>> GetAll(int userId);
        Task<Course> GetOne(int id);
        Task<bool> SaveAll();
    }
}
