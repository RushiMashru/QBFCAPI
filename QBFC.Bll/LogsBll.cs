using QBFC.Bll.Base;
using QBFC.Models.DataModel;
using QBFC.Repos.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QBFC.Bll
{
    public class LogsBll : IlogsBll
    {
        private readonly IQbLogsRepos _logsRepos;
        public LogsBll(IQbLogsRepos logsRepos)
        {
            _logsRepos = logsRepos;
        }
        public async Task<int> InsertLog(string request, string response, string message, bool isSuccess = true)
        {
            var result = await _logsRepos.InsertLogs(new tAPILogs
            {
                Source = $"API",
                Request = request,
                Response = response,
                Message = isSuccess ? $"OK {message}" : $"Failed {message}",
                CreatedDT = DateTime.Now
            });

            return result;
        }
    }
}
