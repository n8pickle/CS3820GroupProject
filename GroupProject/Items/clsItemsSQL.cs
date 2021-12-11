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
        /// This is a method to update the jewelryItem that is changed when the save button is pressed
        /// </summary>
        public string UpdateJewelryItem(ItemViewModel itemViewModel) 
        {
            try
            {
                return $"UPDATE ItemDesc SET ItemDesc = '{itemViewModel.Description}', Cost = {itemViewModel.Price} WHERE ItemCode = '{itemViewModel.Code}';";
            } 
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        
        /// <summary>
        /// This is a method to get all items to display in the Datagrid
        /// </summary>
        public string GetAllJewelryItems() 
        {
            try
            {
                return "select ItemCode, ItemDesc, Cost from ItemDesc;";
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
        public string DeleteItemByItemCode(string code)
        {
            try
            {
               return $"delete from ItemDesc where ItemCode like '{code}';";
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
        public string InsertItem(ItemViewModel item)
        {
            try
            {
                return $"INSERT INTO ItemDesc (ItemCode, ItemDesc, Cost) VALUES ('{item.Code}', '{item.Description}', {item.Price})";
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
        public string GetInvoiceNumberByItemCode(string itemCode)
        {
            try
            {
                return $"select distinct(InvoiceNum) from LineItems where ItemCode = '{itemCode}';";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
