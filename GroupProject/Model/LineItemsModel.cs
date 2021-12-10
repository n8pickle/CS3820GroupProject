using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Model
{
    class LineItemsModel
    {
        /// <summary>
        /// Invoice Number
        /// </summary>
        public int InvoiceNum { get; set; }

        /// <summary>
        /// Line Item Number
        /// </summary>
        public int LineItemNum { get; set; }

        /// <summary>
        /// The Item code for the transaction
        /// </summary>
        public string ItemCode { get; set; }
    }
}
