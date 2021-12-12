using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using GroupProject.Items;
using GroupProject.Main;
using GroupProject.Model;
using GroupProject.Search;

namespace GroupProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Item window object to create the window
        /// </summary>
        wndItems itemsWindow;
        /// <summary>
        /// Search window object to create the window.
        /// </summary>
        wndSearch searchWindow;

        /// <summary>
        /// Main Logic object
        /// </summary>
        clsMainLogic clsMainLogic;

        /// <summary>
        /// bool for editing invoice
        /// </summary>
        bool isEditing;

        /// <summary>
        /// bool for creating a new invoice
        /// </summary>
        bool isCreatingNewInvoice;

        /// <summary>
        /// bool for if an item is currently being added
        /// </summary>
        bool isAddingItem;

        /// <summary>
        /// InvoiceNum from main window.
        /// </summary>
        public string InvoiceNum;

        /// <summary>
        /// string to keep track of the current itemCode (Code)
        /// </summary>
        string itemCode;

        /// <summary>
        /// string to keep track of the current lineItemNum
        /// </summary>
        string lineItemNum;

        /// <summary>
        /// Generate an ID for new Invoices
        /// </summary>
        private string newID;

        /// <summary>
        /// Create a date for a new Invoice
        /// </summary>
        public string InvoiceDate;

        /// <summary>
        /// ItemDesc column in datagrid
        /// </summary>
        public string ItemDesc;

        /// <summary>
        /// ItemPrice column in datagrid
        /// </summary>
        public string ItemPrice;

        /// <summary>
        /// Current Selected Item
        /// </summary>
        public string curItem;

        /// <summary>
        /// Current Selected Added Item
        /// </summary>
        public string addedItem;

        /// <summary>
        /// List of Added items
        /// </summary>
        public List<LineItemDisplayContainer> addedItemsList = new List<LineItemDisplayContainer>();

        /// <summary>
        /// List of Added items
        /// </summary>
        public List<ItemViewModel> addeditems = new List<ItemViewModel>();

        /// <summary>
        /// Running Total of Added Items
        /// </summary>
        double total = 0;

        /// <summary>
        /// Constructor for Main Window
        /// </summary>
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

                clsMainLogic = new clsMainLogic();
                cmbInvoiceItem.ItemsSource = clsMainLogic.getItems().Select(a => a.Description);

                List<LineItemDisplayContainer> item = clsMainLogic.getAllLineItems();
                dgInvoice.ItemsSource = item;

                btnEdit.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnAddItem.IsEnabled = false;
                btnRemoveItem.IsEnabled = false;
                btnSave.IsEnabled = false;
                cmbInvoiceItem.IsEnabled = false;
                isAddingItem = false;
                dpInvoiceDate.IsEnabled = false;
                isEditing = false;
                isCreatingNewInvoice = false;

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// handles when the edit items menu item is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditItemsMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isEditing && !isCreatingNewInvoice)
                {
                    itemsWindow = new wndItems();

                    this.Hide();

                    itemsWindow.ShowDialog();

                    this.Show();

                    cmbInvoiceItem.ClearValue(ItemsControl.ItemsSourceProperty);
                    cmbInvoiceItem.ItemsSource = clsMainLogic.getItems().Select(a => a.Description);
                }
                else
                {
                    MessageBox.Show("Must not be editing or making new invoice.");
                }

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// handles when the search menu item is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchItems_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                searchWindow = new wndSearch();

                this.Hide();

                searchWindow.ShowDialog();
                int num;
                string selectedID = searchWindow.SelectedID;


                int i = 0;
                if (selectedID != "")
                {
                    num = Convert.ToInt32(selectedID);
                    foreach (var item in dgInvoice.Items)
                    {
                        LineItemDisplayContainer selectedItem = (LineItemDisplayContainer)item;
                        if (selectedItem.InvoiceNum == num)
                        {
                            break;
                        }
                        i++;
                    }

                    dgInvoice.SelectedIndex = i;
                }
                this.Show();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Tracks selection in the InvoiceItem combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbInvoiceItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBox item = (ComboBox)sender;
                curItem = item.ToString();

                if (!isAddingItem)
                {
                    string value = cmbInvoiceItem.SelectedItem.ToString();

                    string curItemDesc = clsMainLogic.getItemCode(value);
                    double curItemCost = clsMainLogic.getItemCost(curItemDesc);
                    txtCost.Text = curItemCost.ToString();
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// Tracks selected item in dgInvoice datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgInvoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                LineItemDisplayContainer invoice = (LineItemDisplayContainer)dgInvoice.SelectedItem;

                if (invoice != null)
                {
                    btnEdit.IsEnabled = true;
                    btnDelete.IsEnabled = true;
                    btnNew.IsEnabled = true;

                    txtInvoiceNum.Text = invoice.InvoiceNum.ToString();
                    InvoiceNum = invoice.InvoiceNum.ToString();
                    dpInvoiceDate.Text = invoice.InvoiceDate;
                    txtCost.Text = invoice.ItemPrice;

                    txtTotalCost.Text = "$ " + String.Format("{0:N2}", invoice.TotalCost.ToString());
                }
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// Refreshes the items in the window
        /// </summary>
        internal void RefreshWindow()
        {
            try
            {
                dgInvoice.ItemsSource = null;
                dgInvoice.ItemsSource = clsMainLogic.GetAllInvoices();
                cmbInvoiceItem.ItemsSource = null;
                cmbInvoiceItem.ItemsSource = clsMainLogic.getItems().Select(a => a.Description);
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Handles when the New button is clicked. Creates a new invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                isEditing = false;
                isCreatingNewInvoice = true;
                isAddingItem = false;
                txtInvoiceNum.IsEnabled = true;
                dpInvoiceDate.IsEnabled = true;
                txtTotalCost.IsEnabled = true;
                cmbInvoiceItem.IsEnabled = true;
                btnAddItem.IsEnabled = true;
                btnSave.IsEnabled = false;
                btnEdit.IsEnabled = false;
                btnDelete.IsEnabled = false;

                dpInvoiceDate.SelectedDate = DateTime.Today;
                clsMainLogic.SaveInvoice(dpInvoiceDate.SelectedDate.Value.Date.ToShortDateString(), "0");

                newID = clsMainLogic.GenerateInvoiceID();

                txtInvoiceNum.Text = newID;

                dgInvoice.ClearValue(ItemsControl.ItemsSourceProperty);

                InvoiceNum = newID;

                total = 0;
                txtInvoiceNum.Text = "TBD";

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// handles when the Save button is clicked. Saves the invoice record.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string total = this.total.ToString();
                clsMainLogic.UpdateInvoiceTotal(InvoiceNum, total);
                clsMainLogic.InsertLineItem(InvoiceNum, lineItemNum, itemCode);

                dgInvoice.ClearValue(ItemsControl.ItemsSourceProperty);

                List<LineItemDisplayContainer> item = clsMainLogic.getAllLineItems();
                dgInvoice.ItemsSource = item;

                txtInvoiceNum.Text = "TBD";
                dpInvoiceDate.SelectedDate = null;
                txtTotalCost.Text = null;

                addedItemsList = new List<LineItemDisplayContainer>();

                txtInvoiceNum.IsEnabled = false;
                dpInvoiceDate.IsEnabled = false;
                txtTotalCost.IsEnabled = false;
                cmbInvoiceItem.IsEnabled = false;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnNew.IsEnabled = true;
                btnSave.IsEnabled = false;
                isCreatingNewInvoice = false;
                isEditing = false;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// handles when the Edit button is clicked. Edits an invoice record.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                isEditing = true;
                btnNew.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnAddItem.IsEnabled = true;
                btnRemoveItem.IsEnabled = true;
                btnSave.IsEnabled = true;
                txtInvoiceNum.IsEnabled = true;
                dpInvoiceDate.IsEnabled = true;
                cmbInvoiceItem.IsEnabled = true;
                btnDelete.IsEnabled = false;

                List<LineItemsModel> i = clsMainLogic.getInvoiceItems(InvoiceNum);
                List<String> code = i.Select(a => a.Code).ToList();

                List<LineItemDisplayContainer> temp = new List<LineItemDisplayContainer>();

                foreach (var item in code)
                {
                    temp = clsMainLogic.GetItemsByCode(item);

                }
                dgInvoice.ClearValue(ItemsControl.ItemsSourceProperty);
                dgInvoice.ItemsSource = temp;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// handles when the Delete button is clicked. Deletes an invoice record.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                LineItemDisplayContainer selectedItem = (LineItemDisplayContainer)dgInvoice.SelectedItem;
                string invoiceNum = selectedItem.InvoiceNum.ToString();

                clsMainLogic.DeleteLineItems(invoiceNum);
                clsMainLogic.DeleteInvoice(invoiceNum);

                dgInvoice.ClearValue(ItemsControl.ItemsSourceProperty);

                List<LineItemDisplayContainer> refresh = clsMainLogic.getAllLineItems();
                dgInvoice.ItemsSource = refresh;

                btnDelete.IsEnabled = false;
                btnEdit.IsEnabled = false;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// handles when the Remove button is clicked. Removes an item from editing/creating mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // TODO: FIX THIS METHOD

                //string itemDesc = cmbInvoiceItem.SelectedItem.ToString(

                LineItemDisplayContainer invoice = (LineItemDisplayContainer)dgInvoice.SelectedItem;



                /*
                LineItemDisplayContainer itemInfo = clsMainLogic.getItemInfo(itemDesc);

                addedItemsList.Add(itemInfo);

                string itemCode = clsMainLogic.getItemCode(itemDesc);
                */


                if (invoice != null)
                {
                    string itemDesc = invoice.ItemDesc;
                    string itemCode = invoice.Code.ToString();

                    double cost;
                    cost = Convert.ToDouble(invoice.ItemPrice);
                    total -= cost;

                    addedItemsList.Remove(invoice);

                    if (!isCreatingNewInvoice)
                    {
                        clsMainLogic.DeleteItemFromInvoice(InvoiceNum, itemCode);
                    }

                    dgInvoice.ClearValue(ItemsControl.ItemsSourceProperty);
                    dgInvoice.ItemsSource = addedItemsList;
                    
                    txtTotalCost.Text = "$ " + String.Format("{0:N2}", total);
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// handles when the Add button is clicked. Adds an item to the invoice record, but doens't save it to the add.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddItem_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                isAddingItem = true;
                string itemDesc = cmbInvoiceItem.SelectedItem.ToString();

                LineItemDisplayContainer itemInfo = clsMainLogic.getItemInfo(itemDesc);

                addedItemsList.Add(itemInfo);

                itemCode = clsMainLogic.getItemCode(itemDesc);

                double cost;
                cost = clsMainLogic.getItemCost(itemCode);
                total += cost;

                lineItemNum = clsMainLogic.GenerateLineItemNum(InvoiceNum);
                if (lineItemNum.Equals("0") || lineItemNum.Equals(null))
                {
                    lineItemNum = "1";
                }

                dgInvoice.ClearValue(ItemsControl.ItemsSourceProperty);
                dgInvoice.ItemsSource = addedItemsList;

                cmbInvoiceItem.SelectedIndex = -1;
                btnSave.IsEnabled = true;
                txtTotalCost.Text = "$ " + String.Format("{0:N2}", total);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// exception handler that shows the error
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
    }
}
