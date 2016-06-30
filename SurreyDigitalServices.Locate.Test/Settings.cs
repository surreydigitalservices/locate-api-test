using System;

namespace SurreyDigitalServices.Locate.Test
{
    internal static class Settings
    {
        internal static string LocateApiHost
        {
            get
            {
                return Environment.GetEnvironmentVariable("LocateApiHost");
            }
        }

        internal static string LocateApiToken
        {
            get
            {
                return Environment.GetEnvironmentVariable("LocateApiToken");
            }
        }

        internal static string SinglePointUri
        {
            get
            {
                return Environment.GetEnvironmentVariable("SinglePointUri");
            }
        }
    }
}
