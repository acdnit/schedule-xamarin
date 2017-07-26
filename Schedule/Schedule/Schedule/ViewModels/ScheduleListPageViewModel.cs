using Prism.Navigation;
using Schedule.Servvices;

namespace Schedule.ViewModels {
    public class ScheduleListPageViewModel : ViewModelBase {
        private string _url;
        private readonly IScheduleService _scheduleService;

        public ScheduleListPageViewModel(INavigationService navigationService, IScheduleService scheduleService) : base(navigationService) {            
            Title = "Lịch tuần";
            _scheduleService = scheduleService;
        }

        public string Url {
            get => _url;
            set => SetProperty(ref _url, value);
        }

        public override async void OnNavigatedTo(NavigationParameters parameters) {
            base.OnNavigatedTo(parameters);
            Url = "http://ctn-cantho.com.vn/index.php/vi/hoat-dong/Lich-lam-viec/Lich-tuan-chinh-thuc-24-7-28-7-2017-369/";
            var html = await _scheduleService.GetContentFromUrl(Url);
            var document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(html);
        }
    }
}