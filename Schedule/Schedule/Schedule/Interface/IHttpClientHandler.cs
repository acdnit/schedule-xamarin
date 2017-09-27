using System.IO;
using System.Threading.Tasks;

namespace Schedule.Interface {
    public interface IHttpClientHandler {
        Task<string> GetContentFromUrl(string url);
        Task<Stream> GetStreamFromUrl(string url);
    }
}