using System;
using System.Linq;
using SurreyDigitalServices.Locate.Test.Interfaces;

namespace SurreyDigitalServices.Locate.Test.SinglePoint
{
    public static class SearchResultItemExtension
    {
        private static string TryGetValue(this FieldInfo[] fi, string Tag)
        {
            var val = fi.Where(f => f.Tag == Tag).FirstOrDefault();

            if (val != null)
            {
                return val.Value;
            }

            return string.Empty;
        }

        public static IAddress ToAddress(this SearchResultItem result)
        {
            if (result != null)
            {
                var Address = new Models.Address();

                string FormattedAddress;

                FormattedAddress = result.FieldItems.TryGetValue("FULL_ADDRESS");

                var Index = Math.Max(0, FormattedAddress.IndexOf(result.FieldItems.TryGetValue("STREET") + ","));

                Address.Uprn = long.Parse(result.FieldItems.TryGetValue("UPRN") ?? null);
                //Address.Usrn = long.Parse(result.FieldItems.TryGetValue("USRN") ?? null);
                //Address.Organisation = result.FieldItems.TryGetValue("ORGANISATION");
                Address.Property = (!String.IsNullOrWhiteSpace(FormattedAddress) ? FormattedAddress.Substring(0, Index) : String.Empty).Trim().TrimEnd(',');
                Address.Street = result.FieldItems.TryGetValue("STREET");
                Address.Locality = result.FieldItems.TryGetValue("LOCALITY");
                Address.Town = result.FieldItems.TryGetValue("TOWN");
                Address.Area = result.FieldItems.TryGetValue("POSTTOWN");
                Address.PostCode = result.FieldItems.TryGetValue("POSTCODE");

                return Address;
            }
            return null;
        }
    }
}
