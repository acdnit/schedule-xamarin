using System.Linq;
using System.Xml.Linq;
using HtmlAgilityPack;
using Plugin.Connectivity;
using Prism.Navigation;
using Prism.Services;
using Schedule.Servvices;
using Xamarin.Forms;

namespace Schedule.ViewModels {
    public class ScheduleListPageViewModel : ViewModelBase {
        private HtmlWebViewSource _resource;
        private bool _isConnected;

        private readonly IScheduleService _scheduleService;
        private readonly IPageDialogService _pageDialogService;        

        public ScheduleListPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IScheduleService scheduleService) : base(navigationService) {
            Title = "Lịch tuần";
            _scheduleService = scheduleService;
            _pageDialogService = pageDialogService;
        }

        public HtmlWebViewSource Resource {
            get => _resource;
            set => SetProperty(ref _resource, value);
        }

        public bool IsConnected {
            get => _isConnected;
            set => SetProperty(ref _isConnected, value);
        }

        public override async void OnNavigatedTo(NavigationParameters parameters) {
            base.OnNavigatedTo(parameters);
            IsConnected = CrossConnectivity.Current.IsConnected;
            if (IsConnected) {
                const string url = "http://ctn-cantho.com.vn/index.php/vi/hoat-dong/rss/Lich-lam-viec/";
                var rss = await _scheduleService.GetStreamFromUrl(url);
                var docXml = XDocument.Load(rss);
                var linkElement = (from c in docXml.Descendants("channel").Elements("item") select c).FirstOrDefault().Element("link");
                if (linkElement != null) {
                    var link = linkElement.Value;
                    var stream = await _scheduleService.GetStreamFromUrl(link);
                    var docHtml = new HtmlDocument();
                    docHtml.Load(stream);
                    var table = (from c in docHtml.DocumentNode.Descendants("div")
                        where c.Attributes.Contains("class") && c.Attributes["class"].Value == "bodytext"
                        select c).FirstOrDefault().Descendants("table").LastOrDefault();

                    var html = "<html><head>" +
                               "<meta name='viewport' content='width=device-width; height=device-height; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;'/>" +
                               "</head><body height='100%' width='100%'><table style='width: 100%;'>";
                    html += table.InnerHtml;
                    html += "</table></body></html>";
                    var htmlSource = new HtmlWebViewSource { Html = html };
                    Resource = htmlSource;
                }
            } else {
                await _pageDialogService.DisplayAlertAsync("Thông báo", "Kiểm tra kết nối mạng", "Hủy");
            }
        }
    }
}