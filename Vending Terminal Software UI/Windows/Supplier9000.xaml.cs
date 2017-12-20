using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Vending_Terminal_Software_Classes;
using System.Data.Entity;
using System;
using Vending_Terminal_Software_UI.Windows.Linq;

namespace Vending_Terminal_Software_UI
{
    /// <summary>
    /// Логика взаимодействия для Supplier9000.xaml
    /// </summary>
    public partial class Supplier9000 : Window
    {
        CurrentProduct pcopy;
        Context Context;
        Vending_Machine_Emulator vme;

        public Supplier9000(Vending_Machine_Emulator VME, Context c)
        {
            InitializeComponent();

            Context = c;
            vme = VME;

            vme.SupplierRefresh += Refresh;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void listBoxSupplies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxSupplies.SelectedItem is CurrentProduct p)
            {
                textBlockName.Text = $"{p.Name}";
                textBoxAmount.Text = $"{p.Amount}";
                textBoxPrice.Text = $"{p.Price}";
                Delete.IsEnabled = true;

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
                Delete.IsEnabled = false;
            }
        }

        private void textBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            // My code goes here
        }

        private void AddNew_Click(object sender, RoutedEventArgs e)
        {
            AddNew Add = new AddNew(vme);
            Add.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var tmp = Context.CurrentStates.Include(cs => cs.Product).First(n => n.ID == 1).Product.First(n => n.Code == pcopy.Code);
            Context.CurrentStates.Include(cs => cs.Product).First(n => n.ID == 1).Product.Remove(tmp);

            Context.SaveChanges();
            Refresh();

            Delete.IsEnabled = false;
        }

        private void AddNewGlobal_Click(object sender, RoutedEventArgs e)
        {
            AddNewGlobal Add = new AddNewGlobal();
            Add.ShowDialog();
        }

        private void Refresh()
        {
            listBoxSupplies.ItemsSource = (from P in Context.CurrentStates
            .Include(cs => cs.Product)
            .First(n => n.Location == ((App)Application.Current).Location).Product
                                           join I in Context.Info on P.Code equals I.Code
                                           where P.Code == I.Code
                                           select new CurrentProduct { Code = P.Code, Name = I.Name, Amount = P.Amount, Price = I.SellingPrice }
            ).ToList();

            listBoxBanknotesCoins.ItemsSource = Context.CurrentStates
                    .Include(cs => cs.Banknotes_n_Coins)
                    .First(n => n.Location == ((App)Application.Current).Location)
                    .Banknotes_n_Coins;
        }

        private void TaskA_Click(object sender, RoutedEventArgs e)
        {
            Task_A A = new Task_A();
            A.ShowDialog();
        }
    }
}
