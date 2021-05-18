using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DomainModels
{
    public class CarDomain
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Code { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public int EngineVolume { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
