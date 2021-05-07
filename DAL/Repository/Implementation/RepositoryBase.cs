using DAL.Converters;
using DAL.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Implementation
{
    public class RepositoryBase<TModel, TDomain> : IRepository<TModel, TDomain> 
        where TModel : class
        where TDomain : class
    {
        public DbContext Context { get; set; }
        public DbSet<TModel> DBSet { get; set; }

        private readonly DomainToPersistConverter<TModel, TDomain> converter;

        public RepositoryBase()
        {
            Context = DAL.PersistModels.AppContext.GetInstance();
            DBSet = Context.Set<TModel>();
            converter = new DomainToPersistConverter<TModel, TDomain>();
        }

        public TModel Create(TDomain model)
        {
            var persist = converter.ConvertToPersist(model);
            converter.ConvertToPersist(model, ref persist);
            persist = DBSet.Add(persist).Entity;
            Context.SaveChanges();
            return persist;
        }

        public void Delete(TDomain model)
        {
            var persist = converter.ConvertToPersist(model);
            converter.ConvertToPersist(model, ref persist);
            DBSet.Remove(persist);
        }

        public TModel Update(TDomain model)
        {
            var persist = converter.ConvertToPersist(model);
            persist = DBSet.Update(persist).Entity;
            return persist;
        }

        public IEnumerable<TModel> Get(Func<TModel, bool> query)
        {
            return query != null ? DBSet.Where(query) : DBSet;
        }

        public IEnumerable<TModel> Get()
        {
            return DBSet;
        }
    }
}
