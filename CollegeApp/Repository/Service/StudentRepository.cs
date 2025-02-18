using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebAPI_Learning.Data;
using WebAPI_Learning.Repository.Implementation;

namespace WebAPI_Learning.Repository.Service
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CollegeDBContext _dbContext;

        public StudentRepository(CollegeDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Create(Student student)
        {
            _dbContext.Students.Add(student);
            await _dbContext.SaveChangesAsync();

            return student.Id;
        }

        public async Task<bool> Delete(Student student)
        {
            //var studentToupdate = await _dbContext.Students.Where(student => student.Id == student.Id).FirstOrDefaultAsync();
            //if (studentToupdate == null)
            //    throw new ArgumentNullException($"No student found with id: {id}");

            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<Student>> GetAll()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public async Task<Student> GetById(int id, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _dbContext.Students.AsNoTracking().Where(student => student.Id == id).FirstOrDefaultAsync();

            else
                return await _dbContext.Students.Where(student => student.Id == id).FirstOrDefaultAsync();

        }

        public async Task<Student> GetByName(string name)
        {
            return await _dbContext.Students.Where(student => student.StudentName.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<int> Update(Student student)
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

            _dbContext.Update(student);
            await _dbContext.SaveChangesAsync();

            return student.Id;
        }
    }
}
