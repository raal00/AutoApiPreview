using DAL.PersistModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository.Abstraction
{
    public interface IRoleRepository<TDomain> : IRepository<RolePersist, TDomain>
        where TDomain : class
    {
    }
}
