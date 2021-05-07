using DAL.PersistModels;
using DAL.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository.Implementation
{
    public class AssociateLoginRepository<TDomain> : RepositoryBase<AssociateLoginPersist, TDomain>, IAssociateLoginRepository<TDomain>
        where TDomain : class
    {
        public AssociateLoginRepository()
        {

        }
    }
}
