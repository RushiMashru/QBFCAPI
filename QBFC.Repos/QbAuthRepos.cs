using Microsoft.EntityFrameworkCore;
using QBFC.Models.DataModel;
using QBFC.Repos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBFC.Repos
{
    public class QbAuthRepos : IQbAuthRepos
    {
        private readonly QBFCDbcontext _dbContext;
        public QbAuthRepos(QBFCDbcontext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<tAuthDetails>> GetAllAuths()
        {
            try
            {
                return await _dbContext.tAuthDetails.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<tAuthDetails> GetAuthByAccountId(int AccountId, string QBEnv)
        {
            try
            {
                return await _dbContext.tAuthDetails.Where(x => x.AccountID == AccountId && x.QBEnv == QBEnv.ToLower()).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpsertAuthDetails(tAuthDetails oAuth)
        {
            try
            {
                if (oAuth != null)
                {
                    if (oAuth.ID != 0)
                    {
                        _dbContext.Update(oAuth);
                    }
                    else
                    {
                        await _dbContext.AddAsync(oAuth);
                    }

                    return await _dbContext.SaveChangesAsync();
                }

                return 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdateRefreshToken(int Id, string RefreshToken)
        {
            try
            {
                var oAuth = await _dbContext.tAuthDetails.Where(x => x.ID == Id).FirstOrDefaultAsync();

                if (oAuth != null)
                {
                    oAuth.RefreshToken = RefreshToken;
                    oAuth.CreatedDT = DateTime.Now;
                    oAuth.ConsumedDT = DateTime.Now;
                    _dbContext.Update(oAuth);
                    return await _dbContext.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
