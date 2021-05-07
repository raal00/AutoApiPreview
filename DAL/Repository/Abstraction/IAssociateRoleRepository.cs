using DAL.PersistModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository.Abstraction
{
    public interface IAssociateRoleRepository<TDomain> : IRepository<AssociateRolePersist, TDomain>
        where TDomain : class
    {
    }
}
