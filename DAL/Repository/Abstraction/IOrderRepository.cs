using DAL.PersistModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository.Abstraction
{
    public interface IOrderRepository<TDomain> : IRepository<OrderPersist, TDomain>
        where TDomain : class
    {
    }
}
