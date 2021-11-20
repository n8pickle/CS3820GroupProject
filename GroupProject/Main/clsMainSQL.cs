using System;
using System.Reflection;

namespace GroupProject.Main
{
    /// <summary>
    /// Class for all the needed SQL statements for main window
    /// </summary>
    class clsMainSQL
    {
        /// <summary>
        /// Get all items from ItemDesc
        /// </summary>
        /// <returns></returns>
        public string getAllItems()
        {
            try
            {
                return "SELECT * from ItemDesc;";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Get an invoice from Invoices table
        /// </summary>
        /// <param name="invoiceNum">InvoiceNum</param>
        /// <returns></returns>
        public string getInvoice(int invoiceNum)
        {
            try
            {
                return "SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = " + invoiceNum + ";";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Add invoice to Invoices table
        /// </summary>
        /// <param name="date">InvoiceDate</param>
        /// <param name="cost">TotalCost</param>
        /// <returns></returns>
        public string insertInvoice(DateTime date, int cost)
        {
            try
            {
                return "INSERT INTO Invoices(InvoiceDate, TotalCost) Values(#" + date + "#, " + cost + ");";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Edit invoice in Invoices table
        /// </summary>
        /// <param name="cost">TotalCost</param>
        /// <param name="invoiceNum">InvoiceNum</param>
        /// <returns></returns>
        public string updateInvoice(int cost, int invoiceNum)
        {
            try
            {
                return "UPDATE Invoices SET TotalCost = " + cost + " WHERE InvoiceNum = " + invoiceNum + ";";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Get line items from an invoice
        /// </summary>
        /// <param name="invoicenum">InvoiceNum</param>
        /// <returns></returns>
        public string getLineItemsFromInvoice(int invoicenum)
        {
            try
            {
                return "SELECT LineItems.ItemCode, LineItems.InvoiceNum, ItemDesc.ItemDesc, ItemDesc.Cost FROM ItemDesc INNER JOIN LineItems ON ItemDesc.Itemcode = LineItems.ItemCode WHERE LineItems.InvoiceNum = " + invoicenum + ";";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Delete an invoice record from Invoices
        /// </summary>
        /// <param name="invoiceNum">InvoiceNum</param>
        /// <returns></returns>
        public string deleteInvoice(int invoiceNum)
        {
            try
            {
                return "DELETE From Invoices WHERE InvoiceNum = " + invoiceNum + ";";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Insert a line item into LineItems table
        /// </summary>
        /// <param name="invoiceNum">InvoiceNum</param>
        /// <param name="lineItemNum">LineItemNum</param>
        /// <param name="itemCode">ItemCode</param>
        /// <returns></returns>
        public string insertLineItem(int invoiceNum, int lineItemNum, string itemCode)
        {
            try
            {
                return "INSERT INTO LineItems(InvoiceNum, LineItemNum, ItemCode) Values(" + invoiceNum + ", " + lineItemNum + ", '" + itemCode + "');";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Delete line item in LineItems table
        /// </summary>
        /// <param name="invoiceNum">InvoiceNum</param>
        /// <returns></returns>
        public string deleteLineItem(int invoiceNum)
        {
            try
            {
                return "DELETE From LineItems WHERE InvoiceNum = " + invoiceNum + ";";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Delete a line item from an invoice
        /// </summary>
        /// <param name="invoiceNum">InvoiceNum</param>
        /// <param name="lineItemNum">LineItemNum</param>
        /// <returns></returns>
        public string deleteLineItemsFromInvoice(int invoiceNum, int lineItemNum)
        {
            try
            {
                return "DELETE FROM ItemDesc WHERE InvoiceNum = " + invoiceNum + " AND LineItemNum =  " + lineItemNum + ";";

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// Get max invoice number
        /// </summary>
        /// <returns></returns>
        public string getMaxInvoiceNum()
        {
            try
            {
                return "SELECT MAX(InvoiceNum) from Invoices;";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }



    }
}