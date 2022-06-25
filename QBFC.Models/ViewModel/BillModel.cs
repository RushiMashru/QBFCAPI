using System;
using System.Collections.Generic;
using System.Text;

namespace QBFC.Models.ViewModel
{
    public class BillModel
    {
        public string RefNumber { get; set; }
        public string Vendor { get; set; }
        public string VendorValue { get; set; }
        public string TxnDate { get; set; }
        public string DueDate { get; set; }
        public string SalesTerm { get; set; }
        public string SalesTermValue { get; set; }
        public string ExpenseClass { get; set; }
        public string ExpenseClassValue { get; set; }
        public string ExpenseAccount { get; set; }
        public string ExpenseAccountValue { get; set; }
        public string ExpenseDesc { get; set; }
        public string ExpenseAmount { get; set; }
        public string PrivateNote { get; set; }
    }
}
