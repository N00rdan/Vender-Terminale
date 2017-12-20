using System.Collections.Generic;

namespace Vending_Terminal_Software_Classes
{
    public class CurrentState
    {
        public int ID { get; set; }
        public string Location { get; set; }
        public int Money { get; set; }
        public List<Product> Product { get; set; }
        public List<Banknotes_n_Coins> Banknotes_n_Coins { get; set; }
        public int Profit { get; set; }
        public List<Sale> Sales { get; set; }

        public int CountZeroes()
        {
            return Product.FindAll(n => n.Amount == 0).Count;
        }
    }
}