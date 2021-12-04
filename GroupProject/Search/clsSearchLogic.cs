using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GroupProject;

namespace GroupProject.Search
{
    /// <summary>
    /// Holds information for a particular item in an invoice
    /// </summary>
    public class conItem {

        //Function to set the class variables for an item
        public conItem(string _code, string _desc, string _cost) {
            code = _code;
            desc = _desc;
            cost = "$" + _cost;
        }

        // Hold the variables associated with an item
        public string code { get; set; }
        public string desc { get; set; }
        public string cost { get; set; }
    }

    /// <summary>
    /// Class to hold the information associated with a single record (invoice), 
    /// as well as a collection of items associated with it
    /// </summary>
    public class invRecord {

        //Constructor for this class
        public invRecord(string passNum, string passDate, string passTotal) {
            Num = passNum;
            Date = passDate;
            Total = "$" + passTotal;
        }

        // Variables for information associated
        public string Num { get; set; }
        public string Date { get; set; }
        public string Total { get; set; }

        public List<conItem> conItems = new List<conItem>();
      
        // Gets or sets the list of connected items
        public List<conItem> ConnectedItems
        {
            get {
                return conItems;
            }
            set {
                conItems = value;
            }
        }
    }

    /// <summary>
    /// class to handle all logic for business class window
    /// </summary>
    public class clsSearchLogic
    {

        // class object to hold and construct all sql queries
        clsSearchSQL query = new clsSearchSQL();

        // class object to handle executing SQL code
        clsDataAccess db = new clsDataAccess();

        // dataset to hold what's returned from sql executions
        DataSet ds = new DataSet();

        // reference return value for SQL executions
        int ret = 0;

        // collection to hold invoice numbers
        public ObservableCollection<string> invoiceNums = new ObservableCollection<string>();

        // collection to hold invoice dates
        public ObservableCollection<string> invoiceDates = new ObservableCollection<string>();

        // collection to hold invoice totals
        public ObservableCollection<string> invoiceTotals = new ObservableCollection<string>();

        // collection to hold the actual invoice records
        public ObservableCollection<invRecord> Records = new ObservableCollection<invRecord>();

