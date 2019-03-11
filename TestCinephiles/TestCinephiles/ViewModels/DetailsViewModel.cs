using Prism.Navigation;
using TestCinephiles.Model.UpcomingMovies;

namespace TestCinephiles.ViewModels
{
    public class DetailsViewModel : ViewModelBase
    {
        public DetailsViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Movie = new UpcomingMovie();
            Title = "Movie";
        }

        public UpcomingMovie Movie { get; set; }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("Movie"))
            {
                Movie = (UpcomingMovie)parameters["Movie"];
            }

            RaisePropertyChanged("Movie");
        }
    }
}
