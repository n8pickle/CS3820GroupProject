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
            string sSQL = "Update Invoices SET TotalCost = " + TotalCost + " WHERE InvoiceNum =  " + InvoiceNum;
            return sSQL;
        }
        /// <summary>
        /// This will Delete Selected Items
        /// </summary>
        /// <returns></returns>
        public string DeleteLineItems(string InvoiceNum)
        {
            string sSQL = "DELETE FROM LineItems WHERE InvoiceNum = " + InvoiceNum;
            return sSQL;
        }
        /// <summary>
        /// This will Delete an Item From the Invoice through the LineItems DB
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string DeleteItemFromInvoice(string invoiceNum, string itemCode)
        {
            return String.Format("DELETE FROM LineItems WHERE InvoiceNum = {0} AND ItemCode = '{1}'", invoiceNum, itemCode);
        }
        /// <summary>
        /// This will Delete Selected Invoices
        /// </summary>
        /// <returns></returns>
        public string DeleteInvoices(string InvoiceNum)
        {
            string sSQL = "DELETE FROM Invoices WHERE InvoiceNum = " + InvoiceNum;
            return sSQL;
        }
        /// <summary>
        /// This will Insert a new Set of Items and Attach it to the corresponding InvoiceNum
        /// </summary>
        /// <returns></returns>
        public string InsertLineItems(string InvoiceNum, string LineItemNum, string ItemCode)
        {
            string sSQL = String.Format("INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) " +
                "VALUES ('{0}', '{1}', '{2}')", InvoiceNum, LineItemNum, ItemCode);
            return sSQL;
        }
        /// <summary>
        /// This will insert a new Invoice into the Database
        /// </summary>
        /// <returns></returns>
        public string InsertInvoices(string InvoiceDate, string TotalCost)
        {
            string sSQL = "INSERT INTO Invoices (InvoiceDate, TotalCost)" +
                "VALUES (#" + InvoiceDate + "#, " + TotalCost + ")";
            return sSQL;
        }

        /// <summary>
        /// This will Select and return Invoice Data from the DB
        /// </summary>
        /// <returns></returns>
        public string SelectInvoice(string InvoiceNum)
        {
            string sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCost " +
                "FROM Invoices WHERE InvoiceNum =" + InvoiceNum;
            return sSQL;
        }

        /// <summary>
        /// This will Select and Return Items from the DB
        /// </summary>
        /// <returns></returns>
        public string SelectItems()
        {
            string sSQL = "SELECT ItemCode, ItemDesc, Cost " +
                "FROM ItemDesc";
            return sSQL;
        }

        /// <summary>
        /// This will get all items
        /// </summary>
        /// <returns></returns>
        public string SelectAllItems()
        {
            return "SELECT * FROM ItemDesc";
        }

        /// <summary>
        /// This will Select and Return The InvoiceNum and corresponding ItemNums
        /// </summary>
        /// <returns></returns>
        public string SelectLineItems(string InvoiceNum)
        {
            string sSQL = String.Format("SELECT InvoiceNum, LineItemNum, ItemCode " +
                "FROM LineItems " +
                "WHERE InvoiceNum = {0}", InvoiceNum);
            return sSQL;
        }

        // TODO: delte this
        public string SelectAllLineItems()
        {
            return "SELECT InvoiceNum, LineItemNum, ItemCode FROM LineItems";
        }
        
        /// <summary>
        /// Select All Invoices
        /// </summary>
        /// <returns></returns>
        public string SelectAllInvoices()
        {
            return "SELECT * FROM Invoices";
        }

        /// <summary>
        /// Returns the Max Invoice Number
        /// </summary>
        /// <returns></returns>
        public string GenerateInvoiceID()
        {
            return "SELECT MAX(InvoiceNum) FROM Invoices";
        }
        /// <summary>
        /// This will generate a Line Item Number via Invoice
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string GenerateLineItemNum(string invoiceNum)
        {
            return "SELECT MAX(LineItemNum) FROM LineItems WHERE InvoiceNum = " + invoiceNum;
        }
        /// <summary>
        /// This will return an ItemCode by Description
        /// </summary>
        /// <param name="itemDesc"></param>
        /// <returns></returns>
        public string GetItemCode(string itemDesc)
        {
            return "SELECT ItemCode FROM ItemDesc WHERE ItemDesc = '" + itemDesc + "'";
        }
        /// <summary>
        /// This will Return the Item Cost by ItemCode
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string GetItemCost(string itemCode)
        {
            return String.Format("SELECT Cost FROM ItemDesc WHERE ItemCode = '" + itemCode + "'");
        }
        /// <summary>
        /// This will Return the Item Description
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string GetItemDesc(string itemCode)
        {
            return String.Format("SELECT ItemDesc FROM ItemDesc WHERE ItemCode = '" + itemCode + "'");
        }
    }



}