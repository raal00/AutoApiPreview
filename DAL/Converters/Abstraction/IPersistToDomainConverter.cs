using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Converters.Abstraction
{
    public interface IPersistToDomainConverter<TPersist, TDomain>
    {
        public TDomain ConvertToDomain(TPersist persist);
    }
}
