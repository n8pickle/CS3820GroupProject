using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Model
{
    /// <summary>
    /// class for storing stuff in the LineItems table in LineItemsModel
    /// </summary>
    public class LineItemsModel
    {
        /// <summary>
        /// gets and sets the InvoiceNum
        /// </summary>
        public int InvoiceNum { get; set; }

        /// <summary>
        /// gets and sets the LineItemNum
        /// </summary>
        public int LineItemNum { get; set; }

        /// <summary>
        /// gets and sets the ItemCode
        /// </summary>
        public string Code { get; set; }
    }
}
