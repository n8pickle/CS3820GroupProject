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
using GroupProject.Model;

namespace GroupProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

/*        private void GoToEditItems_Click(object sender, RoutedEventArgs e)
        {
            List<ItemViewModel> items = new List<ItemViewModel>
            {
                new ItemViewModel
                {
                    Code = "A",
                    Name = "Diamond Bracelet",
                    Price = 499.99,
                    Description = "The perfect gift for your GF!"
                }
            };
            wndItems windowItems = new wndItems(items);
            windowItems.Show();
        }*/
    }
}
