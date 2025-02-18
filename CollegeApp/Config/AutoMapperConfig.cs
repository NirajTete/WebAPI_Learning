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

            // To Map diffrent prop name  NOTE: before ReverseMap() it Map StudentDTO -> Student and after it Map StudentDTO <- Student
            //CreateMap<StudentDTO , Student>().ReverseMap().ForMember(n => n.Name, opt => opt.MapFrom(x => x.StudentName)); // It will map both StudentDTO <-> Student

            // To Ignore Specific prop   NOTE: before ReverseMap() it Map StudentDTO -> Student and after it Map StudentDTO <- Student
            //CreateMap<StudentDTO , Student>().ReverseMap().ForMember(n => n.StudentName, opt => opt.Ignore()); // It will map both StudentDTO <-> Student

            // To transform the null value 
            //CreateMap<StudentDTO , Student>().ReverseMap().AddTransform<string>(n => string.IsNullOrEmpty(n) ? "No address found" : n); // It will map both StudentDTO <-> Student

            CreateMap<StudentDTO, Student>().ReverseMap();
        }
    }
}
