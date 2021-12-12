using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Model
{
    /// <summary>
    /// class for storing stuff in the Invoice table in InvoiceModel
    /// </summary>
    public class InvoiceModel
    {
        /// <summary>
        /// gets and sets the InvoiceNum
        /// </summary>
        public int InvoiceNum { get; set; }

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
