using QBFC.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QBFC.Bll.Base
{
    public interface IQbClientBll
    {
        Task<string> PulseCheck(int AccountId);
        Task<bool> SetRToken(string rtoken);
        Task<Response<object>> GetCompanyInfo(int AccountId);
        Task<Response<object>> GetCustomerById(int CustomerId, int AccountId);
        Task<Response<object>> GetVendorById(int VendorId, int AccountId);
        Task<Response<object>> GetExpenceAccountById(int ExpenceAccountId, int AccountId);
        Task<Response<object>> CreateBill(string content, int AccountId);
        Task<Response<object>> GetBillById(int BillId, int AccountId);
        Task<string> GetByQuery(string query, int AccountId);
    }
}
