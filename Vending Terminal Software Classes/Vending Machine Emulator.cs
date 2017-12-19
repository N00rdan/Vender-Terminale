using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.IO;

namespace Vending_Terminal_Software_Classes
{
    public class Vending_Machine_Emulator
    {
        int BiggestCoin;

        public DataBase Data = new DataBase();

        List<Banknotes_n_Coins> ChangeList = new List<Banknotes_n_Coins>(); string ChangeString;

        public Product pCopy { get; set; }

        public event Action MainRefresh;

        public event Action SupplierRefresh;

        public event Action<string> MessageBox;

        public Vending_Machine_Emulator()
        {

        }

        public void Buy()
        {
            Data.CurrentState.Money -= pCopy.Price;

            var Product = Data.CurrentState.Product.Find(n => n == pCopy);

            Product.Amount -= 1;

            Data.Save();

            MR();
        }

        public void Change() // Usual change method
        {
            ChangeList.Clear();

            if (GiveFive() == true)
            {
                ComplicatedChange();
            }

            while (Data.CurrentState.Money > 0)
            {
                var Coin = Data.CurrentState.Banknotes_n_Coins.Find(n => n.Cost <= Data.CurrentState.Money & n.Amount > 0 & n.CanBeChange == true);

                Coin.Amount -= 1;
                Data.CurrentState.Money -= Coin.Cost;

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

            Data.Save();

            // ChangeString creater

            ChangeList.Sort(new MoneyComparer());

            ChangeString = "Change is given:";

            ChangeList.ForEach(delegate (Banknotes_n_Coins n) { ChangeString += " " + n.Cost + "₽x" + n.Amount; });

            MR();

            MessageBox?.Invoke(ChangeString);
        }

        public void ComplicatedChange() // This change method gives only 1 5₽ coin
        {
            Data.CurrentState.Money -= 5;

            var Coin = Data.CurrentState.Banknotes_n_Coins.Find(n => n.Cost == 5);
            Coin.Amount -= 1;

            ChangeList.Add(new Banknotes_n_Coins { Cost = 5, Amount = 1 });

            while (Data.CurrentState.Money > 0)
            {
                Coin = Data.CurrentState.Banknotes_n_Coins.Find(n => n.Cost <= Data.CurrentState.Money & n.Amount > 0 & n.CanBeChange == true & n.Cost != 5);

                Coin.Amount -= 1;
                Data.CurrentState.Money -= Coin.Cost;

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
            var Coin = Data.CurrentState.Banknotes_n_Coins.Find(n => n.Cost == value);

            Coin.Amount += 1;
            Data.CurrentState.Money += value;

            Data.Save();

            MR();
        }

        public bool ChangeCount(int tag)
        {
            BiggestCoin = Data.CurrentState.Banknotes_n_Coins.Find(n => n.CanBeChange == true).Cost;

            if (tag <= BiggestCoin)
                return true;

            int tmp = 0;
            var Change = Data.CurrentState.Banknotes_n_Coins.FindAll(n => n.CanBeChange == true);

            foreach (Banknotes_n_Coins B in Change)
            {
                tmp += B.Cost * B.Amount;
            }

            if (tag + Data.CurrentState.Money <= tmp)
                return true;
            else
                return false;
        }

        public bool GiveFive() // Check amount of 1₽ coins and if change is odd
        {
            if (Data.CurrentState.Banknotes_n_Coins.Find(n => n.Cost == 1).Amount == 0 & Data.CurrentState.Money % 2 == 1)
                return true;
            else
                return false;
        }

        public bool CanBeBought()
        {
            try
            {
                if (pCopy.Price <= Data.CurrentState.Money & pCopy.Amount > 0)
                {
                    if (Data.CurrentState.Banknotes_n_Coins.Find(n => n.Cost == 1).Amount == 0 & Data.CurrentState.Money - pCopy.Price < 5)
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
