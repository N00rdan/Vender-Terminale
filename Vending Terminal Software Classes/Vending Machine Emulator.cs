using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.IO;

namespace Vending_Terminal_Software_Classes
{
    public class Vending_Machine_Emulator
    {
        public string TerminalLocation;

        public Context context = new Context();

        int BiggestCoin;

        public DataBase Data = new DataBase();

        List<Banknotes_n_Coins> ChangeList = new List<Banknotes_n_Coins>(); string ChangeString;

        public CurrentProduct pCopy { get; set; }

        public event Action MainRefresh;

        public event Action SupplierRefresh;

        public event Action<string> MessageBox;

        public Vending_Machine_Emulator()
        {

        }

        public void Buy()
        {
            context.CurrentStates.First(n => n.Location == TerminalLocation).Money -= pCopy.Price;

            var Product = context.CurrentStates
                .Include(cs => cs.Product)
                .First(n => n.Location == TerminalLocation)
                .Product.Find(n => n.Code == context.Info
                .First(m => m.Name == pCopy.Name).Code);

            Product.Amount -= 1;

            context.CurrentStates
                .Include(cs => cs.Sales)
                .First(n => n.Location == TerminalLocation)
                .Sales.Add(new Sale { Code = Product.Code, Date = DateTime.Now });

            context.SaveChanges();

            MR();
        }

        public void Change() // Usual change method
        {
            ChangeList.Clear();

            if (GiveFive() == true)
            {
                ComplicatedChange();
            }

            while (context.CurrentStates.First(n => n.Location == TerminalLocation).Money > 0)
            {
                var Coin = context.CurrentStates
                    .Include(cs => cs.Banknotes_n_Coins)
                    .First(n => n.Location == TerminalLocation)
                    .Banknotes_n_Coins.Find(n => n.Cost <= context.CurrentStates
                    .First(m => m.Location == TerminalLocation).Money & n.Amount > 0 & n.CanBeChange == true);

                Coin.Amount -= 1;
                context.CurrentStates.First(n => n.Location == TerminalLocation).Money -= Coin.Cost;

                // Given coins counter

                if (ChangeList.Exists(n => n.Cost == Coin.Cost))
                {
                    var tmp = ChangeList.Find(n => n.Cost == Coin.Cost);
                    tmp.Amount += 1;
                }
                else
                {
                    ChangeList.Add(new Banknotes_n_Coins { Cost = Coin.Cost, Amount = 1 });
                }
            }

            context.SaveChanges();

            // ChangeString creater

            ChangeList.Sort(new MoneyComparer());

            ChangeString = "Change is given:";

            ChangeList.ForEach(delegate (Banknotes_n_Coins n) { ChangeString += " " + n.Cost + "₽x" + n.Amount; });

            MR();

            MessageBox?.Invoke(ChangeString);
        }

        public void ComplicatedChange() // This change method gives only 1 5₽ coin
        {
            context.CurrentStates.First(n => n.Location == TerminalLocation).Money -= 5;

            var Coin = context.CurrentStates.Include(cs => cs.Banknotes_n_Coins).First(n => n.Location == TerminalLocation).Banknotes_n_Coins.Find(n => n.Cost == 5);
            Coin.Amount -= 1;

            ChangeList.Add(new Banknotes_n_Coins { Cost = 5, Amount = 1 });

            while (context.CurrentStates.First(n => n.Location == TerminalLocation).Money > 0)
            {
                Coin = context.CurrentStates.Include(cs => cs.Banknotes_n_Coins).First(n => n.Location == TerminalLocation).Banknotes_n_Coins.Find(n => n.Cost <= context.CurrentStates.First(m => m.Location == TerminalLocation).Money & n.Amount > 0 & n.CanBeChange == true & n.Cost != 5);

                Coin.Amount -= 1;
                context.CurrentStates.First(n => n.Location == TerminalLocation).Money -= Coin.Cost;

                // Given coins counter

                if (ChangeList.Exists(n => n.Cost == Coin.Cost))
                {
                    var tmp = ChangeList.Find(n => n.Cost == Coin.Cost);
                    tmp.Amount += 1;
                }
                else
                {
                    ChangeList.Add(new Banknotes_n_Coins { Cost = Coin.Cost, Amount = 1 });
                }
            }
        }

        public void MoneyAdd(int value)
        {
            var Coin = context.CurrentStates.Include(cs => cs.Banknotes_n_Coins).First(n => n.Location == TerminalLocation).Banknotes_n_Coins.Find(n => n.Cost == value);

            Coin.Amount += 1;
            context.CurrentStates.First(n => n.Location == TerminalLocation).Money += value;

            context.SaveChanges();

            MR();
        }

        public bool ChangeCount(int tag)
        {
            BiggestCoin = context.CurrentStates
                .Include(cs => cs.Banknotes_n_Coins)
                .First(n => n.Location == TerminalLocation)
                .Banknotes_n_Coins.Find(n => n.CanBeChange == true)
                .Cost;

            if (tag <= BiggestCoin)
                return true;

            int tmp = 0;

            using (var context = new Context())
            {
                var Change = context.CurrentStates.Include(cs => cs.Banknotes_n_Coins).First(n => n.Location == TerminalLocation).Banknotes_n_Coins.FindAll(n => n.CanBeChange == true);

                foreach (Banknotes_n_Coins B in Change)
                {
                    tmp += B.Cost * B.Amount;
                }

                if (tag + context.CurrentStates.First(n => n.Location == TerminalLocation).Money <= tmp)
                    return true;
                else
                    return false;
            }
        }

        public bool GiveFive() // Check amount of 1₽ coins and if change is odd
        {
            if (context.CurrentStates.Include(cs => cs.Banknotes_n_Coins).First(n => n.Location == TerminalLocation).Banknotes_n_Coins.Find(n => n.Cost == 1).Amount == 0 & context.CurrentStates.First(n => n.Location == TerminalLocation).Money % 2 == 1)
                return true;
            else
                return false;
        }

        public bool CanBeBought()
        {
            try
            {
                if (pCopy.Price <= context.CurrentStates.First(n => n.Location == TerminalLocation).Money & pCopy.Amount > 0)
                {
                    if (context.CurrentStates.Include(cs => cs.Banknotes_n_Coins).First(n => n.Location == TerminalLocation).Banknotes_n_Coins.Find(n => n.Cost == 1).Amount == 0 & context.CurrentStates.First(n => n.ID == 1).Money - pCopy.Price < 5)
                        return false;
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public void SR()
        {
            SupplierRefresh?.Invoke();
        }

        public void MR()
        {
            MainRefresh?.Invoke();
        }
    }
}
