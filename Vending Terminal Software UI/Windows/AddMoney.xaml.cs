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
using Vending_Terminal_Software_Classes;

namespace Vending_Terminal_Software_UI
{
    /// <summary>
    /// Логика взаимодействия для AddMoney.xaml
    /// </summary>
    public partial class AddMoney : Window
    {
        Vending_Machine_Emulator vme;

        public AddMoney(Vending_Machine_Emulator VME)
        {
            InitializeComponent();

            vme = VME;

            Refresh();
        }

        private void Money_Click(object sender, RoutedEventArgs e)
        {
            var tmp = sender as Button;
            var tag = Convert.ToInt32(tmp.Tag);

            if (vme.ChangeCount(tag) == true)
                vme.MoneyAdd(tag);
            else
                MessageBox.Show("В автомате не хватает сдачи,\nВаши деньги возвращены");

            Refresh();
        }

        private void Refresh()
        {
            Current.Text = vme.Data.CurrentState.Money.ToString();
        }
    }
}
