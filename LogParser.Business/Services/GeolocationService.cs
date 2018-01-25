using System.Net.Http;
using System.Threading.Tasks;

namespace LogParser.Business.Services
{
    public static class GeolocationService
    {
        private static string geoLocationProvider = "http://freegeoip.net/json/";

        public static string GetGeolocation(string host)
        {
            HttpClient client = new HttpClient();
            return client.GetAsync($"{geoLocationProvider}{host}").Result.Content.ReadAsStringAsync().Result;
        }

        public static Task<string> GetGeolocationAsync(string host)
        {
            HttpClient client = new HttpClient();
            return client.GetAsync($"{geoLocationProvider}{host}").Result.Content.ReadAsStringAsync();
        }
    }
}
