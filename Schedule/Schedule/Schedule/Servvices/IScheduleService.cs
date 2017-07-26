using Schedule.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schedule.Servvices {
    public interface IScheduleService {
        Task<List<ScheduleInfo>> GetListSchedule();
        Task<string> GetContentFromUrl(string url);
    }
}