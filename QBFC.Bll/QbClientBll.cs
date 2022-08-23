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

        public async Task<Response<object>> GetCompanyInfo()
        {
            var oauthClient = AuthClient();

            var oTokens = GetTokens();

            var respToken = await oauthClient.RefreshTokenAsync(oTokens.refreshToken);

            if (!string.IsNullOrEmpty(respToken.AccessToken) || !string.IsNullOrEmpty(respToken.RefreshToken))
            {
                var uri = $"{_baseUrl}/v3/company/{oTokens.RealmId}/companyinfo/{oTokens.RealmId}?minorversion=63";

                SetTokens(respToken.AccessToken, respToken.RefreshToken, setConsumedDt: true);

                var qbResp = await _httpClient.HttpGet(uri, respToken.AccessToken);

                var respData = JsonConvert.DeserializeObject(qbResp);

                var response = new Response<object>(respData);

                _ = await InsertLog(string.Empty, JsonConvert.SerializeObject(response), $"GetCompanyInfo {uri}");

                return response;
            }
            else
            {
                var response = new Response<object>() { Success = false, Message = "Authentication Token Issue" };

                _ = await InsertLog(string.Empty, JsonConvert.SerializeObject(response), $"GetCompanyInfo");

                return response;
            }
        }

        public async Task<Response<object>> GetCustomerById(int id)
        {
            var oauthClient = AuthClient();

            var oTokens = GetTokens();

            var respToken = await oauthClient.RefreshTokenAsync(oTokens.refreshToken);

            if (!string.IsNullOrEmpty(respToken.AccessToken) || !string.IsNullOrEmpty(respToken.RefreshToken))
            {
                var uri = $"{_baseUrl}/v3/company/{oTokens.RealmId}/customer/{id}?minorversion=65";

                SetTokens(respToken.AccessToken, respToken.RefreshToken, setConsumedDt: true);

                var qbResp = await _httpClient.HttpGet(uri, respToken.AccessToken);

                var respData = JsonConvert.DeserializeObject(qbResp);

                var response = new Response<object>(respData);

                _ = await InsertLog(JsonConvert.SerializeObject(id), JsonConvert.SerializeObject(response), $"GetCustomerById {uri}");

                return response;
            }
            else
            {
                var response = new Response<object>() { Success = false, Message = "Authentication Token Issue" };

                _ = await InsertLog(JsonConvert.SerializeObject(id), JsonConvert.SerializeObject(response), $"GetCustomerById", false);

                return response;
            }
        }

        public async Task<Response<object>> GetVendorById(int id)
        {
            var oauthClient = AuthClient();

            var oTokens = GetTokens();

            var respToken = await oauthClient.RefreshTokenAsync(oTokens.refreshToken);

            if (!string.IsNullOrEmpty(respToken.AccessToken) || !string.IsNullOrEmpty(respToken.RefreshToken))
            {
                var uri = $"{_baseUrl}/v3/company/{oTokens.RealmId}/vendor/{id}?minorversion=65";

                SetTokens(respToken.AccessToken, respToken.RefreshToken, setConsumedDt: true);

                var qbResp = await _httpClient.HttpGet(uri, respToken.AccessToken);

                var respData = JsonConvert.DeserializeObject(qbResp);

                var response = new Response<object>(respData);

                _ = await InsertLog(JsonConvert.SerializeObject(id), JsonConvert.SerializeObject(response), $"GetVendorById {uri}");

                return response;
            }
            else
            {
                var response = new Response<object>() { Success = false, Message = "Authentication Token Issue" };

                _ = await InsertLog(JsonConvert.SerializeObject(id), JsonConvert.SerializeObject(response), $"GetVendorById", false);

                return response;
            }
        }

        public async Task<Response<object>> GetAccountById(int id)
        {
            var oauthClient = AuthClient();

            var oTokens = GetTokens();

            var respToken = await oauthClient.RefreshTokenAsync(oTokens.refreshToken);

            if (!string.IsNullOrEmpty(respToken.AccessToken) || !string.IsNullOrEmpty(respToken.RefreshToken))
            {
                var uri = $"{_baseUrl}/v3/company/{oTokens.RealmId}/account/{id}?minorversion=65";

                SetTokens(respToken.AccessToken, respToken.RefreshToken, setConsumedDt: true);

                var qbResp = await _httpClient.HttpGet(uri, respToken.AccessToken);

                var respData = JsonConvert.DeserializeObject(qbResp);

                var response = new Response<object>(respData);

                _ = await InsertLog(JsonConvert.SerializeObject(id), JsonConvert.SerializeObject(response), $"GetAccountById {uri}");

                return response;
            }
            else
            {
                var response = new Response<object>() { Success = false, Message = "Authentication Token Issue" };

                _ = await InsertLog(JsonConvert.SerializeObject(id), JsonConvert.SerializeObject(response), $"GetAccountById", false);

                return response;
            }
        }

        public async Task<Response<object>> CreateBill(string content)
        {
            var oauthClient = AuthClient();

            var oTokens = GetTokens();

            var respToken = await oauthClient.RefreshTokenAsync(oTokens.refreshToken);

            if (!string.IsNullOrEmpty(respToken.AccessToken) || !string.IsNullOrEmpty(respToken.RefreshToken))
            {
                var uri = $"{_baseUrl}/v3/company/{oTokens.RealmId}/bill?minorversion=65";

                SetTokens(respToken.AccessToken, respToken.RefreshToken, setConsumedDt: true);

                var qbResp = await _httpClient.HttpPost(uri, respToken.AccessToken, content);

                var respData = JsonConvert.DeserializeObject(qbResp);

                var response = new Response<object>(respData);

                _ = await InsertLog(content, JsonConvert.SerializeObject(response), $"CreateBill {uri}");

                return response;
            }
            else
            {
                var response = new Response<object>() { Success = false, Message = "Authentication Token Issue" };

                _ = await InsertLog(content, JsonConvert.SerializeObject(response), $"CreateBill", false);

                return response;
            }
        }

        public async Task<Response<object>> GetBillById(int id)
        {
            var oauthClient = AuthClient();

            var oTokens = GetTokens();

            var respToken = await oauthClient.RefreshTokenAsync(oTokens.refreshToken);

            if (!string.IsNullOrEmpty(respToken.AccessToken) || !string.IsNullOrEmpty(respToken.RefreshToken))
            {
                var uri = $"{_baseUrl}/v3/company/{oTokens.RealmId}/bill/{id}?minorversion=65";

                SetTokens(respToken.AccessToken, respToken.RefreshToken, setConsumedDt: true);

                var qbResp = await _httpClient.HttpGet(uri, respToken.AccessToken);

                var respData = JsonConvert.DeserializeObject(qbResp);

                var response = new Response<object>(respData);

                _ = await InsertLog(JsonConvert.SerializeObject(id), JsonConvert.SerializeObject(response), $"GetBillById {uri}");

                return response;
            }
            else
            {
                var response = new Response<object>() { Success = false, Message = "Authentication Token Issue" };

                _ = await InsertLog(JsonConvert.SerializeObject(id), JsonConvert.SerializeObject(response), $"GetBillById", false);

                return response;
            }
        }

        public async Task<string> GetByQuery(string query)
        {
            var oauthClient = AuthClient();

            var oTokens = GetTokens();

            var respToken = await oauthClient.RefreshTokenAsync(oTokens.refreshToken);

            if (!string.IsNullOrEmpty(respToken.AccessToken) || !string.IsNullOrEmpty(respToken.RefreshToken))
            {
                var uri = $"{_baseUrl}/v3/company/{oTokens.RealmId}/query?query={query}";

                SetTokens(respToken.AccessToken, respToken.RefreshToken, setConsumedDt: true);

                var response = await _httpClient.HttpGet(uri, respToken.AccessToken);

                _ = await InsertLog(JsonConvert.SerializeObject(query), JsonConvert.SerializeObject(response), $"GetByQuery {uri}");

                return response;
            }

            _ = await InsertLog(JsonConvert.SerializeObject(query), JsonConvert.SerializeObject(string.Empty), "GetByQuery", false);

            return "";
        }

        public async Task<string> PulseCheck()
        {
            var oauthClient = AuthClient();

            var oTokens = GetTokens();

            var response = await oauthClient.RefreshTokenAsync(oTokens.refreshToken);

            if (string.IsNullOrEmpty(response.AccessToken) || string.IsNullOrEmpty(response.RefreshToken))
            {
                _ = await InsertLog(string.Empty, JsonConvert.SerializeObject(response), "PulseCheck", false);

                return null;
            }
            else if (SetTokens(response.AccessToken, response.RefreshToken, setConsumedDt: true))
            {
                _ = await InsertLog(string.Empty, JsonConvert.SerializeObject(response), "PulseCheck");

                return "Pulse Checked !!";
            }
            else
            {
                return null;
            }
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
