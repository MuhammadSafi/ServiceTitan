using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace ServiceTitan.Caching
{
    public abstract class CacheBase : ICache
    {
        /// <summary>
        /// Get Cache value using key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="_CacheManager"></param>
        /// <returns></returns>
        public object Get(string key, MemoryCache _CacheManager)
        {
            return _CacheManager.Get(key);
        }

        /// <summary>
        /// Check cached value exist against the key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="_CacheManager"></param>
        /// <returns></returns>
        public bool Contains(string key, MemoryCache _CacheManager)
        {
            return _CacheManager.Contains(key);
        }
        /// <summary>
        /// Remove cache using key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="_CacheManager"></param>
        public void Flush(string key, MemoryCache _CacheManager)
        {
            _CacheManager.Remove(key);
        }

        /// <summary>
        /// Set cache with the Predefined Policy
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="policy"></param>
        /// <param name="_CacheManager"></param>
        public void Set(string key, object value, CacheItemPolicy policy, MemoryCache _CacheManager)
        {
            if (_CacheManager.Contains(key))
                _CacheManager.Remove(key);
            this.Add(key, value, policy, _CacheManager);
        }

        /// <summary>
        /// Add cache with the Predefined Policy
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="policy"></param>
        /// <param name="_CacheManager"></param>
        public void Add(string key, object value, CacheItemPolicy policy, MemoryCache _CacheManager)
        {
            _CacheManager.Add(key, value, policy, null);
        }

        /// <summary>
        /// Mehtod to allow thread safe Cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="_CacheManager"></param>
        /// <param name="valueFactory"></param>
        /// <param name="policy"></param>
        /// <returns></returns>
        public object GetItem<T>(string key, MemoryCache _CacheManager, Func<T> valueFactory, CacheItemPolicy policy)
        {
            return AddOrGetExisting(key, _CacheManager, valueFactory, policy);
        }


        protected abstract T AddOrGetExisting<T>(string key, MemoryCache _CacheManager, Func<T> valueFactory, CacheItemPolicy policy);

        protected abstract object InitItem(string key);


    }
}
