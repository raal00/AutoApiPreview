using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Converters.Abstraction
{
    public interface IDomainToPersistConverter<TPersist, TDomain>
    {
        public void ConvertToPersist(TDomain domain, ref TPersist persist);
        public TPersist ConvertToPersist(TDomain domain);
    }
}
