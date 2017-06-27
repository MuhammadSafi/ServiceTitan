using ServiceTitan.Api.Attribute;
using ServiceTitan.Api.Models;
using ServiceTitan.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServiceTitan.Api.Controllers
{
    [RoutePrefix("ServiceTitan")]
    public class ServiceTitanController : ApiController
    {
        
        [ServiceTitanCacheRepository(Duration =5)]
        [HttpGet]
        [Route("JobDescription")]
        public IEnumerable<Job> JobDescription()
        {
            List<Job> lstjob = new List<Job>();
            lstjob.Add(new Job { Price ="100", Location ="USA", Technicians ="Mark", Estimate ="Today", Invoice ="InProcess", Payment ="Done"});
            return lstjob;
        }
    }

   
}
