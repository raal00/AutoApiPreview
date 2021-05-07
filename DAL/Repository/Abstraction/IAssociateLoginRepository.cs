using DAL.PersistModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository.Abstraction
{
    public interface IAssociateLoginRepository<TDomain> : IRepository<AssociateLoginPersist, TDomain>
        where TDomain : class
    {
    }
}
