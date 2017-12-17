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
    /// Логика взаимодействия для Supplier9000.xaml
    /// </summary>
    public partial class Supplier9000 : Window
    {
        public Supplier9000()
        {
            InitializeComponent();

            listBoxBanknotesCoins.ItemsSource = ((App)Application.Current).dataBase.CurrentState.Banknotes_n_Coins;
            listBoxSupplies.ItemsSource = ((App)Application.Current).dataBase.CurrentState.Product;
        }

        private void listBoxSupplies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxSupplies.SelectedItem is Product p)
            {
                textBlockName.Text = $"{p.Name}";
                textBoxAmount.Text = $"{p.Amount}";
                textBoxPrice.Text = $"{p.Price}";
                textBoxPrice.IsEnabled = true;
            }
        }

        private void listBoxBanknotesCoins_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxBanknotesCoins.SelectedItem is Banknotes_n_Coins b)
            {
                textBlockName.Text = $"{b.Cost}";
                textBoxAmount.Text = $"{b.Amount}";
                textBoxPrice.Clear();
                textBoxPrice.IsEnabled = false;
            }
        }

        private void textBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                foreach (Banknotes_n_Coins b in ((App)Application.Current).dataBase.CurrentState.Banknotes_n_Coins)
                {
                    if (b.Cost == Convert.ToInt32(textBlockName.Text))
                    {
                        b.Amount = Convert.ToInt32(textBoxAmount.Text);
                    }
                }
            }
            catch { }

            try
            {
                foreach (Product p in ((App)Application.Current).dataBase.CurrentState.Product)
                {
                    if (p.Name == textBlockName.Text)
                    {
                        p.Amount = Convert.ToInt32(textBoxAmount.Text);
                        p.Price = Convert.ToInt32(textBoxPrice.Text);
                    }
                }
            }
            catch { }

            ((App)Application.Current).dataBase.Save();
        }
    }
}
