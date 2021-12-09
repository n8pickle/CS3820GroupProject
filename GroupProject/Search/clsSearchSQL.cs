using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GroupProject;


namespace GroupProject.Search
{
    /// <summary>
    /// This class will just hold query strings for the search logic and not do any real business logic
    /// Will at most concatecnate values and return appropriate query strings
    /// </summary>
    class clsSearchSQL
    {
        //Class Variable to hold the current query that the search logic is asking for
        string curQuery = "";

        /////////////////////////////////////////////////////////////////////////////////////////////////
        /// These Queries will be used to fill the datagrid with records that match the specified values
        /// in the search comboboxes

        /// <summary>
        /// Gets all of the invoices, meant to be called on initial load and on clear selection
        /// </summary>
        /// <returns>Query string to select all invoices</returns>
        public string getAll()
        {
            try
            {
                curQuery = "SELECT * FROM Invoices;";

                return curQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Build query to get records that match the given invoice number
        /// </summary>
        /// <param name="num">Specific InvoiceNum to match to</param>
        /// <returns></returns>
        public string getFromInvoiceNum(string num)
        {
            try
            {
                curQuery = "SELECT * FROM Invoices WHERE InvoiceNum = " + num + ";";

                return curQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// builds query to select record matching given InvoiceDate
        /// </summary>
        /// <param name="date">InvoiceDate value to match</param>
        /// <returns></returns>
        public string getFromDate(string date)
        {
            try
            {
                curQuery = "SELECT * FROM Invoices WHERE InvoiceDate = #" + date + "#;";

                return curQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Retrieve records matching the given TotalCost
        /// </summary>
        /// <param name="charge">TotalCost to match to</param>
        /// <returns></returns>
        public string getFromTotalCharge(string charge)
        {
            try
            {
                curQuery = "SELECT * FROM Invoices WHERE TotalCost = " + charge + ";";

                return curQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// query to retrieve invoices matching a number and a date
        /// </summary>
        /// <param name="num">InvoiceNum to match to</param>
        /// <param name="date">InvoiceDate to match to</param>
        /// <returns></returns>
        public string getFromNumAndDate(string num, string date)
        {
            try
            {
                curQuery = "SELECT * FROM Invoices WHERE InvoiceNum = " + num + " AND InvoiceDate = #" + date + "#;";

                return curQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Query to retrive records given an InvoiceNum and a TotalCost value
        /// </summary>
        /// <param name="num">InvoiceNum to match to</param>
        /// <param name="charge">TotalCostToMatch To</param>
        /// <returns></returns>
        public string getfromNumAndCharge(string num, string charge)
        {
            try
            {
                curQuery = "SELECT * FROM Invoices WHERE InvoiceNum = " + num + " AND TotalCost = " + charge + ";";


                return curQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Builds Query to match records to an InvoiceDate and TotalCost
        /// </summary>
        /// <param name="date">InvoiceDate to match to</param>
        /// <param name="charge">TotalCost to match to</param>
        /// <returns></returns>
        public string getFromDateAndCharge(string date, string charge)
        {
            try
            {
                curQuery = "SELECT * FROM Invoices WHERE InvoiceDate = #" + date + "# AND TotalCost = " + charge + ";";

                return curQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Builds query to match record to specific values for all 3 fields
        /// </summary>
        /// <param name="num">InvoiceNum to match to</param>
        /// <param name="date">InvoiceDate to match to</param>
        /// <param name="charge">TotalCost to Match to</param>
        /// <returns></returns>
        public string getFromAllThree(string num, string date, string charge)
        {
            try
            {
                curQuery = "SELECT * FROM Invoices WHERE InvoiceNum = "
                            + num + " AND InvoiceDate = #"
                            + date + "# AND TotalCost = " + charge + ";";

                return curQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*                                -------------- BREAK --------------              */
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        ///This section will be general purpose queries to fill the comboboxes when first loaded, 
        ///or when the search is cleared
        ///



        /// <summary>
        /// Query to get all the InvoiceNumbers in the table, meant to be used to fill the combobox on load
        /// </summary>
        /// <returns></returns>
        public string getNumbers()
        {
            try
            {
                curQuery = "SELECT InvoiceNum FROM Invoices;";

                return curQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets all the charges in the Invoice table. Meant to to be used with combobox on load
        /// </summary>
        /// <returns></returns>
        public string getCharges()
        {
            try
            {
                curQuery = "SELECT DISTINCT TotalCost FROM Invoices ORDER BY TotalCost DESC;";
                return curQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets all the dates from the Invoice table. Meant to be used with combobox on load
        /// </summary>
        /// <returns></returns>
        public string getDates()
        {
            try
            {
                curQuery = "SELECT DISTINCT InvoiceDate FROM Invoices;";
                return curQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////

        /*                       ---------BREAK ------------                                */

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Functions in this section will be used to dynamically fill comboboxes, so when a specific item is chosen
        /// From any of the 3 comboboxes, any blank comboboxes will only contain values associated with the values chosen
        ///


        /// <summary>
        /// Get all dates matching InvoiceNum and TotalCost
        /// </summary>
        /// <param name="num">InvoiceNum to Match To</param>
        /// <param name="charge">TotalCost to Match to</param>
        /// <returns></returns>
        public string getDatesWithNumAndCharge(string num, string charge)
        {
            try
            {
                curQuery = "SELECT InvoiceDate FROM Invoices WHERE InvoiceNum = " + num
                            + " AND TotalCost = " + charge
                            + ";";

                return curQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Get dates with a specified InvoiceNum
        /// </summary>
        /// <param name="num">InvoiceNum to match to</param>
        /// <returns></returns>
        public string getDatesWithNum(string num)
        {
            try
            {
                curQuery = "SELECT InvoiceDate FROM Invoices WHERE InvoiceNum = " + num
                            + ";";

                return curQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Get Dates that match to a TotalCost
        /// </summary>
        /// <param name="charge">TotalCost to match to</param>
        /// <returns></returns>
        public string getDatesWithCharge(string charge)
        {
            try
            {
                curQuery = "SELECT InvoiceDate FROM Invoices WHERE TotalCost = " + charge
                            + ";";

                return curQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Get charges that match with a specific InvoiceNum and InvoiceDate
        /// </summary>
        /// <param name="num">InvoiceNum to match to</param>
        /// <param name="date">InvoiceDate to match to</param>
        /// <returns></returns>
        public string getChargeWithNumAndDate(string num, string date)
        {
            try
            {
                curQuery = "SELECT TotalCost FROM Invoices WHERE InvoiceNum = " + num +
                            " AND InvoiceDate = #" + date + "# ORDER BY TotalCost DESC;";

                return curQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Get Charges that match with a specific InvoiceNum
        /// </summary>
        /// <param name="num">InvoiceNum to match to</param>
        /// <returns></returns>
        public string getChargeWithNum(string num)
        {
            try
            {
                curQuery = "SELECT TotalCost FROM Invoices WHERE InvoiceNum = " + num +
                            " ORDER BY TotalCost DESC;";

                return curQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Get charges that match to a specific InvoiceDate
        /// </summary>
        /// <param name="date">InvoiceDate to match to</param>
        /// <returns></returns>
        public string getChargeWithDate(string date)
        {
            try
            {
                curQuery = "SELECT TotalCost FROM Invoices WHERE InvoiceDate = #" + date + "# ORDER BY TotalCost DESC;";

                return curQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// get InvoiceNums that match to a specific TotalCost and InvoiceDate
        /// </summary>
        /// <param name="charge">TotalCost To Match To</param>
        /// <param name="date">InvoiceDate to match to</param>
        /// <returns></returns>
        public string getNumWithChargeAndDate(string charge, string date)
        {
            try
            {
                curQuery = "SELECT InvoiceNum FROM Invoices WHERE TotalCost = " + charge +
                            " AND InvoiceDate = #" + date + "#;";

                return curQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Get InvoiceNums with specific TotalCost
        /// </summary>
        /// <param name="charge">TotalCost to match to</param>
        /// <returns></returns>
        public string getNumWithCharge(string charge)
        {
            try
            {
                curQuery = "SELECT InvoiceNum FROM Invoices WHERE TotalCost = " + charge +
                            ";";

                return curQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Get InvoiceNums that Match to specific InvoiceDate
        /// </summary>
        /// <param name="date">InvoiceDate to match to</param>
        /// <returns></returns>
        public string getNumWithDate(string date)
        {
            try
            {
                curQuery = "SELECT InvoiceNum FROM Invoices WHERE InvoiceDate = #" + date + "#;";

                return curQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Get the items connected to a particular invoice
        /// </summary>
        /// <param name="ID">Invoice ID to match</param>
        /// <returns>Query that will return the Connected items</returns>
        public string getConnectedItems(string ID)
        {
            try
            {
                curQuery = "SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost " +
                            " FROM ItemDesc " +
                            "INNER JOIN LineItems " +
                            "ON ItemDesc.Itemcode = LineItems.ItemCode " +
                            "WHERE LineItems.InvoiceNum = " + ID + ";";
                return curQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
