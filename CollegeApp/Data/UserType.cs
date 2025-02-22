using System.ComponentModel.DataAnnotations;

namespace WebAPI_Learning.Data
{
    public class UserType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(1500)]
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
