using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupProject.Model;
using GroupProject;

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
            string sSql = $"UPDATE Item SET ItemDesc = {itemViewModel.Description}, ItemPrice = {itemViewModel.Price}, ItemName = {itemViewModel.Name} WHERE ItemNo = {itemViewModel.Id}";
            int numOfRowsAffected = _dataAccess.ExecuteNonQuery(sSql);
            Console.WriteLine($"The number of rows affected was {numOfRowsAffected}");
        }
        
        /// <summary>
        /// This is a method to get all items to display in the Datagrid
        /// </summary>
        public List<ItemViewModel> GetAllJewelryItems() 
        {
            List<ItemViewModel> resultList = new List<ItemViewModel>();
            string sSql = $"SELECT * FROM Item";
            int recordsRetrieved = 0;
            _dataAccess.ExecuteSQLStatement(sSql, ref recordsRetrieved);
            // TODO parse DataTable to objects
            return resultList;
        }
    }
}
