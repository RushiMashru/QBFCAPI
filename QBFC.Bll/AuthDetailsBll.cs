using Newtonsoft.Json;
using QBFC.Bll.Base;
using QBFC.Models.DataModel;
using QBFC.Models.ViewModel;
using QBFC.Repos.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QBFC.Bll
{
    public class AuthDetailsBll: IAuthDetailsBll
    {

        private readonly IlogsBll _logs;
        private readonly IQbAuthRepos _qbAuthRepos;

        public AuthDetailsBll(IlogsBll logs, IQbAuthRepos qbAuthRepos)
        {
            _logs = logs;
            _qbAuthRepos = qbAuthRepos;
        }

        // insert or update auth details in DB
        public async Task<int> UpsertAuthDetails(AuthModel authModel)
        {
            if (authModel != null)
            {
                tAuthDetails otAuthDetails = new tAuthDetails()
                {
                    ID = authModel.ID,
                    AccountID = authModel.AccountID,
                    QBID = authModel.QBID,
                    ClientID = authModel.ClientID,
                    ClientSecret = authModel.ClientSecret,
                    AccessToken = authModel.AccessToken,
                    RefreshToken = authModel.RefreshToken,
                    QBEnv = authModel.QBEnv,
                    Status = authModel.Status,
                    CreatedDT = DateTime.Now,
                    ConsumedDT = DateTime.Now
                };

                var result = await _qbAuthRepos.UpsertAuthDetails(otAuthDetails);

                _ = await _logs.InsertLog(JsonConvert.SerializeObject(otAuthDetails), JsonConvert.SerializeObject(result), "UpsertAuthDetails");

                return result;

            }

            _ = await _logs.InsertLog(JsonConvert.SerializeObject(authModel), JsonConvert.SerializeObject(0), "UpsertAuthDetails", false);

            return 0;
        }

        // update refresh token in DB
        public async Task<int> UpdateRefreshToken(int Id, string RefreshToken)
        {
            var result = await _qbAuthRepos.UpdateRefreshToken(Id, RefreshToken);

            _ = await _logs.InsertLog(JsonConvert.SerializeObject(new { ID = Id, RefreshToken = RefreshToken }),
                JsonConvert.SerializeObject(result), "UpdateRefreshToken");

            return result;

        }

        // get auth details from DB
        public async Task<Response<AuthModel>> GetAuthByAccountId(int AccountId, string QBEnv)
        {
            if (AccountId != 0 && !string.IsNullOrEmpty(QBEnv))
            {
                var response = await _qbAuthRepos.GetAuthByAccountId(AccountId, QBEnv);

                AuthModel authModel = new AuthModel
                {
                    ID = response.ID,
                    AccountID = response.AccountID,
                    QBID = response.QBID,
                    ClientID = response.ClientID,
                    ClientSecret = response.ClientSecret,
                    AccessToken = response.AccessToken,
                    RefreshToken = response.RefreshToken,
                    QBEnv = response.QBEnv,
                    Status = response.Status,
                    CreatedDT = response.CreatedDT,
                    ConsumedDT = response.ConsumedDT
                };

                var respData = new Response<AuthModel>(authModel);

                return respData;
            }

            return new Response<AuthModel>() { Success = false, Message = "Something went wrong" };
        }
    }
}
