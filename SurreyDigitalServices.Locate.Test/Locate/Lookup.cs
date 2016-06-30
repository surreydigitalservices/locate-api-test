using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SurreyDigitalServices.Locate.Test.Interfaces;
using SurreyDigitalServices.Locate.Test.Models;

namespace SurreyDigitalServices.Locate.Test.Locate
{
    public static class Lookup
    {
        public static async Task<IEnumerable<IAddress>> ByPostCode(string PostCode)
        {
            if (string.IsNullOrWhiteSpace(PostCode))
            {
                return null;
            }

            PostCode = Regex.Replace(PostCode, @"[^A-Z0-9]+", "", RegexOptions.IgnoreCase);

            using (var hc = new HttpClient())
            {
                hc.BaseAddress = new Uri(Settings.LocateApiHost);
                hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.LocateApiToken);

                var response = await hc.GetAsync("addresses?queryType=residential&postcode=" + PostCode);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<IEnumerable<Address>>();
            }
        }
    }
}
