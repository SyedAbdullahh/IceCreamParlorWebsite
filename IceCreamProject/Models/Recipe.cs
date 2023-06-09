using System.ComponentModel.DataAnnotations;

namespace IceCreamProject.Models
{
    public class Recipe
    {
        [Key]
        public int Recipe_ID { get; set; }
        [Required]
        public string Recipe_By { get; set; }
        public string R_Name { get; set; }
        [Required]
        public string R_Url { get; set; }
        [Required]
        public string Ingredients { get; set; }

        public string Procedure { get; set; }
    }
}
