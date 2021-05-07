using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.PersistModels
{
    [Table(name: "Order", Schema = "dbo")]
    public class OrderPersist
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int AssociateId { get; set; }
        public string SystemNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
