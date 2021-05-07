using DAL.PersistModels;
using DAL.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository.Implementation
{
    public class CarRepository<TDomain> : RepositoryBase<CarPersist, TDomain>, ICarRepository<TDomain>
        where TDomain : class
    {
        public CarRepository()
        {

        }
    }
}
