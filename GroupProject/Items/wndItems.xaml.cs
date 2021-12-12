using System.Collections.Generic;
using System.Windows;
using GroupProject.Model;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System;
using System.Linq;

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
        /// MainWindow used to refresh main window after action
        /// </summary>
        public MainWindow Main;

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
            if (PropertyChanged != null)
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

        private clsItemsLogic _logic;

        /// <summary>
        /// Constructor for other windows to pass info into the window
        /// </summary>
        /// <param name="items"></param>
        public wndItems()
        {
            _logic = new clsItemsLogic();
            Main = (MainWindow)Application.Current.MainWindow;
            items = _logic.GetItemViewModels();
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

        /// <summary>
        /// Onclick method to add an item to the DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ItemDescritpionText.Text) && !string.IsNullOrEmpty(ItemPriceText.Text))
                {
                    string desc = ItemDescritpionText.Text;
                    double.TryParse(ItemPriceText.Text, out double price);
                    _logic.AddItemToDb(desc, price);
                    Items = new ObservableCollection<ItemViewModel>(_logic.GetItemViewModels());
                    ItemsGrid.ItemsSource = null;
                    ItemsGrid.ItemsSource = Items;
                    Main.RefreshWindow();
                }
                else
                {
                    MessageBox.Show("The item description and price need to be set before creation");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Onclick Method to removed an item from the DB if it isn't in any of the Invoices
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedItem != null)
                {
                    var numReturned = _logic.GetInvoiceByItemCode(SelectedItem.Code);
                    if (numReturned.Count == 0)
                    {
                        _logic.DeleteItemFromDb(SelectedItem.Code);
                        Items = new ObservableCollection<ItemViewModel>(_logic.GetItemViewModels());
                        ItemsGrid.ItemsSource = null;
                        ItemsGrid.ItemsSource = Items;
                        Main.RefreshWindow();
                    }
                    else
                    {
                        string invoices = GetInvoiceString(numReturned);
                        MessageBox.Show($"The selected item can't be deleted because it is in the following invoices: {invoices}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets as string of all the invoices comma seperated
        /// </summary>
        /// <param name="numReturned"></param>
        /// <returns></returns>
        private string GetInvoiceString(List<int> numReturned)
        {
            string result = $"{numReturned.First()}";
            int[] arr = numReturned.ToArray();
            for (int i = 1; i < arr.Length; i++)
            {
                result += $", {arr[i]}";
            }
            return result;
        }

        /// <summary>
        /// Method to update the selected item in the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedItem != null)
                {
                    string desc = ItemDescritpionText.Text;
                    double.TryParse(ItemPriceText.Text, out double price);
                    _logic.UpdateItemInDb(SelectedItem.Code, desc, price);
                    Items = new ObservableCollection<ItemViewModel>(_logic.GetItemViewModels());
                    ItemsGrid.ItemsSource = null;
                    ItemsGrid.ItemsSource = Items;
                    Main.RefreshWindow();
                }
                else
                {
                    MessageBox.Show("No Item is selected. Please select an item to update");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to close the window to edit items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
