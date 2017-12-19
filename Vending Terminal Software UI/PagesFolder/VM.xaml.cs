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
using Vending_Terminal_Software_Classes;

namespace Vending_Terminal_Software_UI.PagesFolder
{
    /// <summary>
    /// Логика взаимодействия для VM.xaml
    /// </summary>
    public partial class VM : System.Windows.Controls.Page
    {
        Vending_Machine_Emulator VME = new Vending_Machine_Emulator();

        public VM()
        {
            InitializeComponent();

            VME.MainRefresh += Refresh;
            VME.MessageBox += Message;
        }

        private void listBoxSupplies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxSupplies.SelectedItem is Product p)
            {
                VME.pCopy = p;
                Refresh();
            }
        }

        private void AddMoney_Click(object sender, RoutedEventArgs e)
        {
            AddMoney Add = new AddMoney(VME);
            Add.ShowDialog();
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            VME.Buy();
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            VME.Change();
        }

        private void ProductsAdd_Click(object sender, RoutedEventArgs e)
        {
            Supplier9000 Supp = new Supplier9000(VME);
            Supp.ShowDialog();
        }

        private void Refresh()
        {
            listBoxSupplies.ItemsSource = VME.Data.CurrentState.Product;

            Current.Text = VME.Data.CurrentState.Money.ToString();
            
            if (VME.CanBeBought() == true)
                Buy.IsEnabled = true;
            else
                Buy.IsEnabled = false;

            if (VME.Data.CurrentState.Money > 0)
                Change.IsEnabled = true;
            else
                Change.IsEnabled = false;
        }

        private void Message(string str)
        {
            MessageBox.Show(str);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
