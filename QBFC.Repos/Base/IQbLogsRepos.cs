using QBFC.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QBFC.Repos.Base
{
    public interface IQbLogsRepos
    {
        Task<IEnumerable<tAPILogs>> GetAllLogs();
        Task<tAPILogs> GetLogById(int Id);
        Task<int> InsertLogs(tAPILogs ologs);
    }
}
