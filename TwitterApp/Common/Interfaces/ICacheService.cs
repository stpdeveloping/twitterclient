using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterApp.Common.Interfaces
{
    interface ICacheService
    {
        Task<T> GetOrSet<T>(string cacheKey, Func<Task<T>> getItemCallback) where T : class;
    }
}
