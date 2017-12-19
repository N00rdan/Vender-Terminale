using System;
using System.Windows;
using Vending_Terminal_Software_Classes;
using Vending_Terminal_Software_UI.PagesFolder;

namespace Vending_Terminal_Software_UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            frameMain.Navigate(Pages.VM);
        }
    }
}
