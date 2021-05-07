using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.PersistModels
{
    [Table(name: "Car", Schema = "dbo")]
    public class CarPersist
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public int EngineVolume { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
