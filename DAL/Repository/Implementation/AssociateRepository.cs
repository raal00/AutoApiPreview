using DAL.PersistModels;
using DAL.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository.Implementation
{
    public class AssociateRepository<TDomain> : RepositoryBase<AssociatePersist, TDomain>, IAssociateRepository<TDomain>
        where TDomain : class
    {
        public AssociateRepository()
        {

        }
}
}
