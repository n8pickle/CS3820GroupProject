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
        /// This will Update an Invoice
        /// </summary>
        /// <returns></returns>
        public string UpdateInvoices(string TotalCost, string InvoiceNum)
        {
            try
            {
                string sSQL = "Update Invoices SET TotalCost = " + TotalCost + " WHERE InvoiceNum =  " + InvoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }
        /// <summary>
        /// This will Delete Selected Items
        /// </summary>
        /// <returns></returns>
        public string DeleteLineItems(string InvoiceNum)
        {
            try
            {
                string sSQL = "DELETE FROM LineItems WHERE InvoiceNum = " + InvoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }
        /// <summary>
        /// This will Delete an Item From the Invoice through the LineItems DB
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string DeleteItemFromInvoice(string invoiceNum, string itemCode)
        {
            try
            {
                return String.Format("DELETE FROM LineItems WHERE InvoiceNum = {0} AND ItemCode = '{1}'", invoiceNum, itemCode);

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// This will Delete Selected Invoices
        /// </summary>
        /// <returns></returns>
        public string DeleteInvoices(string InvoiceNum)
        {
            try
            {
                string sSQL = "DELETE FROM Invoices WHERE InvoiceNum = " + InvoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }
        /// <summary>
        /// This will Insert a new Set of Items and Attach it to the corresponding InvoiceNum
        /// </summary>
        /// <returns></returns>
        public string InsertLineItems(string InvoiceNum, string LineItemNum, string ItemCode)
        {
            try
            {
                string sSQL = String.Format("INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) VALUES ('{0}', '{1}', '{2}')", InvoiceNum, LineItemNum, ItemCode);
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }
        /// <summary>
        /// This will insert a new Invoice into the Database
        /// </summary>
        /// <returns></returns>
        public string InsertInvoices(string InvoiceDate, string TotalCost)
        {
            try
            {
                string sSQL = "INSERT INTO Invoices (InvoiceDate, TotalCost) VALUES (#" + InvoiceDate + "#, " + TotalCost + ")";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// This will Select and return Invoice Data from the DB
        /// </summary>
        /// <returns></returns>
        public string SelectInvoice(string InvoiceNum)
        {
            try
            {
                string sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum =" + InvoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// This will Select and Return Items from the DB
        /// </summary>
        /// <returns></returns>
        public string SelectItems()
        {
            try
            {
                string sSQL = "SELECT ItemCode, ItemDesc, Cost " +
                    "FROM ItemDesc";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// This will get all items
        /// </summary>
        /// <returns></returns>
        public string SelectAllItems()
        {
            try
            {
                return "SELECT * FROM ItemDesc";

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This will Select and Return The InvoiceNum and corresponding ItemNums
        /// </summary>
        /// <returns></returns>
        public string SelectLineItems(string InvoiceNum)
        {
            try
            {
                string sSQL = String.Format("SELECT InvoiceNum, LineItemNum, ItemCode FROM LineItems WHERE InvoiceNum = {0}", InvoiceNum);
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        // TODO: delte this
        public string SelectAllLineItems()
        {
            try
            {
                return "SELECT InvoiceNum, LineItemNum, ItemCode FROM LineItems";

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Select All Invoices
        /// </summary>
        /// <returns></returns>
        public string SelectAllInvoices()
        {
            try
            {
                return "SELECT * FROM Invoices";

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns the Max Invoice Number
        /// </summary>
        /// <returns></returns>
        public string GenerateInvoiceID()
        {
            try
            {
                return "SELECT MAX(InvoiceNum) FROM Invoices";

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// This will generate a Line Item Number via Invoice
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string GenerateLineItemNum(string invoiceNum)
        {
            try
            {
                return "SELECT MAX(LineItemNum) FROM LineItems WHERE InvoiceNum = " + invoiceNum;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// This will return an ItemCode by Description
        /// </summary>
        /// <param name="itemDesc"></param>
        /// <returns></returns>
        public string GetItemCode(string itemDesc)
        {
            try
            {
                return "SELECT ItemCode FROM ItemDesc WHERE ItemDesc = '" + itemDesc + "'";

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// This will Return the Item Cost by ItemCode
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string GetItemCost(string itemCode)
        {
            try
            {
                return String.Format("SELECT Cost FROM ItemDesc WHERE ItemCode = '" + itemCode + "'");

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// This will Return the Item Description
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string GetItemDesc(string itemCode)
        {
            try
            {
                return String.Format("SELECT ItemDesc FROM ItemDesc WHERE ItemCode = '" + itemCode + "'");
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This will Return all the item details from item desc
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string GetItemInfo(string itemDesc)
        {
            try
            {
                string sSQL = "SELECT * FROM ItemDesc WHERE ItemDesc = '" + itemDesc + "'";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This will Select and return Invoice Data from the DB
        /// </summary>
        /// <returns></returns>
        public string GetInvoiceDate(string InvoiceNum)
        {
            try
            {
                string sSQL = "SELECT InvoiceDate FROM Invoices WHERE InvoiceNum = " + InvoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }
    }



}
