using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Model
{
    class InvoiceModel
    {
        /// <summary>
        /// The invoice number.
        /// </summary>
        public int InvoiceNum { get; set; }

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
