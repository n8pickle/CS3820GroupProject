using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupProject;
using GroupProject.Model;

namespace GroupProject.Main
{
    public class clsMainLogic
    {
        private clsMainSQL _sql;
        private clsDataAccess _dataAccess;
        public clsMainLogic()
        {
             _sql = new clsMainSQL();
            _dataAccess = new clsDataAccess();
        }

        public List<ItemViewModel> GetItemViewModels()
        {
            List<ItemViewModel> itemViewModels = new List<ItemViewModel>();
            int iRet = 0;
            var data = _dataAccess.ExecuteSQLStatement(_sql.getAllItems(), ref iRet);

            for(int i = 0; i < iRet; i++)
            {
                string itemCode = (string)data.Tables[0].Rows[i][0];
                string itemDesc = (string)data.Tables[0].Rows[i][1];
                double.TryParse(data.Tables[0].Rows[i][2].ToString(), out double cost);
                itemViewModels.Add(new ItemViewModel
                {
                    Code = itemCode,
                    Description = itemDesc,
                    Price = cost
                });
            }

            return itemViewModels;
        }
    }
}
