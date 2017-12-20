using System.IO;
using System.Linq;
using System.Windows;
using Vending_Terminal_Software_Classes;

namespace Vending_Terminal_Software_UI.PagesFolder
{
    /// <summary>
    /// Логика взаимодействия для SelectVM.xaml
    /// </summary>
    public partial class SelectVM : System.Windows.Controls.Page
    {
        public SelectVM()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var Context = new Context())
            {
                if (Context.CurrentStates.ToList().Count == 0)
                {
                    var tmp = new DataBase();
                    tmp.BasicState();
                }

                Locations.ItemsSource = Context.CurrentStates.ToList();
            }
        }

        private void Locations_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (Locations.SelectedItem is CurrentState C)
            {
                ((App)Application.Current).Location = C.Location;

                NavigationService.Navigate(Pages.VM);
            }
        }
    }
}
