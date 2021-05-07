using DAL.PersistModels;
using DAL.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository.Implementation
{
    public class AssociateRoleRepository<TDomain> : RepositoryBase<AssociateRolePersist, TDomain>, IAssociateRoleRepository<TDomain>
        where TDomain : class
    {
        public AssociateRoleRepository() 
        {

        }
    }
}
