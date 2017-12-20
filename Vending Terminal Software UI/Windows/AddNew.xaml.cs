using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity;
using Vending_Terminal_Software_Classes;

namespace Vending_Terminal_Software_UI
{
    /// <summary>
    /// Логика взаимодействия для AddNew.xaml
    /// </summary>
    public partial class AddNew : Window
    {
        CurrentProduct pCopy;
        List<CurrentProduct> ExistingProducts;
        Vending_Machine_Emulator vme;

        public AddNew(Vending_Machine_Emulator VME)
        {
            InitializeComponent();

            vme = VME;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var Context = new Context())
                {
                    Context.CurrentStates.Include(cs => cs.Product).First(n => n.Location == ((App)Application.Current).Location).Product.Add(new Product { Code = pCopy.Code, Amount = int.Parse(Amount.Text) });
                    Context.SaveChanges();
                }
                
                vme.SR();
            }
            catch { MessageBox.Show("Please, check your input"); }

            Amount.Text = "0";

            Update();
        }

        private void InfoSelected(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ExistingNames.SelectedItem is CurrentProduct P)
            {
                pCopy = P;
            }
        }

        private void Update()
        {
            using (var Context = new Context())
            {
                ExistingProducts = (from I in Context.Info
                                    select new CurrentProduct { Code = I.Code, Name = I.Name }
                    ).ToList();

                var Tmp = (from P in Context.CurrentStates.Include(cs => cs.Product).First(n => n.Location == ((App)Application.Current).Location).Product
                           join I in Context.Info on P.Code equals I.Code
                           select new CurrentProduct { Code = P.Code, Name = I.Name }
                    ).ToList();

                ExistingProducts = ExistingProducts.Except(Tmp, new ProductComparer()).ToList();
            }

            ExistingNames.ItemsSource = ExistingProducts;
        }
    }
}
