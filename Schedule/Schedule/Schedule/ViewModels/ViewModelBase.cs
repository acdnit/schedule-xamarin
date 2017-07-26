using Prism.Mvvm;
using Prism.Navigation;

namespace Schedule.ViewModels {
    public class ViewModelBase : BindableBase, INavigationAware {
        private string _title;

        protected INavigationService NavigationService { get; }
        protected ViewModelBase(INavigationService navigationService) {
            NavigationService = navigationService;
        }

        public string Title {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters) {
            
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters) {
            
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters) {
            
        }
    }
}
