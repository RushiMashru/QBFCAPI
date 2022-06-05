using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QBFC.Bll.Base
{
    public interface IHttpClientBll
    {
        Task<string> HttpGet(string uri, string authToken);
        Task<string> HttpPost(string uri, string authToken, string data);
    }
}
