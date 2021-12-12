﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using GroupProject.Search;

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
        #region Variables
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
        /// Main SQL object
        /// </summary>
        clsMainSQL sql;

        /// <summary>
        /// flag for editing invoice
        /// </summary>
        bool isEditing;

        /// <summary>
        /// InvoiceNum from main window.
        /// </summary>
        public string InvoiceNum;


        /// <summary>
        /// Generate an ID for new Invoices
        /// </summary>
        private string newID;

        /// <summary>
        /// Create a date for a new Invoice
        /// </summary>
        public string InvoiceDate;

        public string ItemDesc;

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
        public List<ItemViewModel> addeditems = new List<ItemViewModel>();
        /// <summary>
        /// Running Total of Added Items
        /// </summary>
        double total = 0;
        #endregion

        #region Constructor
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
                //Populate Items ComboBox
                cmbInvoiceItem.ItemsSource = clsMainLogic.getItems().Select(a => a.Description);

                //Populate DataGrid with Invoices
                //List<InvoiceModel> invoice = clsMainLogic.GetAllInvoices();
                //dgInvoice.ItemsSource = invoice;

                List<LineItemDisplayContainer> item = clsMainLogic.getAllLineItems();
                dgInvoice.ItemsSource = item;

                btnEdit.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion

        #region Control Menu

        /// <summary>
        /// handles control menu clicks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ctrlItems.IsChecked == true)
                {
                    itemsWindow = new wndItems();

                    this.Hide();

                    itemsWindow.ShowDialog();

                    //Update all dropdowns to reflect any changes (dropdown.itemsSource =)

                    this.Show();

                    cmbInvoiceItem.ClearValue(ItemsControl.ItemsSourceProperty);
                    cmbInvoiceItem.ItemsSource = clsMainLogic.getItems().Select(a => a.Description);
                }
                if (ctrlSearch.IsChecked == true)
                {
                    searchWindow = new wndSearch();

                    this.Hide();

                    searchWindow.ShowDialog();
                    int num;
                    string selectedID = searchWindow.SelectedID;
                   

                    //find the invoice in the datagrid
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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        #endregion

        #region Buttons
        
        /// <summary>
        /// Edit an existing Invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                isEditing = true;
                btnNew.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnAddItem.IsEnabled = true;
                btnSave.IsEnabled = true;
                //btnDeleteLine.Visibility = Visibility.Visible;


                /*
                //Enable Buttons
                txtInvoiceNum.IsEnabled = true;
                dpInvoiceDate.IsEnabled = true;
                txtTotalCost.IsEnabled = true;
                cmbInvoiceItem.IsEnabled = true;
                //cmbxItemsAdded.IsEnabled = true;

                //Enable Save button
                btnSave.IsEnabled = true;

                //disable edit and delete buttons
                btnEdit.IsEnabled = false;
                btnDelete.IsEnabled = false;

                //get items from line item 
                List<LineItemsModel> i = ml.getInvoiceItems(InvoiceNum);
                List<String> code = i.Select(a => a.ItemCode).ToList();

                List<ItemViewModel> temp = new List<ItemViewModel>();

                foreach (var item in code)
                {
                    temp = ml.GetItemsByCode(item);

                }
                List<String> desc = new List<String>();
                desc = temp.Select(a => a.Description).ToList();
                addeditems.AddRange(desc);

                //cmbxItemsAdded.ItemsSource = addeditems;
                */
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        internal void RefreshWindow()
        {
            dgInvoice.ItemsSource = null;
            dgInvoice.ItemsSource = clsMainLogic.GetAllInvoices();
            cmbInvoiceItem.ItemsSource = null;
            cmbInvoiceItem.ItemsSource = clsMainLogic.getItems().Select(a => a.Description);
        }

        /// <summary>
        /// This will delete an existing Invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LineItemDisplayContainer selectedItem = (LineItemDisplayContainer)dgInvoice.SelectedItem;
                string invoiceNum = selectedItem.InvoiceNum.ToString();

                clsMainLogic.DeleteLineItems(invoiceNum);
                clsMainLogic.DeleteInvoice(invoiceNum);

                dgInvoice.ClearValue(ItemsControl.ItemsSourceProperty);

                //Populate DataGrid with Invoices
                List<InvoiceModel> refresh = clsMainLogic.GetAllInvoices();
                dgInvoice.ItemsSource = refresh;
                //disable Delete and Edit Buttons
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
        /// On Click Save button will submit Total to Invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // TODO: if !isEditing
                string t = total.ToString();
                clsMainLogic.UpdateInvoiceTotal(InvoiceNum, t);

                //clear old data
                dgInvoice.ClearValue(ItemsControl.ItemsSourceProperty);

                //refresh the items in the data grid
                List<InvoiceModel> invoice = clsMainLogic.GetAllInvoices();
                dgInvoice.ItemsSource = invoice;

                txtInvoiceNum.Text = "TBD";
                dpInvoiceDate.SelectedDate = null;
                txtTotalCost.Text = null;

                //cmbxItemsAdded.ClearValue(ItemsControl.ItemsSourceProperty);

                txtInvoiceNum.IsEnabled = false;
                dpInvoiceDate.IsEnabled = false;
                txtTotalCost.IsEnabled = false;
                cmbInvoiceItem.IsEnabled = false;
                //cmbxItemsAdded.IsEnabled = false;
                //Enable Save button
                btnSave.IsEnabled = false;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }
        /// <summary>
        /// Adds a new item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Check to make sure item has been selected
                //if (curItem.Equals(" ") || curItem.Equals(null)) return;
                //Create variable for Item Description
                //string itemDesc = cmbInvoiceItem.SelectedItem.ToString();
                //Add Item to List of Invoice Added Items
                //addeditems.Add(itemDesc);


                /* TODO: WHITNEY */
                addeditems = clsMainLogic.getItems();
                ItemViewModel item;

                if (dpInvoiceDate.Text == "")
                {
                    MessageBox.Show("Please select a date.");
                    return;
                }
                if (cmbInvoiceItem.SelectedItem == null)
                {
                    MessageBox.Show("Please select an item.");
                    return;
                }

                for (int i = 0; i < addeditems.Count; i++)
                {
                    if (cmbInvoiceItem.SelectedIndex == i)
                    {
                        item = addeditems[i];
                        total += item.Price;
                        txtTotalCost.Text = "$ " + String.Format("{0:N2}", total);
                        dgInvoice.Items.Add(item);
                    }
                }


                /*

                //Get Item Code from Description
                string itemCode = ml.getItemCode(itemDesc);

                //Add cost of Item to Total
                double cost;
                cost = ml.getItemCost(itemCode);
                total += cost;

                //Get Line Item Number
                string lineItemNum = ml.GenerateLineItemNum(InvoiceNum);
                //Check to make sure it is not 0
                if (lineItemNum.Equals("0") || lineItemNum.Equals(null))
                {
                    lineItemNum = "1";
                }
                //Insert Item into Line Item DB
                ml.InsertLineItem(InvoiceNum, lineItemNum, itemCode);
                //Clear Combo Box and Reload
                //cmbxItemsAdded.ClearValue(ItemsControl.ItemsSourceProperty);
                //cmbxItemsAdded.ItemsSource = addeditems;
                dgInvoice.Items.Add()
                //Clear Selected Item
                cmbInvoiceItem.SelectedIndex = -1;
                //Change Add Item Button IsEnabled to false
                btnAddItem.IsEnabled = false;

                //Change Total
                txtTotalCost.Text = "$ " + String.Format("{0:N2}", total);
                */
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }
        /// <summary>
        /// Removes Items from Invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /*
                //Check to make sure item is selected
                if (addedItem.Equals(" ") || addedItem.Equals(null)) return;
                string itemDesc = cmbxItemsAdded.SelectedItem.ToString();
                //Remove item
                addeditems.Remove(itemDesc);

                string itemCode = ml.getItemCode(itemDesc);
                //Subtract removed item from total
                double cost;
                cost = ml.getItemCost(itemCode);
                total -= cost;
                //Remove Item from Invoice
                ml.DeleteItemFromInvoice(InvoiceNum, itemCode);
                //Clear ComboBox Added Items
                //cmbxItemsAdded.ClearValue(ItemsControl.ItemsSourceProperty);
                //Reload Added Items to ComboBox
                //cmbxItemsAdded.ItemsSource = addeditems;
                //Clear Selected
                //cmbxItemsAdded.SelectedIndex = -1;
                //Disable Remove Button
                btnRemoveItem.IsEnabled = false;

                //Change Total
                txtTotalCost.Text = "$ " + String.Format("{0:N2}", total);
                */
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }
        #endregion

        #region Combo Box
        /// <summary>
        /// Invoice Item on Selection loads curItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbInvoiceItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //Take Selected Item and instantiate it into Current Item variable
                ComboBox item = (ComboBox)sender;
                curItem = item.ToString();
                //Enable Add Item to Invoice Button
                btnAddItem.IsEnabled = true;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Combo Box Items in Invoice loads addedItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbxItemsAdded_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //Take Selected Item and instantiate it into Added Item variable
                ComboBox item = (ComboBox)sender;
                addedItem = item.ToString();
                //Enable Remove Item from Invoice Button
                btnRemoveItem.IsEnabled = true;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        #endregion

        #region Data Grid
        /// <summary>
        /// Data Grid on Selected Invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgInvoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //ItemViewModel item = (ItemViewModel)dgInvoice.SelectedItem;
                //string selectedCode = item.Code; // don't need this?
                //int index = dgInvoice.SelectedIndex + 1;
                //lblSelected.Content = "Item: " + item.description + " Line " + index;

                LineItemDisplayContainer invoice = (LineItemDisplayContainer)dgInvoice.SelectedItem;
                //clsItems items = mainLogic.
                
                if (invoice != null)
                {
                    btnEdit.IsEnabled = true;
                    btnDelete.IsEnabled = true;

                    txtInvoiceNum.Text = invoice.InvoiceNum.ToString();
                    InvoiceNum = invoice.InvoiceNum.ToString();
                    dpInvoiceDate.Text = invoice.InvoiceDate;
                    txtTotalCost.Text = "$ " + String.Format("{0:N2}", invoice.TotalCost.ToString());
                    //total = invoice.TotalCost;
                }
                

            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion

        #region Error Handling
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





        #endregion

        private void btnNew_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                //Enable textboxes
                isEditing = false;
                txtInvoiceNum.IsEnabled = true;
                dpInvoiceDate.IsEnabled = true;
                txtTotalCost.IsEnabled = true;
                cmbInvoiceItem.IsEnabled = true;
                btnAddItem.IsEnabled = true;
                btnSave.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;

                //Insert Current Date
                dpInvoiceDate.SelectedDate = DateTime.Today;
                //clsMainLogic.SaveInvoice(dpInvoiceDate.SelectedDate.Value.Date.ToShortDateString(), "0");

                //Generate a new Invoice ID
                newID = clsMainLogic.GenerateInvoiceID();

                //Insert Invoice ID onto Page
                txtInvoiceNum.Text = newID;

                //Set variable to Current InvoiceNum
                InvoiceNum = newID;

                dgInvoice.Items.Clear();
                total = 0;
                txtInvoiceNum.Text = "TBD";

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}
