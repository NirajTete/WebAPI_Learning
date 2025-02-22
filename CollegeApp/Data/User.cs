using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_Learning.Data
{
    public class User
    {
        public int Id { get; set; } 
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        
        public int UserTypeId { get; set; }

        [ForeignKey(nameof(UserTypeId))]
        public virtual UserType UserType { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<UserRoleMapping> UserRoleMappings { get; set; }  
    }
}
