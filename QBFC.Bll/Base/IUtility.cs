using QBFC.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QBFC.Bll.Base
{
    public interface IUtility
    {
        Task<string> GetQBBillModel(BillRequestModel model);
    }
}
