using System.ComponentModel.DataAnnotations;

namespace IceCreamProject.Models
{
    public class Feedback
    {
        [Key]
        public  int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
