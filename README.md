This Solution Consist of three project
A) ServiceTitan Console Application
B)ServiceTitan.Api
C)ServiceTitan.Caching
Console Application is just used to call the web api.
in console application we can call our thread safe method as well.but we need to make some changes i:e minor changes
ServiceTitan.API is just have one controller as ServiceTitanController and also i have included one class name as   "ServiceTitanCacheRepository" inherit with ActionFilterAttribute.so we can use ServiceTitanCacheRepository Attribute to mark the cache   duraiton for the method. i:e [ServiceTitanCacheRepository(Duration =5)]
ServiceTitan.Caching is another project contains caching base type.
There is one class inside ServiceTitan.Caching i:e cachepolicy although i am not using it but we can make use of it in many cases.
This project consist of 1 interface i:e ICache which is inherit by CacheBase implement it mehtod .and it also has two abstract mehthod that is implemented by the class inherit with CacheBase. i:e Cache.
Cache Key builder is used to create the unique key for the request.
 
