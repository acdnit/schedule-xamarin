using System;
using Schedule.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Schedule.Servvices {
    public class ScheduleService : IScheduleService {
        public Task<List<ScheduleInfo>> GetListSchedule() {
            var listSchedule = new List<ScheduleInfo> {
                new ScheduleInfo { Name = "Name 01", Detail = "Detail 01" },
                new ScheduleInfo { Name = "Name 02", Detail = "Detail 02" },
                new ScheduleInfo { Name = "Name 03", Detail = "Detail 03" },
                new ScheduleInfo { Name = "Name 04", Detail = "Detail 04" },
                new ScheduleInfo { Name = "Name 05", Detail = "Detail 05" },
                new ScheduleInfo { Name = "Name 06", Detail = "Detail 06" },
                new ScheduleInfo { Name = "Name 07", Detail = "Detail 07" },
                new ScheduleInfo { Name = "Name 08", Detail = "Detail 08" },
                new ScheduleInfo { Name = "Name 09", Detail = "Detail 09" },
                new ScheduleInfo { Name = "Name 10", Detail = "Detail 10" },
                new ScheduleInfo { Name = "Name 11", Detail = "Detail 11" },
                new ScheduleInfo { Name = "Name 12", Detail = "Detail 12" },
                new ScheduleInfo { Name = "Name 13", Detail = "Detail 13" },
                new ScheduleInfo { Name = "Name 14", Detail = "Detail 14" },
                new ScheduleInfo { Name = "Name 15", Detail = "Detail 15" }
            };
            return Task.FromResult(listSchedule);
        }

        public async Task<string> GetContentFromUrl(string url) {
            var uri = new Uri(url);
            var client = new HttpClient();
            var resspone = await client.GetAsync(uri);
            var content = resspone.Content;
            return await content.ReadAsStringAsync();
        }
    }
}
