using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ServiceTitan.Caching;
using System.Runtime.Caching;
using System.Net.Http.Headers;

namespace ServiceTitan.Api.Attribute
{
    public class ServiceTitanCacheRepositoryAttribute : ActionFilterAttribute
    {
        public int Duration { get; set; }
        private string _cachekey { get; set; }
        //Initializing cache repository
        private static MemoryCache _cacheManager = new MemoryCache("ServiceTitanCache");
        Cache cache = new Cache();
        private readonly ICacheKey client;

        public ServiceTitanCacheRepositoryAttribute() : this(new CacheKeyBuilder())
        {

        }

        /// <summary>
        /// Contructor Overloading
        /// </summary>
        /// <param name="client"></param>
        public ServiceTitanCacheRepositoryAttribute(ICacheKey client)
        {

            this.client = client;
        }

        /// <summary>
        ///  Checking Cache duration and verify the reuqest is GET
        /// </summary>
        /// <param name="ac"></param>
        /// <returns></returns>
        private bool _isCacheable(HttpActionContext ac)
        {
            if (Duration > 0)
            {

                if (ac.Request.Method == HttpMethod.Get) return true;
            }
            else
            {
                throw new InvalidOperationException("Wrong Arguments");
            }
            return false;
        }


        public override void OnActionExecuting(HttpActionContext ac)
        {
            if (ac != null)
            {
                if (_isCacheable(ac))
                {
                    //if token header present it will join it in a string.in this case it is null
                    // for the key absolute path will be the unique for every api request making sure every key for cache item is unique
                    _cachekey = string.Join(":", client.By(ac.Request.RequestUri.AbsolutePath.Replace("/","-")), Convert.ToString(ac.Request.Headers.Authorization));
                    //check if value exist in cache against specified key
                    if (cache.Contains(_cachekey, _cacheManager))
                    {
                        var val = (string)cache.Get(_cachekey, _cacheManager);
                        if (val != null)
                        {
                            ac.Response = ac.Request.CreateResponse();
                            ac.Response.Content = new StringContent(val);
                            return;
                        }

                    }

                    //***************************************************************************************************
                    //This is the implementaiton for Thread safe Cache handled by Memcache using Lazy<T>
                    //***************************************************************************************************


                    //var cacheItem = cache.GetItem(_cachekey, _cacheManager, () =>
                    //   {
                    //       return "I've solved this problem";
                    //   }, new CacheItemPolicy
                    //   {

                    //       SlidingExpiration = TimeSpan.FromMinutes(Duration)
                    //   });

                    //*********************************************END Thread Safe Cache**********************************

                }
            }
            else
            {
                throw new ArgumentNullException("actionContext");
            }

        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {

            if (!(cache.Contains(_cachekey, _cacheManager)))
            {
                var body = actionExecutedContext.Response.Content.ReadAsStringAsync().Result;
              
                cache.Set(_cachekey,
                          body,
                          new CacheItemPolicy { SlidingExpiration = TimeSpan.FromMinutes(Duration) },
                         _cacheManager);

            }

        }
    }
}