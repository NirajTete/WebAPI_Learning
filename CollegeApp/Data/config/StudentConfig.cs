using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace WebAPI_Learning.Data.config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).UseIdentityColumn();
            builder.Property(s => s.StudentName).IsRequired().HasMaxLength(250);
            builder.Property(s => s.Email).IsRequired().HasMaxLength(250);
            builder.Property(s => s.Address).HasMaxLength(500);

            // to add default data
            builder.HasData(new List<Student>()
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
