using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Vending_Terminal_Software_Classes;

namespace Vending_Terminal_Software_UI
{
    /// <summary>
    /// Логика взаимодействия для Supplier9000.xaml
    /// </summary>
    public partial class Supplier9000 : Window
    {
        Product pcopy;
        Vending_Machine_Emulator vme;

        public Supplier9000(Vending_Machine_Emulator VME)
        {
            InitializeComponent();

            vme = VME;

            vme.SupplierRefresh += Refresh;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void listBoxSupplies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxSupplies.SelectedItem is Product p)
            {
                textBlockName.Text = $"{p.Name}";
                textBoxAmount.Text = $"{p.Amount}";
                textBoxPrice.Text = $"{p.Price}";

                pcopy = p;
            }
        }

        private void listBoxBanknotesCoins_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxBanknotesCoins.SelectedItem is Banknotes_n_Coins b)
            {
                textBlockName.Text = $"{b.Cost}";
                textBoxAmount.Text = $"{b.Amount}";
                textBoxPrice.Clear();
            }
        }

        private void textBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            // My code goes here
        }

        private void Refresh()
        {
            listBoxSupplies.ItemsSource = vme.Data.CurrentState.Product;

            listBoxBanknotesCoins.ItemsSource = vme.Data.CurrentState.Banknotes_n_Coins;
        }
    }
}
