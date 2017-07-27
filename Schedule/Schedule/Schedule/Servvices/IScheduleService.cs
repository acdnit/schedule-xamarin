using System.IO;
using System.Threading.Tasks;

namespace Schedule.Servvices {
    public interface IScheduleService {
        Task<string> GetContentFromUrl(string url);
        Task<Stream> GetStreamFromUrl(string url);
    }
}