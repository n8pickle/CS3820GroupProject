using GroupProject.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Main
{
    class clsMainLogic
    {
        #region Variables
        /// <summary>
        /// Object for the Database
        /// </summary>
        clsDataAccess db = new clsDataAccess();
        /// <summary>
        /// Object for SQL queries
        /// </summary>
        clsMainSQL sql = new clsMainSQL();
        /// <summary>
        /// List of Invoice Objects 
        /// </summary>
        List<InvoiceModel> invoiceResult;

        /// <summary>
        /// List of item objects
        /// </summary>
        List<ItemViewModel> itemResult;

        /// <summary>
        /// List of interface item/line item objects
        /// </summary>
        List<LineItemDisplayContainer> displayList;
        /// <summary>
        /// List of Item Objects
        /// </summary>
        List<ItemViewModel> itemsResult;
        /// <summary>
        /// List of Item Objects for Searching By Item Code
        /// </summary>
        List<LineItemDisplayContainer> itemsSearch;
        /// <summary>
        /// List of Line Items Objects
        /// </summary>
        List<LineItemsModel> lineItemsResult;
        #endregion
        #region Get Logic
        /// <summary>
        /// Runs the provided SQL string and fills the items variable with the results;
        /// </summary>
        /// <param name="sSQL"></param>
        /// <returns></returns>
        public List<ItemViewModel> getItems()
        {
            try
            {
                DataSet ds = new DataSet();
                int iRef = 0;
                itemsResult = new List<ItemViewModel>();

                var query = sql.SelectItems();

                ItemViewModel items;

                ds = db.ExecuteSQLStatement(query, ref iRef);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    items = new ItemViewModel();
                    items.Code = ds.Tables[0].Rows[i][0].ToString();
                    items.Description = ds.Tables[0].Rows[i][1].ToString();
                    items.Price = Convert.ToDouble(ds.Tables[0].Rows[i][2].ToString());

                    itemsResult.Add(items);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
            return itemsResult;
        }

        public List<LineItemDisplayContainer> getAllLineItems()
        {
            try
            {
                DataSet dsLineItems = new DataSet();
                DataSet dsInvoice = new DataSet();
                DataSet dsItemDesc = new DataSet();
                DataSet dsItemCost = new DataSet();

                int iRet = 0;
                
                displayList = new List<LineItemDisplayContainer>();

                var query = sql.SelectAllLineItems();

                dsLineItems = db.ExecuteSQLStatement(query, ref iRet);

                LineItemDisplayContainer displayLineItem;

                for (int i = 0; i < dsLineItems.Tables[0].Rows.Count; i++)
                {
                    displayLineItem = new LineItemDisplayContainer();
                    displayLineItem.InvoiceNum = Convert.ToInt32(dsLineItems.Tables[0].Rows[i][0].ToString());
                    displayLineItem.LineItemNum = Convert.ToInt32(dsLineItems.Tables[0].Rows[i][1].ToString());
                    displayLineItem.Code = dsLineItems.Tables[0].Rows[i][2].ToString();

                    var queryItemDesc = sql.GetItemDesc(displayLineItem.Code);
                    var queryItemCost = sql.GetItemCost(displayLineItem.Code);
                    var queryInvoiceDate = sql.GetInvoiceDate(displayLineItem.InvoiceNum.ToString());

                    dsInvoice = db.ExecuteSQLStatement(queryInvoiceDate, ref iRet);
                    displayLineItem.InvoiceDate = dsInvoice.Tables[0].Rows[0][0].ToString();
                    
                    if (queryItemDesc != "")
                    {
                        dsItemDesc = db.ExecuteSQLStatement(queryItemDesc, ref iRet);
                        displayLineItem.ItemDesc = dsItemDesc.Tables[0].Rows[0][0].ToString();
                    }

                    if (queryItemCost != "")
                    {
                        dsItemCost = db.ExecuteSQLStatement(queryItemCost, ref iRet);
                        displayLineItem.ItemPrice = dsItemCost.Tables[0].Rows[0][0].ToString();
                    }
                    
                    displayList.Add(displayLineItem);
                }
                return displayList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Runs the provided SQL string and fills string up with result
        /// </summary>
        /// <param name="itemDesc"></param>
        /// <returns></returns>
        public string getItemCode(string itemDesc)
        {
            try
            {
                DataSet ds = new DataSet();
                int iRef = 0;
                var query = sql.GetItemCode(itemDesc);

                ds = db.ExecuteSQLStatement(query, ref iRef);

                string itemCode = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                return itemCode;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Runs the provided SQL string and fills string up with result
        /// </summary>
        /// <param name="itemDesc"></param>
        /// <returns></returns>
        public LineItemDisplayContainer getItemInfo(string itemDesc)
        {
            try
            {

                DataSet ds = new DataSet();
                int iRef = 0;
                var query = sql.GetItemInfo(itemDesc);

                //itemsSearch = new List<LineItemDisplayContainer>();

                LineItemDisplayContainer items = new LineItemDisplayContainer();

                ds = db.ExecuteSQLStatement(query, ref iRef);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    items.Code = ds.Tables[0].Rows[i][0].ToString();
                    items.ItemDesc = ds.Tables[0].Rows[i][1].ToString();
                    items.ItemPrice = ds.Tables[0].Rows[i][2].ToString();

                    //itemsSearch.Add(items);
                }

                return items;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Runs SQL to Get Invoice Items through the LineItems DB
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public List<LineItemsModel> getInvoiceItems(string invoiceNum)
        {
            try
            {
                DataSet ds = new DataSet();
                int iRef = 0;

                var query = sql.SelectLineItems(invoiceNum);

                lineItemsResult = new List<LineItemsModel>();

                ds = db.ExecuteSQLStatement(query, ref iRef);

                LineItemsModel li;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    li = new LineItemsModel();
                    li.InvoiceNum = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                    li.LineItemNum = Convert.ToInt32(ds.Tables[0].Rows[i][1]);
                    li.Code = ds.Tables[0].Rows[i][2].ToString();

                    lineItemsResult.Add(li);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

            return lineItemsResult;
        }
        /// <summary>
        /// Runs SQL to Get Item Cost
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public double getItemCost(string itemCode)
        {
            try
            {
                DataSet ds = new DataSet();
                int iRef = 0;
                var query = sql.GetItemCost(itemCode);

                ds = db.ExecuteSQLStatement(query, ref iRef);

                string itemCost = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                double cost;
                Double.TryParse(itemCost, out cost);

                return cost;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Runs SQL to Get Items and returns an Item List
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public List<LineItemDisplayContainer> GetItemsByCode(string itemCode)
        {
            try
            {
                DataSet ds = new DataSet();
                int iRef = 0;
                var query = sql.GetItemDesc(itemCode);
                itemsSearch = new List<LineItemDisplayContainer>();

                ds = db.ExecuteSQLStatement(query, ref iRef);

                LineItemDisplayContainer items = new LineItemDisplayContainer();

                items.ItemDesc = ds.Tables[0].Rows[0].ItemArray[0].ToString();

                itemsSearch.Add(items);

                return itemsSearch;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
            
        }

        public List<InvoiceModel> GetAllInvoices()
        {
            try
            {
                DataSet ds = new DataSet();
                int iRef = 0;
                var query = sql.SelectAllInvoices();
                invoiceResult = new List<InvoiceModel>();

                InvoiceModel invoice;

                ds = db.ExecuteSQLStatement(query, ref iRef);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    invoice = new InvoiceModel();
                    invoice.InvoiceNum = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                    invoice.InvoiceDate = ds.Tables[0].Rows[i][1].ToString();
                    // TODO: description and cost
                    invoice.TotalCost = Convert.ToInt32(ds.Tables[0].Rows[i][2]);

                    invoiceResult.Add(invoice);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

            return invoiceResult;

        }

        /*
        public List<InvoiceModel> GetAllItems()
        {
            try
            {
                DataSet ds = new DataSet();
                int iRef = 0;
                var query = sql.SelectAllItems();
                itemsResult = new List<ItemViewModel>();

                ItemViewModel itemsResult;

                ds = db.ExecuteSQLStatement(query, ref iRef);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    itemsResult = new ItemViewModel();
                    itemsResult.Code = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                    itemsResult.Description = ds.Tables[0].Rows[i][1].ToString();
                    // TODO: description and cost
                    itemsResult.Price = Convert.ToInt32(ds.Tables[0].Rows[i][2]);

                    itemsResult.Add(itemsResult);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

            return invoiceResult;

        }
        */
        #endregion
        #region Insert Logic
        /// <summary>
        /// Non Query to Insert Invoice Item into Line Item DB
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="lineItemNum"></param>
        /// <param name="itemCode"></param>
        public void InsertLineItem(string invoiceNum, string lineItemNum, string itemCode)
        {
            try
            {
                var query = sql.InsertLineItems(invoiceNum, lineItemNum, itemCode);

                db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
        #region Update Logic
        /// <summary>
        /// Non Query To Update Invoices Total
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="total"></param>
        public void UpdateInvoiceTotal(string invoiceNum, string total)
        {
            try
            {
                var query = sql.UpdateInvoices(total, invoiceNum);

                db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
        #region Delete Logic
        /// <summary>
        /// Method to Delete Line Items from DB
        /// </summary>
        /// <param name="invoiceNum"></param>
        public void DeleteLineItems(string invoiceNum)
        {
            try
            {
                db.ExecuteNonQuery(sql.DeleteLineItems(invoiceNum));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Non Query to Delete Item From Invoice through Line Item DB
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="itemCode"></param>
        public void DeleteItemFromInvoice(string invoiceNum, string itemCode)
        {
            try
            {
                db.ExecuteNonQuery(sql.DeleteItemFromInvoice(invoiceNum, itemCode));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Method to Delete Invoices from DB
        /// </summary>
        /// <param name="invoiceNum"></param>
        public void DeleteInvoice(string invoiceNum)
        {
            try
            {
                db.ExecuteNonQuery(sql.DeleteInvoices(invoiceNum));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Method to Save Invoice
        /// </summary>
        /// <param name="date"></param>
        /// <param name="total"></param>
        public void SaveInvoice(string date, string total)
        {
            try
            {
                db.ExecuteNonQuery(sql.InsertInvoices(date, total));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Method to Generate an Invoice ID
        /// </summary>
        /// <returns></returns>
        public string GenerateInvoiceID()
        {
            try
            {
                int iRet = 0;
                int mID = 0;

                DataSet ds = new DataSet();

                var query = sql.GenerateInvoiceID();

                ds = db.ExecuteSQLStatement(query, ref iRet);
                Int32.TryParse(ds.Tables[0].Rows[0].ItemArray[0].ToString(), out mID);
                string newID = (mID).ToString();

                return newID;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Method to Generate Line Item Number by Invoice Number
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string GenerateLineItemNum(string invoiceNum)
        {
            try
            {
                DataSet ds = new DataSet();
                int iRet = 0;
                int mID = 0;

                var query = sql.GenerateLineItemNum(invoiceNum);

                ds = db.ExecuteSQLStatement(query, ref iRet);
                Int32.TryParse(ds.Tables[0].Rows[0].ItemArray[0].ToString(), out mID);
                mID++;
                string newNum = (mID).ToString();

                return newNum;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
    }
}
