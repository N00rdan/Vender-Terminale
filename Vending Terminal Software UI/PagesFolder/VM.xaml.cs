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
            if (listBoxSupplies.SelectedItem is CurrentProduct p)
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
            Supplier9000 Supp = new Supplier9000(VME, VME.context);
            Supp.ShowDialog();
        }

        private void Refresh()
        {
            listBoxSupplies.ItemsSource = VME.Data.GetData();

            using (var context = new Context())
            {
                Current.Text = context.CurrentStates.First(n => n.Location == VME.TerminalLocation).Money.ToString();
            }

            if (VME.CanBeBought() == true)
                Buy.IsEnabled = true;
            else
                Buy.IsEnabled = false;
            
            if (VME.context.CurrentStates.First(n => n.Location == VME.TerminalLocation).Money > 0)
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
            VME.TerminalLocation = ((App)Application.Current).Location;
            VME.Data.TerminalLocation = ((App)Application.Current).Location;

            Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.SelectVM);
        }
    }
}
