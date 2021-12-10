using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GroupProject.Model;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GroupProject;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace GroupProject.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// ItemViewModel for bonding to the view
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; set; }

        /// <summary>
        /// List of items from the ItemViewModel
        /// </summary>
        List<ItemViewModel> items;

        /// <summary>
        /// An item onchanged handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Selected item on the page
        /// </summary>
        private ItemViewModel _selectedItem;
        public ItemViewModel SelectedItem 
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            } 
        }

        /// <summary>
        /// Constructor for other windows to pass info into the window
        /// </summary>
        /// <param name="items"></param>
        public wndItems()
        {
            items = new List<ItemViewModel>();
            Items = new ObservableCollection<ItemViewModel>(items);
            DataContext = this;
            InitializeComponent();
        }

        /// <summary>
        /// Changes the selected item depending on what Row is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItem = ItemsGrid.SelectedItem as ItemViewModel;
        }
    }
}
