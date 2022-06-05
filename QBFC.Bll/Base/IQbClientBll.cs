using QBFC.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QBFC.Bll.Base
{
    public interface IQbClientBll
    {
        Task<string> PulseCheck();
        Task<bool> SetRToken(string rtoken);
        Task<Response<object>> GetCompanyInfo();
        Task<Response<object>> GetCustomerById(int id);
        Task<Response<object>> GetVendorById(int id);
        Task<Response<object>> GetAccountById(int id);
        Task<Response<object>> CreateBill(string content);
    }
}
