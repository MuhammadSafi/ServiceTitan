using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTitan.Caching
{
    public sealed class Cache : CacheBase
    {
        //Getting or adding a cache item is a thread-safe, 
        //atomic operation with the locking 
        //implementation handled within MemoryCache
        //locking implementation handled within Lazy<T>.
        protected override T AddOrGetExisting<T>(string key, MemoryCache _CacheManager, Func<T> valueFactory, CacheItemPolicy policy)
        {
           
            var newValue = new Lazy<T>(valueFactory);
            var oldValue = _CacheManager.AddOrGetExisting(key, newValue, policy) as Lazy<T>;
            try
            {
                return (oldValue ?? newValue).Value;
            }
            catch
            {
                // Handle cached lazy exception by evicting from cache.
                _CacheManager.Remove(key);
                throw;
            }
        }

        protected override object InitItem(string key)
        {
            //// Do something expensive to initialize item
            return new { Value = key.ToUpper() };
        }




    }
}
