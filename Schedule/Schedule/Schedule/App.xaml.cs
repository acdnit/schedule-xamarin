using Microsoft.Practices.Unity;
using Prism.Unity;
using Schedule.Servvices;
using Xamarin.Forms;
using Schedule.Views;

namespace Schedule {
    public partial class App {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized() {
            InitializeComponent();
            NavigationService.NavigateAsync("Navigation/ScheduleList");
        }

        protected override void RegisterTypes() {
            Container.RegisterType<IScheduleService, ScheduleService>(new ContainerControlledLifetimeManager());

            Container.RegisterTypeForNavigation<NavigationPage>("Navigation");
            Container.RegisterTypeForNavigation<ScheduleListPage>("ScheduleList");
        }
    }
}