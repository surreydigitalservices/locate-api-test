using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SurreyDigitalServices.Locate.Test.Interfaces;

namespace SurreyDigitalServices.Locate.Test.SinglePoint
{
    public static class Lookup
    {
        public static async Task<IEnumerable<IAddress>> ByPostCode(string PostCode)
        {
            var SPClient = new SearchServiceSoapClient("SinglePointSearch", Settings.SinglePointUri);

            var Search = await SPClient.AdvancedSearchAsync("LLPG", "POSTCODE=" + PostCode + "|LOGICAL_STATUS=1");

            return Search.Results.Items.Select(r => r.ToAddress());
        }
    }
}
