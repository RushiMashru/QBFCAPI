using System;
using System.Collections.Generic;
using System.Text;

namespace QBFC.Models.ViewModel
{
    public class BillRequestModel
    {
      public int AccountId { get; set; }
      public List<BillModel> BillModel { get; set; }
    }
}
