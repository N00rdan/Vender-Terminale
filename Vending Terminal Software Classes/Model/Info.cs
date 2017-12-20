using System.ComponentModel.DataAnnotations;

namespace Vending_Terminal_Software_Classes
{
    public class Info
    {
        [Key]
        [MaxLength(4)]
        public string Code { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public int SellingPrice { get; set; }

        public int BuyingPrice { get; set; }
    }
}
