using System.ComponentModel.DataAnnotations;

namespace WebAPI_Learning.Models
{
    public class RolePrivilegeDTO
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        [Required]
        [MaxLength(250)]
        public string RolePrivilegeName { get; set; }
        public string Description { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
