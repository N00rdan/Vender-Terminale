//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Vending_Terminal_Software
{
    public class CurrentState
    {
        public int Money { get; set; }
        public List<Product> Product { get; set; }
        public List<Banknotes_n_Coins> Banknotes_n_Coins { get; set; }
    }
}