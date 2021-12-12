using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Model
{
    public class LineItemDisplayContainer
    {
        public string Code { get; set; }
        public int InvoiceNum { get; set; }
        public int LineItemNum { get; set; }

        public string ItemDesc { get; set; }

        public string ItemPrice { get; set; }

        /// <summary>
        /// The invoice date.
        /// </summary>
        public string InvoiceDate { get; set; }

        /// <summary>
        /// The total cost of the invoice.
        /// </summary>
        public int TotalCost { get; set; }
    }
}
