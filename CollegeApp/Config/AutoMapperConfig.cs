using AutoMapper;
using CollegeApp.Models;
using WebAPI_Learning.Data;

namespace WebAPI_Learning.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //CreateMap<StudentDTO, Student>(); // To map StudentDTO -> Student
            //CreateMap<Student, StudentDTO>(); // To map Student -> StudentDTO

            CreateMap<StudentDTO , Student>().ReverseMap(); // It will map both StudentDTO <-> Student
        }
    }
}
