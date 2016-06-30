using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SurreyDigitalServices.Locate.Test.Interfaces;
using SurreyDigitalServices.Locate.Test.Models;

namespace SurreyDigitalServices.Locate.Test
{
    [TestClass]
    public class GU4_7NW
    {
        private IEnumerable<IAddress> LocateAddresses = null;
        private IEnumerable<IAddress> SinglePointAddresses = null;
        private IEnumerable<AddressResult> CombinedAddresses = null;

        public GU4_7NW()
        {
            LoadAddresses();
        }

        private void LoadAddresses(string PostCode = "GU4 7NW")
        {
            SinglePointAddresses = SinglePoint.Lookup.ByPostCode(PostCode).Result;

            LocateAddresses = Locate.Lookup.ByPostCode(PostCode).Result;

            CombinedAddresses = LocateAddresses.Join(SinglePointAddresses, a => a.Uprn, b => b.Uprn, (a, b) => new AddressResult { Locate = a, SinglePoint = b });
        }

        [TestMethod]
        public void CanLoadAddressesFromSinglePoint()
        {
            Assert.IsTrue(SinglePointAddresses.Count() > 0);
        }

        [TestMethod]
        public void CanLoadAddressesFromLocate()
        {
            Assert.IsTrue(LocateAddresses.Count() > 0);
        }

        [TestMethod]
        public void CheckForStreetRecords()
        {
            Assert.IsTrue(LocateAddresses.Where(l => l.Property == "Street record").Count() == 0);
        }

        [TestMethod]
        public void MatchArea()
        {
            var NotEqual = CombinedAddresses.Where(a => 
                !a.Locate.Area.Equals(a.SinglePoint.Area, System.StringComparison.CurrentCultureIgnoreCase)
            );

            var Count = NotEqual.Count();

            Assert.IsTrue(Count == 0, Count + " records didn't match.");
        }

        [TestMethod]
        public void MatchLocality()
        {
            var NotEqual = CombinedAddresses.Where(a =>
                !a.Locate.Locality.Equals(a.SinglePoint.Locality, System.StringComparison.CurrentCultureIgnoreCase)
            );

            var Count = NotEqual.Count();

            Assert.IsTrue(Count == 0, Count + " records didn't match.");
        }

        [TestMethod]
        public void MatchPostCode()
        {
            var NotEqual = CombinedAddresses.Where(a =>
                !a.Locate.PostCode.Equals(a.SinglePoint.PostCode, System.StringComparison.CurrentCultureIgnoreCase)
            );

            var Count = NotEqual.Count();

            Assert.IsTrue(Count == 0, Count + " records didn't match.");
        }

        [TestMethod]
        public void MatchProperty()
        {
            var NotEqual = CombinedAddresses.Where(a =>
                !a.Locate.Property.Equals(a.SinglePoint.Property, System.StringComparison.CurrentCultureIgnoreCase)
            );

            var Count = NotEqual.Count();

            Assert.IsTrue(Count == 0, Count + " records didn't match.");
        }

        [TestMethod]
        public void MatchStreets()
        {
            var NotEqual = CombinedAddresses.Where(a =>
                !a.Locate.Street.Equals(a.SinglePoint.Street, System.StringComparison.CurrentCultureIgnoreCase)
            );

            var Count = NotEqual.Count();

            Assert.IsTrue(Count == 0, Count + " records didn't match.");
        }

        [TestMethod]
        public void MatchTown()
        {
            var NotEqual = CombinedAddresses.Where(a =>
                !a.Locate.Town.Equals(a.SinglePoint.Town, System.StringComparison.CurrentCultureIgnoreCase)
            );

            var Count = NotEqual.Count();

            Assert.IsTrue(Count == 0, Count + " records didn't match.");
        }

        [TestMethod]
        public void UprnNotInLocate()
        {
            var NotInLocate = SinglePointAddresses.Where(s => !LocateAddresses.Select(l => l.Uprn).Contains(s.Uprn));

            var Count = NotInLocate.Count();

            Assert.IsTrue(Count == 0, Count + " UPRNs weren't found in Locate data.");
        }

        [TestMethod]
        public void UprnNotInSinglePoint()
        {
            var NotInSinglePoint = LocateAddresses.Where(l => !SinglePointAddresses.Select(s => s.Uprn).Contains(l.Uprn));

            var Count = NotInSinglePoint.Count();

            Assert.IsTrue(Count == 0, Count + " UPRNs weren't found in SinglePoint data.");
        }
    }
}
