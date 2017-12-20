using System.Linq;
using System.Windows;
using Vending_Terminal_Software_Classes;
using System.Data.Entity;

namespace Vending_Terminal_Software_UI.Windows.Linq
{
    /// <summary>
    /// Логика взаимодействия для Task_B.xaml
    /// </summary>
    public partial class Task_B : Window
    {
        public Task_B()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var Context = new Context())
            {
                //var querry = Context.CurrentStates.Include(cs => cs.Product)
                //    .Join()
                //    .Where(n => n.Sales.Count > 0)
                //    .OrderByDescending(t => t.Product.Count(m => m.Amount > 0)).ToList();

                //TaskB.ItemsSource = querry;

                //Context.CurrentStates.Include(cs => cs.Product)
                //        .Where(n => n.Product.Count(m => m.Amount == 0) > 0)
                //        .OrderByDescending(t => t.Product.Count(m => m.Amount > 0)).ToList();
            }
        }
    }
}
