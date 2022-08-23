using QBFC.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QBFC.Repos.Base
{
    public interface IQbAuthRepos
    {
        Task<IEnumerable<tAuthDetails>> GetAllAuths();
        Task<tAuthDetails> GetAuthByAccountId(int AccountId, string QBEnv);
        Task<int> UpsertAuthDetails(tAuthDetails oAuth);

        Task<int> UpdateRefreshToken(int Id, string RefreshToken);
    }
}
