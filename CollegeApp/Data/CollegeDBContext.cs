using CollegeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPI_Learning.Data
{
    public class CollegeDBContext : DbContext
    {

        public CollegeDBContext(DbContextOptions<CollegeDBContext> options) : base(options)
        {

        }

        DbSet<Student> Students { get; set; }


        //to add default data using overriding the model while migration 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // to add default data
            modelBuilder.Entity<Student>().HasData(new List<Student>()
            {
                new Student{Id = 1, StudentName = "Janvi", Address = "Kapsi", Email = "janvi@gmail.com", DOB = new DateTime(2004,06,06)},
                new Student{Id = 2, StudentName = "Hasari", Address = "Nagpur", Email = "hasari@gmail.com", DOB = new DateTime(2008,06,06)},
                new Student{Id = 3, StudentName = "Jay", Address = "Kapsi", Email = "jay@gmail.com", DOB = new DateTime(2004,06,06)},
                new Student{Id = 4, StudentName = "Lisa", Address = "Bhandara", Email = "lisa@gmail.com", DOB = new DateTime(2005,06,06)},
                new Student{Id = 5, StudentName = "Rosy", Address = "Bhandara", Email = "rosy@gmail.com", DOB = new DateTime(2005,06,06)},
            });
        }

    }
}
