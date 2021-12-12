using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GroupProject;
using GroupProject.Model;

namespace GroupProject.Items
{
    public class clsItemsLogic
    {
        /// <summary>
        /// Data access object
        /// </summary>
        private clsDataAccess _dataAccess = new clsDataAccess();

        private clsItemsSQL _sql = new clsItemsSQL();

        /// <summary>
        /// Method called to add item to the DB
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="price"></param>
        internal void AddItemToDb(string desc, double price)
        {
            try
            {
                var item = new ItemViewModel
                {
                    Code = GetNextPrimaryKey(),
                    Description = desc,
                    Price = price
                };
                string insertStatement = _sql.InsertItem(item);
                var changed = _dataAccess.ExecuteNonQuery(insertStatement);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets a list of all the items from ItemDesc table
        /// </summary>
        /// <returns></returns>
        internal List<ItemViewModel> GetItemViewModels()
        {
            try
            {
                List<ItemViewModel> items = new List<ItemViewModel>();
                int iRet = 0;
                string getAllItemsStatement = _sql.GetAllJewelryItems();
                var data = _dataAccess.ExecuteSQLStatement(getAllItemsStatement, ref iRet);
                for (int i = 0; i < iRet; i++)
                {
                    string itemCode = (string)data.Tables[0].Rows[i][0];
                    string itemDesc = (string)data.Tables[0].Rows[i][1];
                    double.TryParse(data.Tables[0].Rows[i][2].ToString(), out double cost);
                    items.Add(new ItemViewModel { Code = itemCode, Description = itemDesc, Price = cost });
                }
                items.Sort((a, b) => a.Code.CompareTo(b.Code));
                return items;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Gets the next possible primary key
        /// </summary>
        /// <returns></returns>
        private string GetNextPrimaryKey()
        {
            try
            {
                List<ItemViewModel> items = GetItemViewModels();
                items.Sort((a, b) => a.Code.CompareTo(b.Code));
                string currentLatestCode = items.Last().Code;
                char lastChar = currentLatestCode.ToCharArray().Last();
                if (lastChar == 'Z')
                {
                    return currentLatestCode + 'A';
                }
                else
                {
                    lastChar++;
                    return currentLatestCode.Substring(0, currentLatestCode.Length - 1) + lastChar;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes items from Db by item code
        /// </summary>
        /// <param name="code"></param>
        internal void DeleteItemFromDb(string code)
        {
            try
            {
                string deleteStatement = _sql.DeleteItemByItemCode(code);
                _dataAccess.ExecuteNonQuery(deleteStatement);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates item in the database
        /// </summary>
        /// <param name="code"></param>
        /// <param name="desc"></param>
        /// <param name="price"></param>
        internal void UpdateItemInDb(string code, string desc, double price)
        {
            try
            {
                var updatedItem = new ItemViewModel
                {
                    Code = code,
                    Description = desc,
                    Price = price
                };
                string updateStatment = _sql.UpdateJewelryItem(updatedItem);
                _dataAccess.ExecuteNonQuery(updateStatment);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets all invoices by the itemCode and returns the invoice numbers
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal List<int> GetInvoiceByItemCode(string code)
        {
            try
            {
                int iRet = 0;
                string getInvoiceQuery = _sql.GetInvoiceNumberByItemCode(code);
                var data = _dataAccess.ExecuteSQLStatement(getInvoiceQuery, ref iRet);
                
                if(iRet > 0)
                {
                    List<int> invoiceNumbers = new List<int>();
                    for(int i = 0; i < iRet; i++)
                    {
                        int.TryParse(data.Tables[0].Rows[i][0].ToString(), out int invoiceNum);
                        invoiceNumbers.Add(invoiceNum);
                    }
                    return invoiceNumbers;
                }
                else
                {
                    return new List<int>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
