using WebAPI_Learning.Data;

namespace WebAPI_Learning.Repository.Implementation
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAll();

        Task<Student> GetById(int id, bool useNoTracking = false);

        Task<Student> GetByName(string name);

        Task<int> Create(Student student);

        Task<int> Update(Student student);

        Task<bool> Delete(Student student);
    }
}
