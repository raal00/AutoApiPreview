using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DomainModels
{
    public class OrderDomain
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string CarModel { get; set; }
        public int AssociateId { get; set; }
        public string SystemNumber { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
