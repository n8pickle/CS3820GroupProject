using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Model
{
    /// <summary>
    /// This class is like an interface/inheritence. It gets and sets all the things from all teh tables in Invoice.mdb
    /// </summary>
    public class LineItemDisplayContainer
    {
        /// <summary>
        /// gets and sets the ItemCode. Both ItemDesc and LineItems tables.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// gets and sets the InoiceNum. Both Invoices and LineItems tables.
        /// </summary>
        public int InvoiceNum { get; set; }

        /// <summary>
        /// gets and sets the LineItemNum
        /// </summary>
        public int LineItemNum { get; set; }

        /// <summary>
        /// Gets and sets and ItemDesc
        /// </summary>
        public string ItemDesc { get; set; }

        /// <summary>
        /// gets and sets the Cost
        /// </summary>
        public string ItemPrice { get; set; }

        /// <summary>
        /// gets and sets the InvoiceDate
        /// </summary>
        public string InvoiceDate { get; set; }

        /// <summary>
        /// gets and sets the TotalCost
        /// </summary>
        public int TotalCost { get; set; }
    }
}
