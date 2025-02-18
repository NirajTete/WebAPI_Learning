using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebAPI_Learning.Data;
using WebAPI_Learning.Repository.Implementation;

namespace WebAPI_Learning.Repository.Service
{
    public class StudentRepository : CollegeRepository<Student>, IStudentRepository
    {
        private readonly CollegeDBContext _dbContext;

        public StudentRepository(CollegeDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Student>> GetStudentsByFeeStatus(int feeStatus)
        {
            //
            return null;
        }
    }
}
