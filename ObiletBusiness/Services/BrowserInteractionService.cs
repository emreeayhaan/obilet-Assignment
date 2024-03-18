using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ObiletBusiness.Interfaces;
using ObiletData.Dto;
using System.Net;
using System.Net.Sockets;
using static ObiletData.Dto.SessionRequestDto;

namespace ObiletBusiness.Services
{
    public class BrowserInteractionService : IBrowserInteractionService
    {
        private readonly IRedisCacheManager _cacheBusiness;
        private readonly ICustomHttpClient _httpClient;
        private readonly Options _enviromentOptions;

        public BrowserInteractionService(IOptions<Options> enviromentOptions, IRedisCacheManager cacheBusiness, ICustomHttpClient httpclient)
        {
            _cacheBusiness = cacheBusiness;
            _httpClient = httpclient;
            _enviromentOptions = enviromentOptions.Value;
        }
        public async Task<SessionResponseDto> GetSesion()
        {
            try
            {
                BrowserDataDto browserData = await _cacheBusiness.Get<BrowserDataDto>("browser");
                if (browserData == null)
                {
                    return new SessionResponseDto() { Status = "Fail" };
                }

                SessionRequestDto request = new SessionRequestDto
                {
                    Type = _enviromentOptions.SessionType,
                    ConnectionInfo = new Connection
                    {
                        IpAddress = GetIpAddress(),
                        Port = _enviromentOptions.Port,
                    },
                    BrowserInfo = new Browser
                    {
                        Name = browserData.Name,
                        Version = browserData.Version
                    }
                };

                string ApiUrl = $"{_enviromentOptions.ApiUri}{_enviromentOptions.GetSession}";
                //string ApiUrl = Environment.GetEnvironmentVariable("ApiUr") +Environment.GetEnvironmentVariable("GetSession")";
                //string token = Environment.GetEnvironmentVariable("Token");
                HttpResponseMessage httpResponse = await _httpClient.SendPostRequest(request, ApiUrl, _enviromentOptions.Token);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("HTTP isteği başarısız oldu. Durum kodu: " + httpResponse.StatusCode);
                }

                string responseContent = await httpResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<SessionResponseDto>(responseContent);

                if (response?.Status == "Success")
                {
                    await _cacheBusiness.Set("session", response.Data);
                }

                return response ?? new SessionResponseDto() { Status = "Fail" };
            }
            catch
            {
                return new SessionResponseDto() { Status = "Fail" };
            }

        }
        private string GetIpAddress()
        {
            string hostName = Dns.GetHostName();
            IPAddress[] ipAddresses = Dns.GetHostAddresses(hostName);

            foreach (IPAddress ipAddress in ipAddresses)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ipAddress.ToString();
                }
            }

            return string.Empty;
        }
    }
}