        /// <summary>
        ///  Function to update the invoice records and the collections of the 3 attributes associates
        ///  Filters through the selected indexes to see what should be used for a search and changes what is in the other search collections
        /// </summary>
        /// <param name="numIndex"> index for the invoice number from the UI </param>
        /// <param name="dateIndex">index for the invoice date from the UI</param>
        /// <param name="totalIndex">index for the invoice date from the UI</param>
        /// <returns></returns>
        public ObservableCollection<invRecord> GetInvoices(int numIndex, int dateIndex, int totalIndex) {
            try
            {
                // Clear the records collection so what we find isn't just appended
                Records.Clear();


                // filter by the number index first
                if (numIndex == -1)
                {
                    // filter by date next
                    if (dateIndex == -1) {
                        //filter by total
                        if (totalIndex == -1)
                        {
                            // nothing chosen, just get all of them
                            changeList(query.getAll());
                        }
                        else {
                            changeList(query.getFromTotalCharge(invoiceTotals[totalIndex]));
                        }
                    }
                    else {
                        if (totalIndex == -1)
                        {
                            changeList(query.getFromDate(invoiceDates[dateIndex]));
                        }
                        else
                        {
                            changeList(query.getFromDateAndCharge(invoiceDates[dateIndex], invoiceTotals[totalIndex]));
                        }
                    }
                }
                else {
                    if (dateIndex == -1)
                    {
                        if (totalIndex == -1)
                        {
                            changeList(query.getFromInvoiceNum(invoiceNums[numIndex]));
                        }
                        else
                        {
                            changeList(query.getfromNumAndCharge(invoiceNums[numIndex],invoiceTotals[totalIndex]));
                        }
                    }
                    else
                    {
                        if (totalIndex == -1)
                        {
                            changeList(query.getFromNumAndDate(invoiceNums[numIndex],invoiceDates[dateIndex]));
                        }
                        else
                        {
                            changeList(query.getFromAllThree(invoiceNums[numIndex], 
                                invoiceDates[dateIndex], invoiceTotals[totalIndex]));
                        }
                    }
                }


                // refine whats in the comboboxes for a dynamic search
                refineSearch(numIndex, dateIndex, totalIndex);

                // return the records gotten
                return Records;
            }
            catch (Exception ex) {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// reset the logic of the class, make it so that the collections of invoices and attributes are not filtered
        /// </summary>
        public void resetLogic() {
            try
            {
              

                GetInvoices(-1,-1,-1);

                ret = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// return the id of the record selected in the window
        /// </summary>
        /// <param name="index"> index in the list of invoice records </param>
        /// <returns></returns>
        public string finalSelection(int index)
        {
            try {
                return Records[index].Num;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// refine whats in the number, date, an total collections
        /// </summary>
        /// <param name="n">number index</param>
        /// <param name="d">date index</param>
        /// <param name="t"> total index</param>
        public void refineSearch(int n, int d, int t)
        {
            try {
                // change whats in the collection, but only if something hasn't already been selected for that collection
                if (n == -1) { getNums(d, t); }
                if (d == -1) { getDates(n, t); }
                if (t == -1) { getTotals(n, d); }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// used to filter the numbers collection using the indexes of the other 2 collections
        /// </summary>
        /// <param name="d"></param>
        /// <param name="t"></param>
        public void getNums(int d, int t) {
            try
            {
                // clear collection to avoid appending
                invoiceNums.Clear();
                ret = 0;

                if (t == -1 && d == -1)
                {
                    ds = db.ExecuteSQLStatement(query.getNumbers(), ref ret);
                }
                else if (t == -1 && d != -1)
                {
                    ds = db.ExecuteSQLStatement(query.getNumWithDate(invoiceDates[d]), ref ret);
                }
                else if (t != -1 && d == -1)
                {
                    ds = db.ExecuteSQLStatement(query.getNumWithCharge(invoiceTotals[t]), ref ret);
                   
                }
                else if (t != -1 && d != -1)
                {
                    ds = db.ExecuteSQLStatement(query.getNumWithChargeAndDate(invoiceTotals[t],invoiceDates[d]), ref ret);
                }
                

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //build the list of nums
                    invoiceNums.Add(ds.Tables[0].Rows[i][0].ToString());
                }

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Filter the list of dates using the indexes of the other two lists
        /// </summary>
        /// <param name="n"></param>
        /// <param name="t"></param>
        public void getDates(int n, int t)
        {
            try
            {
                // clear the list to avoide appending
                invoiceDates.Clear();
                ret = 0;
                
                if (n == -1 && t == -1)
                {
                    ds = db.ExecuteSQLStatement(query.getDates(), ref ret);
                }
                else if (n == -1 && t != -1)
                {
                    ds = db.ExecuteSQLStatement(query.getDatesWithCharge(invoiceTotals[t]), ref ret);
                }
                else if (n != -1 && t == -1)
                {
                    ds = db.ExecuteSQLStatement(query.getDatesWithNum(invoiceNums[n]), ref ret);
                }
                else if (n != -1 && t != -1)
                {
                    ds = db.ExecuteSQLStatement(query.getDatesWithNumAndCharge(invoiceNums[n],invoiceTotals[t]), ref ret);
                }

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //build the list of dates
                    invoiceDates.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// refine totals based on the indexes of the other collections
        /// </summary>
        /// <param name="n"></param>
        /// <param name="d"></param>
        public void getTotals(int n, int d)
        {
            try
            {
                invoiceTotals.Clear();
                ret = 0;
                if (n == -1 && d == -1) {
                    ds = db.ExecuteSQLStatement(query.getCharges(), ref ret);
                }
                else if (n == -1 && d != -1) {
                    ds = db.ExecuteSQLStatement(query.getChargeWithDate(invoiceDates[d]), ref ret);
                }
                else if (n != -1 && d == -1) {
                    ds = db.ExecuteSQLStatement(query.getChargeWithNum(invoiceNums[n]), ref ret);
                }
                else if (n != -1 && d != -1) {
                    ds = db.ExecuteSQLStatement(query.getChargeWithNumAndDate(invoiceNums[n],invoiceDates[d]), ref ret);
                }


                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //build the list of totals
                    invoiceTotals.Add(ds.Tables[0].Rows[i][0].ToString());
                }

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Build the list of invoice records
        /// </summary>
        /// <param name="q"> query to base the new collection off of</param>
        public void changeList(string q)
        {
            try
            {
                ret = 0;
                ds = db.ExecuteSQLStatement(q,ref ret);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //build the list of Records
                    Records.Add(new invRecord(ds.Tables[0].Rows[i][0].ToString(),
                                                ds.Tables[0].Rows[i][1].ToString(),
                                                ds.Tables[0].Rows[i].ItemArray[2].ToString()));
                }

                for (int j = 0; j < Records.Count; j++) {
                    ret = 0;
                    ds = db.ExecuteSQLStatement(query.getConnectedItems(Records[j].Num), ref ret);

                    for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                    {
                        //build the list of Records
                        Records[j].conItems.Add(new conItem(ds.Tables[0].Rows[k][0].ToString(),
                                                    ds.Tables[0].Rows[k][1].ToString(),
                                                    ds.Tables[0].Rows[k].ItemArray[2].ToString()));
                    }

                }
            }
            catch (Exception ex){
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
