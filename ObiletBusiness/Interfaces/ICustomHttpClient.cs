using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletBusiness.Interfaces
{
    public interface ICustomHttpClient
    {
        Task<HttpResponseMessage> SendPostRequest<T>(T request, string apiUrl, string token) where T : class;
    }
}
