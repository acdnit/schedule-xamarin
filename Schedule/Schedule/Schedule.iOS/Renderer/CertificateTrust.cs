using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Schedule.iOS.Renderer;
using Schedule.Interface;
using Xamarin.Forms;
using System.Net.Http;

[assembly: Dependency(typeof(CertificateTrust))]

namespace Schedule.iOS.Renderer {
    internal class CertificateTrust : ICertificateTrust {
        public async Task<string> GetContentFromUrl(string url) {
            try {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                var uri = new Uri(url);
                var client = new HttpClient(new HttpClientHandler());
                client.DefaultRequestHeaders.Add("User-Agent", "Xamarin App");
                var resspone = await client.GetAsync(uri);
                var content = resspone.Content;
                return await content.ReadAsStringAsync();
            } catch (Exception) {
                return string.Empty;
            }
        }

        public async Task<Stream> GetStreamFromUrl(string url) {
            try {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                var uri = new Uri(url);
                var client = new HttpClient(new HttpClientHandler());
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