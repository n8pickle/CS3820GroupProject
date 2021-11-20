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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

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

        public wndItems(List<ItemViewModel> items)
        {
            Items = new ObservableCollection<ItemViewModel>(items);
            DataContext = this;
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItem = ItemsGrid.SelectedItem as ItemViewModel;
        }
    }
}
