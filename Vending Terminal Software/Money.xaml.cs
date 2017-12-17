using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Vending_Terminal_Software
{
    /// <summary>
    /// Логика взаимодействия для Money.xaml
    /// </summary>
    public partial class Money : Window
    {
        public Money()
        {
            InitializeComponent();

            Current.Text = ((App)Application.Current).dataBase.CurrentState.Money.ToString();
        }

        private void Money_Click(object sender, RoutedEventArgs e)
        {
            var tmp = sender as Button;
            var tag = tmp.Tag as string;

            if (Sum() >= ((App)Application.Current).dataBase.CurrentState.Money + Convert.ToInt32(tag))
            {
                ((App)Application.Current).dataBase.CurrentState.Money = ((App)Application.Current).dataBase.CurrentState.Money + Convert.ToInt32(tag);

                foreach (Banknotes_n_Coins B in ((App)Application.Current).dataBase.CurrentState.Banknotes_n_Coins)
                    if (B.Cost == Convert.ToUInt32(tag))
                    {
                        B.Amount += 1;
                        ((App)Application.Current).dataBase.Save();
                    }

                Current.Text = ((App)Application.Current).dataBase.CurrentState.Money.ToString();
            }
            else
                MessageBox.Show("В автомате не хватает сдачи");
        }

        private int Sum()
        {
            int sum = 0;

            foreach (Banknotes_n_Coins B in ((App)Application.Current).dataBase.CurrentState.Banknotes_n_Coins)
            {
                if(B.CanBeChange == true)
                    sum += B.Cost * B.Amount;
            }

            return sum;
        }
    }
}
