using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DomainModels
{
    public class AssociateRoleDomain
    {
        public int Id { get; set; }
        public int AssociateId { get; set; }
        public int RoleId { get; set; }
    }
}
