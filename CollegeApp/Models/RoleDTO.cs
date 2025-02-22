using System.ComponentModel.DataAnnotations;

namespace WebAPI_Learning.Models
{
    public class RoleDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string RoleName { get; set; }
        public string Description { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
