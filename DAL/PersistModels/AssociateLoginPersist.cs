using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.PersistModels
{
    [Table(name: "AssociateLogin", Schema = "dbo")]
    public class AssociateLoginPersist
    {
        public int Id { get; set; }
        public int AssociateId { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
    }
}
