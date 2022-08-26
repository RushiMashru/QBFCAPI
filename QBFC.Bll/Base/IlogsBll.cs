using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QBFC.Bll.Base
{
    public interface IlogsBll
    {
        Task<int> InsertLog(string request, string response, string message, bool isSuccess = true);
    }
}
