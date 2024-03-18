using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ObiletBusiness.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ObiletBusiness.Request
{
    public class CustomHttpClient : ICustomHttpClient
    {
        public async Task<HttpResponseMessage> SendPostRequest<T>(T request, string apiUrl, string token) where T : class
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", token);

                    string requestJson = JsonConvert.SerializeObject(request);
                    HttpResponseMessage httpResponse = await client.PostAsync(apiUrl, new StringContent(requestJson, Encoding.UTF8, "application/json"));

                    return httpResponse;
                }
            }

            catch (Exception ex)
            {
                throw new Exception($"POST isteği işlenirken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
