using DAL.Converters.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DAL.Converters
{
    public class PersistToDomainConverter<TPersist, TDomain> : IPersistToDomainConverter<TPersist, TDomain>
        where TPersist : class
        where TDomain : class
    {
        public TDomain ConvertToDomain(TPersist persist)
        {
            TDomain domain = (TDomain)Activator.CreateInstance(typeof(TDomain));
            object domainObj = domain;
            object persistObj = persist;

            Type domainType = domainObj.GetType();
            Type persistType = persistObj.GetType();

            List<PropertyInfo> persistFields = persistType.GetProperties().ToList();
            List<PropertyInfo> domainFields = domainType.GetProperties().ToList();
            foreach (PropertyInfo fi in persistFields)
            {
                var persistEqualField = domainFields.Where(x => x.Name == fi.Name).FirstOrDefault();
                if (persistEqualField != null)
                {
                    persistEqualField.SetValue(domainObj, fi.GetValue(persistObj));
                }
            }
            return (TDomain)domainObj;
        }
    }
}
