using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.PersistModels
{
    [Table(name: "Role", Schema = "dbo")]
    public class RolePersist
    {
        public int Id { get; set; }
        public string RoleDisplayName { get; set; }
        public string RoleSystemName { get; set; }
    }
}
