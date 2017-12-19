using System.Collections.Generic;

namespace Vending_Terminal_Software_Classes
{
    public class CurrentState
    {
        public int Money { get; set; }
        public List<Product> Product { get; set; }
        public List<Banknotes_n_Coins> Banknotes_n_Coins { get; set; }
    }
}