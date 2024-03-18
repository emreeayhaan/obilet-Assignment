using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletBusiness.Interfaces
{
    public interface IRedisCacheManager
    {
        Task<T> Get<T>(string key);
        Task Set<T>(string key, T value);
    }
}
