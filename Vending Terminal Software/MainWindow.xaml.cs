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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vending_Terminal_Software
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Operations O = new Operations();
        Product p_copy;

        public MainWindow()
        {
            InitializeComponent();

            Current.IsReadOnly = true;

            listBoxSupplies.ItemsSource = ((App)Application.Current).dataBase.CurrentState.Product;
            Current.Text = ((App)Application.Current).dataBase.CurrentState.Money.ToString();
            Buy.IsEnabled = false;

            if (Current.Text != "0")
                Change.IsEnabled = true;
            else
                Change.IsEnabled = false;
        }

        private void listBoxSupplies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxSupplies.SelectedItem is Product p)
            {
                if (((App)Application.Current).dataBase.CurrentState.Money >= p.Price & p.Amount > 0)
                {
                    p_copy = p;

                    if (O.AmountIsZero(1) == true & O.AmountIsZero(5) == false & ((App)Application.Current).dataBase.CurrentState.Money - p_copy.Price >= 5)
                    {
                        Buy.IsEnabled = true;
                    }
                    else
                    {
                        if (O.AmountIsZero(1) == false)
                            Buy.IsEnabled = true;
                        else
                            Buy.IsEnabled = false;
                    }
                }
                else
                    Buy.IsEnabled = false;
            }
        }

        private void ProductsAdd_Click(object sender, RoutedEventArgs e)
        {
            Supplier9000 _supplier9000 = new Supplier9000();
            _supplier9000.ShowDialog();

            listBoxSupplies.ItemsSource = null;
            listBoxSupplies.ItemsSource = ((App)Application.Current).dataBase.CurrentState.Product;
        }

        private void AddMoney_Click(object sender, RoutedEventArgs e)
        {
            Money _money = new Money();
            _money.ShowDialog();

            if (((App)Application.Current).dataBase.CurrentState.Money > 0)
            {
                Current.Text = ((App)Application.Current).dataBase.CurrentState.Money.ToString();

                if (listBoxSupplies.SelectedItem is Product p)
                {
                    if (((App)Application.Current).dataBase.CurrentState.Money >= p.Price)
                    {
                        Buy.IsEnabled = true;
                    }
                    else
                        Buy.IsEnabled = false;
                }

                Change.IsEnabled = true;
            }
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            foreach (Product p in ((App)Application.Current).dataBase.CurrentState.Product)
            {
                if (p == p_copy)
                {
                    p.Amount -= 1;

                    ((App)Application.Current).dataBase.CurrentState.Money -= p.Price;
                    Current.Text = ((App)Application.Current).dataBase.CurrentState.Money.ToString();

                    if (((App)Application.Current).dataBase.CurrentState.Money < p.Price)
                        Buy.IsEnabled = false;

                    ((App)Application.Current).dataBase.Save();

                    string tmp = "Congratulations, you bought " + p.Name + "!";
                    MessageBox.Show(tmp);
                }
            }

        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (O.AmountIsZero(1) == false)
            {
                StandartChange();
            }
            else
            {
                if ((((App)Application.Current).dataBase.CurrentState.Money) % 2 != 0)
                {
                    O.FiveChange();
                    Change.IsEnabled = false;
                    Current.Text = ((App)Application.Current).dataBase.CurrentState.Money.ToString();
                    ((App)Application.Current).dataBase.Save();
                }
                else
                {
                    StandartChange();
                }
            }
        }

        private void StandartChange()
        {
            O.Сhange();
            Change.IsEnabled = false;
            Current.Text = ((App)Application.Current).dataBase.CurrentState.Money.ToString();
            ((App)Application.Current).dataBase.Save();
        }
    }
}
