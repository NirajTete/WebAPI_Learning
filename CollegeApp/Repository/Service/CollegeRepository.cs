using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAPI_Learning.Data;
using WebAPI_Learning.Repository.Implementation;

namespace WebAPI_Learning.Repository.Service
{
    public class CollegeRepository<T> : ICollegeRepository<T> where T : class
    {
        private readonly CollegeDBContext _dbContext;
        private DbSet<T> _dbSet;

        public CollegeRepository(CollegeDBContext dBContext)
        {
            _dbContext = dBContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T> Create(T dbRecord)
        {
            _dbSet.Add(dbRecord);
            await _dbContext.SaveChangesAsync();

            return dbRecord;
        }

        public async Task<bool> Delete(T dbRecord)
        {
            //var studentToupdate = await _dbContext.Students.Where(student => student.Id == student.Id).FirstOrDefaultAsync();
            //if (studentToupdate == null)
            //    throw new ArgumentNullException($"No student found with id: {id}");

            _dbSet.Remove(dbRecord);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByPara(Expression<Func<T, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _dbSet.AsNoTracking().Where(filter).FirstOrDefaultAsync();

            ////Non Generic 
            //return await _dbSet.AsNoTracking().Where(student => student.Id == id).FirstOrDefaultAsync();

            else
                return await _dbSet.Where(filter).FirstOrDefaultAsync();
            //Non Generic
            //return await _dbSet.Where(student => student.Id == id).FirstOrDefaultAsync();

        }

        /* public async Task<T> GetByName(Expression<Func<T, bool>> filter)
         {
             return await _dbSet.Where(filter).FirstOrDefaultAsync();
         }*/

        public async Task<T> Update(T dbRecord)
        {
            /* var studentToupdate = await _dbContext.Students.Where(student => student.Id == student.Id).FirstOrDefaultAsync();
             if (studentToupdate == null)
                 throw new ArgumentNullException($"No student found with id: {student.Id}");

             if (studentToupdate != null)
             {
                 studentToupdate.StudentName = student.StudentName;
                 studentToupdate.Email = student.Email;
                 studentToupdate.Address = student.Address;
                 studentToupdate.DOB = student.DOB;
             }*/

            _dbContext.Update(dbRecord);
            await _dbContext.SaveChangesAsync();

            return dbRecord;
        }
    }
}
