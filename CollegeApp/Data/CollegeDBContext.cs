﻿using CollegeApp.Models;
using Microsoft.EntityFrameworkCore;
using WebAPI_Learning.Data.config;


namespace WebAPI_Learning.Data
{
    public class CollegeDBContext : DbContext
    {

        public CollegeDBContext(DbContextOptions<CollegeDBContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePrivilege> RolePrivileges { get; set; }
        public DbSet<UserRoleMapping> UserRoleMappings { get; set; }
        public DbSet<UserType> UserTypes { get; set; }


        //to add default data using overriding the model while migration 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*// to add default data
            modelBuilder.Entity<Student>().HasData(new List<Student>()
            {
                new Student{Id = 1, StudentName = "Janvi", Address = "Kapsi", Email = "janvi@gmail.com", DOB = new DateTime(2004,06,06)},
                new Student{Id = 2, StudentName = "Hasari", Address = "Nagpur", Email = "hasari@gmail.com", DOB = new DateTime(2008,06,06)},
                new Student{Id = 3, StudentName = "Jay", Address = "Kapsi", Email = "jay@gmail.com", DOB = new DateTime(2004,06,06)},
                new Student{Id = 4, StudentName = "Lisa", Address = "Bhandara", Email = "lisa@gmail.com", DOB = new DateTime(2005,06,06)},
                new Student{Id = 5, StudentName = "Rosy", Address = "Bhandara", Email = "rosy@gmail.com", DOB = new DateTime(2005,06,06)},
            });

            //to add default validation
            modelBuilder.Entity<Student>(entity =>
            {               
                entity.Property(e => e.StudentName).IsRequired();
                entity.Property(e => e.StudentName).HasMaxLength(250);
                entity.Property(entity => entity.Address).IsRequired(false).HasMaxLength(500);
                entity.Property(entity => entity.Email).IsRequired().HasMaxLength(250);
            });*/

            modelBuilder.ApplyConfiguration(new StudentConfig());
            modelBuilder.ApplyConfiguration(new DepartmentConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
        }
        
    }
}

