using System.ComponentModel.DataAnnotations;

namespace Vending_Terminal_Software_Classes
{
    public class Product : Item
    {
        public int ID { get; set; }

        [MaxLength(4)]
        public string Code { get; set; }

        public Info Info { get; set; }
    }
}
