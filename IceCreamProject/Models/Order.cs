using System.ComponentModel.DataAnnotations;

namespace IceCreamProject.Models
{
    public class Order
    {
        [Key]
        public int Order_ID { get; set; }
        [Required]
        public int Product_ID { get; set; }
        public string Product_Name { get; set; }
        public string Customer_Name { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public int Amount_Payable { get; set; }
        public int Quantity { get; set; }

    }
}
