using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_Learning.Data
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string RoleName { get; set; }
        public string Description { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
      
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<RolePrivilege> RolePrivileges { get; set; } = new List<RolePrivilege>();

        public virtual ICollection<UserRoleMapping> UserRoleMappings { get; set; }
    }
}
