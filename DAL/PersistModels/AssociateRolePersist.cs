using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.PersistModels
{
    [Table(name: "AssociateRole", Schema = "dbo")]
    public class AssociateRolePersist
    {
        public int Id { get; set; }
        public int AssociateId { get; set; }
        public int RoleId { get; set; }
    }
}
