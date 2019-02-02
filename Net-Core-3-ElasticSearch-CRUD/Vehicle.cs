using System;
using System.Collections.Generic;
using System.Text;

namespace Net_Core_3_ElasticSearch_CRUD
{
    public class Vehicle
    {
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string Description { get; set; }
        public int TopSpeed { get; set; }
        public double AvgGasUsage { get; set; }
    }
}
