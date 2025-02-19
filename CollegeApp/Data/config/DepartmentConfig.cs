using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace WebAPI_Learning.Data.config
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).UseIdentityColumn();
            builder.Property(s => s.DepartmentName).IsRequired().HasMaxLength(200);
            builder.Property(s => s.Description).IsRequired().HasMaxLength(500).IsRequired(false);
           

            // to add default data
            builder.HasData(new List<Department>()
            {
                new Department{Id = 1, DepartmentName = "OT", Description = "Operation Theater Technician"},
                new Department{Id = 2, DepartmentName = "School", Description = "NA"},
                new Department{Id = 3, DepartmentName = "Haldirams", Description = "Kapsi Plant"},
                new Department{Id = 4, DepartmentName = "GNM", Description = "Bhandara College"},
                new Department{Id = 5, DepartmentName = "Singer", Description = "Black Pink"},
            });
        }

    }
}
