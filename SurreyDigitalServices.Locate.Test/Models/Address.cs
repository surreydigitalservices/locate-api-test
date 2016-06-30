using SurreyDigitalServices.Locate.Test.Interfaces;

namespace SurreyDigitalServices.Locate.Test.Models
{
    public class Address : IAddress
    {
        public Address()
        {
            Property = "";
            Street = "";
            Locality = "";
            Town = "";
            Area = "";
            PostCode = "";
        }

        public string Property { get; set; }
        public string Street { get; set; }
        public string Locality { get; set; }
        public string Town { get; set; }
        public string Area { get; set; }
        public string PostCode { get; set; }
        public long Uprn { get; set; }
        public string GssCode { get; set; }
    }

    public class AddressResult
    {
        public IAddress Locate { get; set; }
        public IAddress SinglePoint { get; set; }
    }
}
