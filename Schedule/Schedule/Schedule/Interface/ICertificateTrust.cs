using System.IO;
using System.Threading.Tasks;

namespace Schedule.Interface {
    public interface ICertificateTrust {
        Task<string> GetContentFromUrl(string url);
        Task<Stream> GetStreamFromUrl(string url);
    }
}