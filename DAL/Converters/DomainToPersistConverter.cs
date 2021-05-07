using DAL.Converters.Abstraction;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DAL.Converters
{
    public class DomainToPersistConverter<TPersist, TDomain> : IDomainToPersistConverter<TPersist, TDomain>
        where TPersist : class 
        where TDomain : class
    {
        public void ConvertToPersist(TDomain domain, ref TPersist persist)
        {
            object domainObj = domain;
            object persistObj = persist;

            Type domainType = domainObj.GetType();
            Type persistType = persistObj.GetType();

            List<PropertyInfo> persistFields = persistType.GetProperties().ToList();
            List<PropertyInfo> domainFields = domainType.GetProperties().ToList();
            foreach (PropertyInfo fi in domainFields)
            {
                var persistEqualField = persistFields.Where(x => x.Name == fi.Name).FirstOrDefault();
                if (persistEqualField != null)
                {
                    persistEqualField.SetValue(persistObj, fi.GetValue(domainObj));
                }
            }
        }

        public TPersist ConvertToPersist(TDomain domain)
        {
            TPersist persist = (TPersist)Activator.CreateInstance(typeof(TPersist));
            object domainObj = domain;
            object persistObj = persist;

            Type domainType = domainObj.GetType();
            Type persistType = persistObj.GetType();

            List<PropertyInfo> persistFields = persistType.GetProperties().ToList();
            List<PropertyInfo> domainFields = domainType.GetProperties().ToList();
            foreach (PropertyInfo fi in domainFields)
            {
                var persistEqualField = persistFields.Where(x => x.Name == fi.Name).FirstOrDefault();
                if (persistEqualField != null)
                {
                    persistEqualField.SetValue(persistObj, fi.GetValue(domainObj));
                }
            }
            return (TPersist)persistObj;
        }
    }
}
