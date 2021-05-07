
using DAL.PersistModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository.Abstraction
{
    public interface IAssociateRepository<TDomain> : IRepository<AssociatePersist, TDomain>
        where TDomain : class
    {
    }
}
