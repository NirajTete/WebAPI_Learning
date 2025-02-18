using System.Linq.Expressions;
using WebAPI_Learning.Data;

namespace WebAPI_Learning.Repository.Implementation
{
    public interface ICollegeRepository<T>
    {

        Task<List<T>> GetAll();

        Task<T> GetByPara(Expression<Func<T, bool>> filter, bool useNoTracking = false);

        //Task<T> GetByName(Expression<Func<T, bool>> filter);

        Task<T> Create(T dbRecord);

        Task<T> Update(T dbRecord);

        Task<bool> Delete(T dbRecord);
    }
}