using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web;
using TwitterApp.Common.Interfaces;

namespace TwitterApp.Common
{
    public class InMemoryCache : ICacheService
    {
        public async Task<T> GetOrSet<T>(string cacheKey, Func<Task<T>> getItemCallback) where T : class
        {
            T item = MemoryCache.Default.Get(cacheKey) as T;
            if (item == null)
            {
                item = await getItemCallback();
                MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddMinutes(10));
            }
            return item;
        }
    }
}