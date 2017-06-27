using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceTitan.Api.Models
{
    public class Job
    {
        public string Price { get; set; }
        public string Location { get; set; }
        public string Technicians { get; set; }
        public string Estimate { get; set; }
        public string Invoice { get; set; }
        public string Payment { get; set; }

    }
}