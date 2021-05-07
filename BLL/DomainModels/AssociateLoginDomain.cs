using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DomainModels
{
    public class AssociateLoginDomain
    {
        public int Id { get; set; }
        public int AssociateId { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
    }
}
