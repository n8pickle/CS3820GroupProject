using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using GroupProject;
using GroupProject.Model;

namespace GroupProject.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        /// <summary>
        /// This variable will be used as the holder for the value selected by the user, 
        /// By default is an empty string (""), which will indicate no ID was selected
        /// Main window will access it with the window object it has for the search window
        /// Access like [wndSearch Object].SelectedID
        /// </summary>
        public string SelectedID { get; set; }

        /// <summary>
        /// This is the class object that will handle all business logic for this window
        /// </summary>
        clsSearchLogic log = new clsSearchLogic();

        /// <summary>
        /// This will initialize this window and handle all the initial bindings
        /// </summary>
        public wndSearch()
        {
            try
            {

                InitializeComponent();
                //Set the ID to an empty string, If the user selects no record, the empty string will indicate that to the
                // main window
                SelectedID = "";

                //This will return all the invoices without filter, as well as initialize all 3 lists
                Invoicedg.ItemsSource = log.GetInvoices(InvNumCmb.SelectedIndex, InvDateCmb.SelectedIndex, TotalsCmb.SelectedIndex);

                // This will bind the combo boxes to the 3 observable collections
                InvNumCmb.ItemsSource = log.invoiceNums;
                InvDateCmb.ItemsSource = log.invoiceDates;
                TotalsCmb.ItemsSource = log.invoiceTotals;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                   MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }


        /// <summary>
        /// This will handle of any of the Comboboxes are changed, they will all call GetInvoices() which will change the observable
        /// collection initially binded to them in the Window Initializer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                log.GetInvoices(InvNumCmb.SelectedIndex, InvDateCmb.SelectedIndex, TotalsCmb.SelectedIndex);

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                   MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This won't do anything to the UI elements, they're bound to observable collections, this will simply reset all observable collections
        /// to their initial state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                log.resetLogic();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                      MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This will handle if the Select Invoice button is clicked, it will set the class variable for this window to be the 
        /// Invoice ID of the selected record. After setting that variable, the window shall close itself
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                // Check if the user actually selected an invoice, if not, don't do anything
                if (Invoicedg.SelectedIndex == -1)
                {
                    errorLbl.Content = "No Record Selected, Please Select A Record.";
                    return;
                }

                errorLbl.Content = " ";

                //get the id of the selected record and set it to the class variable
                SelectedID = log.finalSelection(Invoicedg.SelectedIndex);

                errorLbl.Content = SelectedID.ToString();

                this.Close();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                      MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// Return to the Main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Error Handling for all top level methods in this class
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                //Show a message box with error info
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
    }
}
