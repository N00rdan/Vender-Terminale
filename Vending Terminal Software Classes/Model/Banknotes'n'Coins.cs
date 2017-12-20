using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vending_Terminal_Software_Classes
{
    public class Banknotes_n_Coins : Item
    {
        public int ID { get; set; }

        public int Cost { get; set; }

        public bool CanBeChange { get; set; }
    }

    public class MoneyComparer : IComparer<Banknotes_n_Coins>
    {
        public int Compare(Banknotes_n_Coins B1, Banknotes_n_Coins B2)
        {
            if (B1.Cost < B2.Cost)
                return 1;
            if (B1.Cost > B2.Cost)
                return -1;
            else
                return 0;
        }
    }
}
