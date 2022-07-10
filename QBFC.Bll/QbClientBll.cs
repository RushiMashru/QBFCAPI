using Intuit.Ipp.OAuth2PlatformClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using QBFC.Bll.Base;
using QBFC.Models.ViewModel;
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
        public QbClientBll(IConfiguration configuration, IHttpClientBll httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
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

                return response;
            }
            else
            {
                var response = new Response<object>() { Success = false, Message = "Authentication Token Issue" };

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

                return response;
            }
            else
            {
                var response = new Response<object>() { Success = false, Message = "Authentication Token Issue" };

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

                return response;
            }
            else
            {
                var response = new Response<object>() { Success = false, Message = "Authentication Token Issue" };

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

                return response;
            }
            else
            {
                var response = new Response<object>() { Success = false, Message = "Authentication Token Issue" };

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

                return response;
            }
            else
            {
                var response = new Response<object>() { Success = false, Message = "Authentication Token Issue" };

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

                return response;
            }
            else
            {
                var response = new Response<object>() { Success = false, Message = "Authentication Token Issue" };

                return response;
            }
        }

        public async Task<Response<object>> GetByQuery(string query)
        {
            var oauthClient = AuthClient();

            var oTokens = GetTokens();

            var respToken = await oauthClient.RefreshTokenAsync(oTokens.refreshToken);

            if (!string.IsNullOrEmpty(respToken.AccessToken) || !string.IsNullOrEmpty(respToken.RefreshToken))
            {
                var uri = $"{_baseUrl}/v3/company/{oTokens.RealmId}/query?query={query}";

                SetTokens(respToken.AccessToken, respToken.RefreshToken, setConsumedDt: true);

                var qbResp = await _httpClient.HttpGet(uri, respToken.AccessToken);

                var respData = JsonConvert.DeserializeObject(qbResp);

                var response = new Response<object>(respData);

                return response;
            }
            else
            {
                var response = new Response<object>() { Success = false, Message = "Authentication Token Issue" };

                return response;
            }
        }

        public async Task<string> PulseCheck()
        {
            var oauthClient = AuthClient();

            var oTokens = GetTokens();

            var response = await oauthClient.RefreshTokenAsync(oTokens.refreshToken);

            if (string.IsNullOrEmpty(response.AccessToken) || string.IsNullOrEmpty(response.RefreshToken))
            {
                return null;
            }
            else if (SetTokens(response.AccessToken, response.RefreshToken, setConsumedDt: true))
            {
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
                return false;
            }
            else
            {
                return SetTokens(response.AccessToken, response.RefreshToken, setCreatedDt: true);
            }
        }

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

    }
}
