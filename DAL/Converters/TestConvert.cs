using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NUnit.Framework;

namespace DAL.Converters
{
    [TestFixture]
    public class TestConvert
    {
        [TestCase]
        public void FullEqualsTest()
        {
            var toPersistConverter = new Converters.DomainToPersistConverter<APersist, ADomain>();
            var domain = new ADomain()
            {
                A = 100,
                B = "test"
            };
            var persist = new APersist();
            toPersistConverter.ConvertToPersist(domain, ref persist);

            Assert.AreEqual(100, persist.A);
            Assert.AreEqual("test", persist.B);
        }

        [TestCase]
        public void DateWithNullFieldTest()
        {
            var toPersistConverter = new Converters.DomainToPersistConverter<BPersist, BDomain>();

            DateTime now = DateTime.Now;
            var domain = new BDomain()
            {
                A = 100,
                B = "test",
                C = now
            };
            var persist = new BPersist();
            toPersistConverter.ConvertToPersist(domain, ref persist);

            Assert.AreEqual(100, persist.A);
            Assert.AreEqual("test", persist.B);
            Assert.AreEqual(now, persist.C);
            Assert.AreEqual(null, persist.D);
        }

        [TestCase]
        public void OverrideConvertTest()
        {
            var toPersistConverter = new Converters.DomainToPersistConverter<APersist, ADomain>();
            var domain = new ADomain()
            {
                A = 100,
                B = "test"
            };
            var persist = toPersistConverter.ConvertToPersist(domain);
            Assert.AreEqual(100, persist.A);
            Assert.AreEqual("test", persist.B);
        }

    }

    /// <summary>
    /// TEST A MODELS
    /// </summary>
    public class APersist
    {
        public int A { get; set; }
        public string B { get; set; }
    }
    public class ADomain
    {
        public int A { get; set; }
        public string B { get; set; }
    }

    /// <summary>
    /// TEST B MODELS
    /// </summary>
    public class BPersist
    {
        public int A { get; set; }
        public string B { get; set; }
        public DateTime C { get; set; }
        public string D { get; set; }
    }
    public class BDomain
    {
        public int A { get; set; }
        public DateTime C { get; set; }
        public string B { get; set; }
    }
}
