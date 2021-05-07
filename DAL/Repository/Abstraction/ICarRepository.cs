using DAL.PersistModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository.Abstraction
{
    public interface ICarRepository<TDomain> : IRepository<CarPersist, TDomain>
        where TDomain : class
    {
    }
}
