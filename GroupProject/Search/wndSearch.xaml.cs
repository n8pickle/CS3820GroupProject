using System;
using System.Collections.Generic;
using System.Data;
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
using GroupProject;

namespace GroupProject.Search
{
     /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        //This variable will be used as the holder for the value selected by the user
        // Main window will access it with the window object it has for the search window
        // Like wndSearch ser = new wndSearch()
        // ...
        // (After search closes) > ser.SelectedID
        // That will access this field
        public string SelectedID { get; set; } 

        public wndSearch()
        {
            InitializeComponent();
            SelectedID = "";
        }

        private void InvNumCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void InvDateCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelectBtn_Click(object sender, RoutedEventArgs e)
        {
            //The Value of SelectedID will be set here. The main window can acces it through the object it has for this class



        }
    }
}
