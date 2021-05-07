using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repository.Abstraction
{
    public interface IRepository<TModel, TDomain> 
        where TModel : class
        where TDomain : class
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TModel> Get(Func<TModel, bool> query);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TModel> Get();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void Delete(TDomain model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public TModel Create(TDomain model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public TModel Update(TDomain model);
    }
}
