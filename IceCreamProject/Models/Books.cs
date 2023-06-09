using System.ComponentModel.DataAnnotations;

namespace IceCreamProject.Models
{
    public class Books
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string B_url { get; set; }
        [Required]
        public string B_name { get; set; }
        
        public string B_Desc { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
