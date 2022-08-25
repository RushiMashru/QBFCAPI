using Intuit.Ipp.OAuth2PlatformClient;
using Microsoft.Extensions.Configuration;
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
    public class QbClientBll : IQbClientBll
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientBll _httpClient;
        private readonly string _env;
        private readonly string _baseUrl;
        private readonly IQbLogsRepos _qbRepos;
        private readonly IQbAuthRepos _qbAuthRepos;

        public QbClientBll(IConfiguration configuration, IHttpClientBll httpClient, IQbLogsRepos qbRepos, IQbAuthRepos qbAuthRepos)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _qbRepos = qbRepos;
            _qbAuthRepos = qbAuthRepos;
            _env = _configuration.GetSection("QbEnv").Value;
            _baseUrl = _env == "sandbox" ? _configuration.GetSection("QbBaseSandUrl").Value : _configuration.GetSection("QbBaseUrl").Value;
        }

        private OAuth2Client AuthClient()
        {
            var clientId = _configuration.GetSection("ClientId").Value;
            var clientSecret = _configuration.GetSection("ClientSecret").Value;
            var redirectUrl = _configuration.GetSection("RedirectUrl").Value;

            OAuth2Client oauthClient = new OAuth2Client(clientId, clientSecret, redirectUrl, _env);

            return oauthClient;
        }

        private async Task<(tAuthDetails AuthDetails, bool IsSuccess)> AuthClient(int AccountId)
        {
            var redirectUrl = _configuration.GetSection("RedirectUrl").Value;

            var oAuthDetails = await _qbAuthRepos.GetAuthByAccountId(AccountId, _env);

            var clientId = oAuthDetails?.ClientID;

            var clientSecret = oAuthDetails?.ClientSecret;

            OAuth2Client oauthClient = new OAuth2Client(clientId, clientSecret, redirectUrl, _env);

            var respToken = await oauthClient.RefreshTokenAsync(oAuthDetails.RefreshToken);

            bool isSuccess = false;

            var log_auth = new
            {
                AccountId = AccountId,
                ClientId = clientId,
                ClientSecret = clientSecret,
                RedirectUrl = redirectUrl,
                Env = _env
            };

            if (!string.IsNullOrEmpty(respToken.AccessToken) || !string.IsNullOrEmpty(respToken.RefreshToken))
            {
                oAuthDetails.AccessToken = respToken.AccessToken;
                oAuthDetails.RefreshToken = respToken.RefreshToken;
                oAuthDetails.ConsumedDT = DateTime.Now;
                _ = await _qbAuthRepos.UpsertAuthDetails(oAuthDetails);
                _ = await InsertLog(JsonConvert.SerializeObject(log_auth), JsonConvert.SerializeObject(respToken), $"AuthClient");
                isSuccess = true;
            }

            _ = await InsertLog(JsonConvert.SerializeObject(log_auth), JsonConvert.SerializeObject(respToken), $"AuthClient", false);

            return (oAuthDetails, isSuccess);
        }

        public async Task<Response<object>> GetCompanyInfo(int AccountId)
        {
            var (AuthDetails, IsSuccess) = await AuthClient(AccountId);

            if (AuthDetails != null && IsSuccess)
            {
                var uri = $"{_baseUrl}/v3/company/{AuthDetails.QBID}/companyinfo/{AuthDetails.QBID}?minorversion=63";

                var qbResp = await _httpClient.HttpGet(uri, AuthDetails.AccessToken);

                var respData = JsonConvert.DeserializeObject(qbResp);

                var response = new Response<object>(respData);

                _ = await InsertLog(string.Empty, JsonConvert.SerializeObject(response), $"GetCompanyInfo AccountId: {AccountId} Uri: {uri}");

                return response;
            }
            else
            {
                var response = new Response<object>() { Success = false, Message = "Authentication Token Issue" };

                _ = await InsertLog(string.Empty, JsonConvert.SerializeObject(response), $"GetCompanyInfo AccountId: {AccountId}", false);

                return response;
            }
        }

        public async Task<Response<object>> GetCustomerById(int CustomerId, int AccountId)
        {
            var (AuthDetails, IsSuccess) = await AuthClient(AccountId);

            if (AuthDetails != null && IsSuccess)
            {
                var uri = $"{_baseUrl}/v3/company/{AuthDetails.QBID}/customer/{CustomerId}?minorversion=65";

                var qbResp = await _httpClient.HttpGet(uri, AuthDetails.AccessToken);

                var respData = JsonConvert.DeserializeObject(qbResp);

                var response = new Response<object>(respData);

                _ = await InsertLog(JsonConvert.SerializeObject(CustomerId), JsonConvert.SerializeObject(response), $"GetCustomerById AccountId: {AccountId} Uri: {uri}");

                return response;
            }
            else
            {
                var response = new Response<object>() { Success = false, Message = "Authentication Token Issue" };

                _ = await InsertLog(JsonConvert.SerializeObject(CustomerId), JsonConvert.SerializeObject(response), $"GetCustomerById AccountId: {AccountId}", false);

                return response;
            }
        }

        public async Task<Response<object>> GetVendorById(int VendorId, int AccountId)
        {
            var (AuthDetails, IsSuccess) = await AuthClient(AccountId);

            if (AuthDetails != null && IsSuccess)
            {
                var uri = $"{_baseUrl}/v3/company/{AuthDetails.QBID}/vendor/{VendorId}?minorversion=65";

                var qbResp = await _httpClient.HttpGet(uri, AuthDetails.AccessToken);

                var respData = JsonConvert.DeserializeObject(qbResp);

                var response = new Response<object>(respData);

                _ = await InsertLog(JsonConvert.SerializeObject(VendorId), JsonConvert.SerializeObject(response), $"GetVendorById AccountId: {AccountId} Uri: {uri}");

                return response;
            }
            else
            {
                var response = new Response<object>() { Success = false, Message = "Authentication Token Issue" };

                _ = await InsertLog(JsonConvert.SerializeObject(VendorId), JsonConvert.SerializeObject(response), $"GetVendorById AccountId: {AccountId}", false);

                return response;
            }
        }

        public async Task<Response<object>> GetExpenceAccountById(int ExpenceAccountId, int AccountId)
        {
            var (AuthDetails, IsSuccess) = await AuthClient(AccountId);

            if (AuthDetails != null && IsSuccess)
            {
                var uri = $"{_baseUrl}/v3/company/{AuthDetails.QBID}/account/{ExpenceAccountId}?minorversion=65";

                var qbResp = await _httpClient.HttpGet(uri, AuthDetails.AccessToken);

                var respData = JsonConvert.DeserializeObject(qbResp);

                var response = new Response<object>(respData);

                _ = await InsertLog(JsonConvert.SerializeObject(ExpenceAccountId), JsonConvert.SerializeObject(response), $"GetAccountById AccountId: {AccountId} Uri: {uri}");

                return response;
            }
            else
            {
                var response = new Response<object>() { Success = false, Message = "Authentication Token Issue" };

                _ = await InsertLog(JsonConvert.SerializeObject(ExpenceAccountId), JsonConvert.SerializeObject(response), $"GetAccountById AccountId: {AccountId}", false);

                return response;
            }
        }

        public async Task<Response<object>> CreateBill(string content, int AccountId)
        {
            var (AuthDetails, IsSuccess) = await AuthClient(AccountId);

            if (AuthDetails != null && IsSuccess)
            {
                var uri = $"{_baseUrl}/v3/company/{AuthDetails.QBID}/bill?minorversion=65";

                var qbResp = await _httpClient.HttpPost(uri, AuthDetails.AccessToken, content);

                var respData = JsonConvert.DeserializeObject(qbResp);

                var response = new Response<object>(respData);

                _ = await InsertLog(content, JsonConvert.SerializeObject(response), $"CreateBill Id: {AccountId} Uri: {uri}");

                return response;
            }
            else
            {
                var response = new Response<object>() { Success = false, Message = "Authentication Token Issue" };

                _ = await InsertLog(content, JsonConvert.SerializeObject(response), $"CreateBill Id: {AccountId}", false);

                return response;
            }
        }

        public async Task<Response<object>> GetBillById(int BillId, int AccountId)
        {
            var (AuthDetails, IsSuccess) = await AuthClient(AccountId);

            if (AuthDetails != null && IsSuccess)
            {
                var uri = $"{_baseUrl}/v3/company/{AuthDetails.QBID}/bill/{BillId}?minorversion=65";

                var qbResp = await _httpClient.HttpGet(uri, AuthDetails.AccessToken);

                var respData = JsonConvert.DeserializeObject(qbResp);

                var response = new Response<object>(respData);

                _ = await InsertLog(JsonConvert.SerializeObject(BillId), JsonConvert.SerializeObject(response), $"GetBillById AccountId:{AccountId} Uri: {uri}");

                return response;
            }
            else
            {
                var response = new Response<object>() { Success = false, Message = "Authentication Token Issue" };

                _ = await InsertLog(JsonConvert.SerializeObject(BillId), JsonConvert.SerializeObject(response), $"GetBillById AccountId: {AccountId}", false);

                return response;
            }
        }

        public async Task<string> GetByQuery(string query, int AccountId)
        {
            var (AuthDetails, IsSuccess) = await AuthClient(AccountId);

            if (AuthDetails != null && IsSuccess)
            {
                var uri = $"{_baseUrl}/v3/company/{AuthDetails.QBID}/query?query={query}";

                var response = await _httpClient.HttpGet(uri, AuthDetails.AccessToken);

                _ = await InsertLog(JsonConvert.SerializeObject(query), JsonConvert.SerializeObject(response), $"GetByQuery AccountId: {AccountId} Uri: {uri}");

                return response;
            }

            _ = await InsertLog(JsonConvert.SerializeObject(query), JsonConvert.SerializeObject(string.Empty), $"GetByQuery AccountId: {AccountId}", false);

            return "";
        }

        public async Task<string> PulseCheck(int AccountId)
        {
            var (AuthDetails, IsSuccess) = await AuthClient(AccountId);

            if (AuthDetails != null && IsSuccess)
            {
                _ = await InsertLog(string.Empty, JsonConvert.SerializeObject(AuthDetails), $"PulseCheck AccountId: {AccountId}");
                return "Pulse Checked !!";
            }
            return null;
        }

        public async Task<bool> SetRToken(string rtoken)
        {
            var oauthClient = AuthClient();

            var response = await oauthClient.RefreshTokenAsync(rtoken);

            if (string.IsNullOrEmpty(response.AccessToken) || string.IsNullOrEmpty(response.RefreshToken))
            {
                _ = await InsertLog(string.Empty, JsonConvert.SerializeObject(response), "SetRToken", false);

                return false;
            }
            else
            {
                _ = await InsertLog(JsonConvert.SerializeObject(rtoken), JsonConvert.SerializeObject(response), "SetRToken");

                return SetTokens(response.AccessToken, response.RefreshToken, setCreatedDt: true);
            }
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

                _ = await InsertLog(JsonConvert.SerializeObject(otAuthDetails), JsonConvert.SerializeObject(result), "UpsertAuthDetails");

                return result;

            }

            _ = await InsertLog(JsonConvert.SerializeObject(authModel), JsonConvert.SerializeObject(0), "UpsertAuthDetails", false);

            return 0;
        }

        // update refresh token in DB
        public async Task<int> UpdateRefreshToken(int Id, string RefreshToken)
        {
            var result = await _qbAuthRepos.UpdateRefreshToken(Id, RefreshToken);

            _ = await InsertLog(JsonConvert.SerializeObject(new { ID = Id, RefreshToken = RefreshToken }),
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



        // helper methods
        private (string accessToken, string refreshToken, string RealmId) GetTokens()
        {
            string json = System.IO.File.ReadAllText("auth.json");

            dynamic jsonObj = JsonConvert.DeserializeObject(json);

            return (jsonObj["access_token"], jsonObj["refresh_token"], jsonObj["id"]);

        }

        private bool SetTokens(string accessToken, string refreshToken, bool setConsumedDt = false, bool setCreatedDt = false)
        {
            try
            {
                string json = System.IO.File.ReadAllText("auth.json");

                dynamic jsonObj = JsonConvert.DeserializeObject(json);

                jsonObj["access_token"] = accessToken;
                jsonObj["refresh_token"] = refreshToken;

                if (setConsumedDt)
                    jsonObj["consumed_dt"] = DateTime.Now.ToString();

                if (setCreatedDt)
                    jsonObj["created_dt"] = DateTime.Now.ToString();

                string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);

                System.IO.File.WriteAllText("auth.json", output);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private async Task<int> InsertLog(string request, string response, string message, bool isSuccess = true)
        {
            var result = await _qbRepos.InsertLogs(new tAPILogs
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
