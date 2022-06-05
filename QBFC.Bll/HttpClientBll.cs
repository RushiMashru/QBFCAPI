using QBFC.Bll.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QBFC.Bll
{
    public class HttpClientBll : IHttpClientBll
    {
        public async Task<string> HttpGet(string uri, string authToken)
        {
            try
            {
                var client = new HttpClient();

                client.DefaultRequestHeaders.Add("authorization", $"Bearer {authToken}");
                client.DefaultRequestHeaders.Add("accept", "application/json");

                HttpResponseMessage response = await client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                var responseData = await response.Content.ReadAsStringAsync();
                return responseData;

            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> HttpPost(string uri, string authToken, string data)
        {
            try
            {
                var client = new HttpClient();

                client.DefaultRequestHeaders.Add("authorization", $"Bearer {authToken}");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);
                response.EnsureSuccessStatusCode();
                var responseData = await response.Content.ReadAsStringAsync();
                return responseData;

            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
