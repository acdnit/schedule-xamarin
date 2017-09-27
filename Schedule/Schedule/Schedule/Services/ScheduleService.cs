using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Schedule.Services {
    public class ScheduleService : IScheduleService {
        public async Task<string> GetContentFromUrl(string url) {
            try {
                var uri = new Uri(url);
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "Xamarin App");
                var resspone = await client.GetAsync(uri);
                var content = resspone.Content;
                return await content.ReadAsStringAsync();
            }
            catch (Exception) {
                return string.Empty;
            }
        }

        public async Task<Stream> GetStreamFromUrl(string url) {
            try {
                var uri = new Uri(url);
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "Xamarin App");
                var resspone = await client.GetAsync(uri);
                var content = resspone.Content;
                return await content.ReadAsStreamAsync();
            } catch (Exception) {
                return null;
            }
        }
    }
}