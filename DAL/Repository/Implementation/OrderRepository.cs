using DAL.PersistModels;
using DAL.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository.Implementation
{
    public class OrderRepository<TDomain> : RepositoryBase<OrderPersist, TDomain>, IOrderRepository<TDomain>
        where TDomain : class
    {
        public OrderRepository() 
        {

        }
    }
}
