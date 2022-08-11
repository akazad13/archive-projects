using CMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Repository
{
    public interface IQuizRepository
    {
        void Add<T>(T entity) where T : class;
        void AddRange<T>(List<T> entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<IEnumerable<Quiz>> GetAll();
        Task<IEnumerable<Quiz>> GetAll(int userId);
        Task<Quiz> GetOne(int id);
        Task<IEnumerable<QAndA>> GetQuestions(int quizId);
        Task<IEnumerable<dynamic>> GetAllEvaluations();
        Task<bool> SaveAll();
    }
}
