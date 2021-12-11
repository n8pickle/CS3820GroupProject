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
        public int LineItemNum;

        public string ItemDesc;

        public string ItemPrice;
    }
}
