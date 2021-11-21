using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupProject.Model;
using GroupProject;
using System.Reflection;

namespace GroupProject.Items
{
    public class clsItemsSQL
    {
        /// <summary>
        /// This is a private data access dependency
        /// </summary>
        private clsDataAccess _dataAccess;

        /// <summary>
        /// Constructor to do DI for the clsDataAcess
        /// </summary>
        public clsItemsSQL(clsDataAccess dataAccess) 
        {
            _dataAccess = dataAccess;
        }

        /// <summary>
        /// This is a method to update the jewelryItem that is changed when the save button is pressed
        /// </summary>
        public void UpdateJewelryItem(ItemViewModel itemViewModel) 
        {
            try
            {
                string sSql = $"UPDATE Item SET ItemDesc = {itemViewModel.Description}, ItemPrice = {itemViewModel.Price}, ItemName = {itemViewModel.Name} WHERE ItemNo = {itemViewModel.Code};";
                int numOfRowsAffected = _dataAccess.ExecuteNonQuery(sSql);
                Console.WriteLine($"The number of rows affected was {numOfRowsAffected}");

            } 
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        
        /// <summary>
        /// This is a method to get all items to display in the Datagrid
        /// </summary>
        public List<ItemViewModel> GetAllJewelryItems() 
        {
            try
            {
                List<ItemViewModel> resultList = new List<ItemViewModel>();
                string sSql = $"select ItemCode, ItemDesc, Cost from Item;";
                int recordsRetrieved = 0;
                _dataAccess.ExecuteSQLStatement(sSql, ref recordsRetrieved);
                // TODO parse DataTable to objects
                return resultList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes Item from database where the item code matches
        /// </summary>
        /// <param name="code"></param>
        public void DeleteItemByItemCode(string code)
        {
            try
            {
                string sSql = $"delete from Item where ItemCode like '{code}';";
                _dataAccess.ExecuteNonQuery(sSql);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// inserts item into the database.
        /// </summary>
        /// <param name="item"></param>
        public void InsertItem(ItemViewModel item)
        {
            try
            {
                string sSql = $"insert into Item (ItemCode, ItemName, ItemDesc, ItemPrice) Values ('{item.Code}', '{item.Name}', '{item.Description}', {item.Price});";
                _dataAccess.ExecuteNonQuery(sSql);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets the invoice number by the item code
        /// </summary>
        /// <param name="itemCode"></param>
        public void GetInvoiceNumberByItemCode(string itemCode)
        {
            try
            {
                string sSql = $"select distinct(InvoiceNum) from LineItems where ItemCode = '{itemCode}';";
                int recordsRetrieved = 0;
                _dataAccess.ExecuteSQLStatement(sSql, ref recordsRetrieved);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
