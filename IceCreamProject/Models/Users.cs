using System.ComponentModel.DataAnnotations;

namespace IceCreamProject.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }

        
        public string? active { get; set; }
        [Required]
        public string Payment { get; set; }
    }
}
