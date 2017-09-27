using System.IO;
using System.Threading.Tasks;

namespace Schedule.Services {
    public interface IScheduleService {
        Task<string> GetContentFromUrl(string url);
        Task<Stream> GetStreamFromUrl(string url);
    }
}