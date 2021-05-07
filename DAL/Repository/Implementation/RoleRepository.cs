using DAL.PersistModels;
using DAL.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository.Implementation
{
    public class RoleRepository<TDomain> : RepositoryBase<RolePersist, TDomain>, IRoleRepository<TDomain>
        where TDomain : class
    {
        public RoleRepository() 
        {

        }
    }
}
