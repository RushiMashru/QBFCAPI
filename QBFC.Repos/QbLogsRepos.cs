using Microsoft.EntityFrameworkCore;
using QBFC.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBFC.Repos.Base
{
    public class QbLogsRepos : IQbLogsRepos
    {
        private readonly QBFCDbcontext _dbContext;

        public QbLogsRepos(QBFCDbcontext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<tAPILogs>> GetAllLogs()
        {
            try
            {
                return await _dbContext.tAPILogs.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public async Task<tAPILogs> GetLogById(int Id)
        {
            try
            {
                return await _dbContext.tAPILogs.Where(x => x.ID == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<int> InsertLogs(tAPILogs ologs)
        {
            try
            {
                await _dbContext.AddAsync(ologs);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
