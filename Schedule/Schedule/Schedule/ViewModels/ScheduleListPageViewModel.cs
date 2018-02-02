using System.Linq;
using System.Xml.Linq;
using HtmlAgilityPack;
using Plugin.Connectivity;
using Prism.Navigation;
using Prism.Services;
using Schedule.Interface;
using Schedule.Services;
using Xamarin.Forms;
using DependencyService = Xamarin.Forms.DependencyService;

namespace Schedule.ViewModels {
    public class ScheduleListPageViewModel : ViewModelBase {
        private readonly IPageDialogService _pageDialogService;

        private readonly IScheduleService _scheduleService;
        private bool _isConnected;
        private HtmlWebViewSource _resource;

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
                const string url = "https://ctn-cantho.com.vn/index.php/vi/hoat-dong/rss/Lich-lam-viec/";
                var rss = await DependencyService.Get<ICertificateTrust>().GetStreamFromUrl(url);
                var docXml = XDocument.Load(rss);
                var firstOrDefault = (from c in docXml.Descendants("channel").Elements("item") select c).FirstOrDefault();
                var linkElement = firstOrDefault?.Element("link");
                if (linkElement != null) {
                    var link = linkElement.Value;
                    var stream = await _scheduleService.GetStreamFromUrl(link);
                    var docHtml = new HtmlDocument();
                    docHtml.Load(stream);
                    var homeText = (from c in docHtml.DocumentNode.Descendants("h1")
                        where c.Attributes.Contains("class") && c.Attributes["class"].Value.Contains("entry-title")
                        select c).FirstOrDefault();
                    var bodyText = (from c in docHtml.DocumentNode.Descendants("div")
                                    where c.Attributes.Contains("class") && c.Attributes["class"].Value.Contains("bodytext")
                                    select c).FirstOrDefault();
                    if (homeText != null && bodyText != null) {
                        var html = "<html><head>" +
                                   "<meta name='viewport' content='width=device-width; height=device-height; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;'/>" +
                                   "<style>.bodytext span { line-height: 1.5; }</style>" +
                                   "</head><body height='100%' width='100%'>";
                        html += "<div style='font-weight: bold; text-align: center; text-transform: uppercase; font-size: 14px; padding: 5px 0 15px;'>" + homeText.InnerHtml + "</div>";
                        html += "<div class='bodytext'>" + bodyText.InnerHtml + "</div>";
                        html += "</body></html>";
                        var htmlSource = new HtmlWebViewSource {Html = html};
                        Resource = htmlSource;
                    }
                }
            }
            else {
                await _pageDialogService.DisplayAlertAsync("Thông báo", "Kiểm tra kết nối mạng", "Hủy");
            }
        }
    }
}