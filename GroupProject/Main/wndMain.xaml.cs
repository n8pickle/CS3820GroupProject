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
using GroupProject;
using GroupProject.Items;
using GroupProject.Main;
using GroupProject.Model;

namespace GroupProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // The main window should allow the user to create new invoices, edit existing invoices, or delete existing invoices.
        // There is a Menu control box in the top left corner to navigate to the search and items pages.
        // There are buttons that will connect the logic and SQL classes for the main window.
        // Button click events are passed to the logic which will execute the SQL statement.
        // The items page will populate the combo box with items.
        // Refresh items in combo box when window visibility changes.
        public List<ItemViewModel> ItemList;
        public MainWindow()
        {
            clsMainLogic logic = new clsMainLogic();
            ItemList = logic.GetItemViewModels();
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgInvoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void ctrlEditItem_Click(object sender, RoutedEventArgs e)
        {
            wndItems window = new wndItems(ItemList);
            window.Show();
        }
    }
}
