using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Schedule.Interface;
using Xamarin.Android.Net;
using Xamarin.Forms;

[assembly: Dependency(typeof(HttpClientHandler))]
namespace Schedule.Droid.Renderer {
    internal class HttpClientHandler : IHttpClientHandler {
        public async Task<string> GetContentFromUrl(string url) {
            try {
                var uri = new Uri(url);
                var client = new HttpClient(new AndroidClientHandler());
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
                var client = new HttpClient(new AndroidClientHandler());
                client.DefaultRequestHeaders.Add("User-Agent", "Xamarin App");
                var resspone = await client.GetAsync(uri);
                var content = resspone.Content;
                return await content.ReadAsStreamAsync();
            }
            catch (Exception) {
                return null;
            }
        }
    }
}