using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Product
    {
        public string NewServiceId { get; set; }
        public string Title { get; set; }
        public string Validity { get; set; }
        public int DurationInDay { get; set; }
        public double Price { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsRenewal { get; set; }
    }
}
