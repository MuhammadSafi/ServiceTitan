using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;


namespace ServiceTitan.Caching
{
    public interface  ICache
    {
        //Thread safe Get Method
        object GetItem<T>(string key, MemoryCache _CacheManager, Func<T> valueFactory, CacheItemPolicy policy);
        bool Contains(string key, MemoryCache _CacheManager);
        void Flush(string key, MemoryCache _CacheManager);
        void Set(string key, object value, CacheItemPolicy policy, MemoryCache _CacheManager);
        
    }
    
    
}
