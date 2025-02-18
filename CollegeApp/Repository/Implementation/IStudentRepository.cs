using WebAPI_Learning.Data;

namespace WebAPI_Learning.Repository.Implementation
{
    public interface IStudentRepository : ICollegeRepository<Student>
    {
       Task<List<Student>> GetStudentsByFeeStatus(int feeStatus);
    }
}
