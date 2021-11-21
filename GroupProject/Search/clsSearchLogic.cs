using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GroupProject;

namespace GroupProject.Search
{
    class invRecord {

        invRecord(string passNum, string passDate, string passTotal) {
            Num = passNum;
            Date = passDate;
            Total = passTotal;
        }

        public string Num;
        public string Date;
        public string Total;

      
    }

    class clsSearchLogic
    {

        clsSearchSQL query = new clsSearchSQL();

        List<string> invoiceNums = new List<string>();

        List<string> invoiceDates = new List<string>();

        List<string> invoiceTotals = new List<string>();

        List<invRecord> Records = new List<invRecord>();

        public List<invRecord> GetInvoices(int numIndex, int dateIndex, int totalIndex) {
            try
            {
                return Records;
            }
            catch (Exception ex) {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public void resetLogic() {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public void refineSearch() { 
        
        }

    }
}
