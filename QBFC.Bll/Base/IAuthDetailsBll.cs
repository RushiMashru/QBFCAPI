using QBFC.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QBFC.Bll.Base
{
    public interface IAuthDetailsBll
    {
        Task<int> UpsertAuthDetails(AuthModel authModel);
        Task<Response<AuthModel>> GetAuthByAccountId(int AccountId, string QBEnv);
        Task<int> UpdateRefreshToken(int Id, string RefreshToken);
    }
}
