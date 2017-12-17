using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Vending_Terminal_Software
{
    class Operations
    {
        public void Sort(List<Banknotes_n_Coins> B)
        {
            B.Sort(delegate (Banknotes_n_Coins b1, Banknotes_n_Coins b2)
            { return b1.Cost.CompareTo(b2.Cost); });
            B.Reverse();
        }

        public void Сhange()
        {
            string change = "Change given:";
            int tmp = 0;

            do
            {
                foreach (Banknotes_n_Coins B in ((App)Application.Current).dataBase.CurrentState.Banknotes_n_Coins)
                {
                    if (B.CanBeChange == true)
                    {
                        A:

                        if (((App)Application.Current).dataBase.CurrentState.Money >= B.Cost & ((App)Application.Current).dataBase.CurrentState.Money != 0 & B.Amount != 0)
                        {
                            B.Amount -= 1;
                            ((App)Application.Current).dataBase.CurrentState.Money -= B.Cost;
                            tmp += 1;

                            goto A;
                        }
                        else
                        {
                            change = change + " " + B.Cost + "₽x" + tmp;
                            tmp = 0;
                        }
                    }
                }
            } while (((App)Application.Current).dataBase.CurrentState.Money != 0);

            MessageBox.Show(change);
        }

        public void FiveChange()
        {
            string change = "Change given:";
            int tmp = 0;

            ((App)Application.Current).dataBase.CurrentState.Money -= 5;

            do
            {
                foreach (Banknotes_n_Coins B in ((App)Application.Current).dataBase.CurrentState.Banknotes_n_Coins)
                {
                    if (B.CanBeChange == true)
                    {
                        A:

                        if (((App)Application.Current).dataBase.CurrentState.Money >= B.Cost & ((App)Application.Current).dataBase.CurrentState.Money != 0 & B.Amount != 0 & B.Cost != 5)
                        {
                            B.Amount -= 1;
                            ((App)Application.Current).dataBase.CurrentState.Money -= B.Cost;
                            tmp += 1;

                            goto A;
                        }
                        else
                        {
                            if (B.Cost == 5)
                            {
                                tmp += 1; B.Amount -= 1;
                            }

                            change = change + " " + B.Cost + "₽x" + tmp;
                            tmp = 0;

                        }
                    }
                }
            } while (((App)Application.Current).dataBase.CurrentState.Money != 0);

            MessageBox.Show(change);
        }

        public bool AmountIsZero(int value)
        {
            try
            {
                bool tmp = false;

                foreach (Banknotes_n_Coins b in ((App)Application.Current).dataBase.CurrentState.Banknotes_n_Coins)
                {
                    if (b.Cost == value)
                    {
                        if (b.Amount == 0)
                            tmp = true;
                    }
                }

                return tmp;
            }
            catch { return false; }
        }
    }
}
