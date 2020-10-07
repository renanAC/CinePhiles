using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TestCinephiles.Exceptions;
using TestCinephiles.Helper;
using TestCinephiles.Model.Genres;
using TestCinephiles.Model.UpcomingMovies;
using TestCinephiles.Services.TMDbService;


namespace TestCinephiles.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly ITMDbService _tMDbService;
        private IPageDialogService _pageDialogService;

        public ObservableCollection<UpcomingMovie> UpcomingMovie { get; set; }
        public List<Genrer> Genres { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public string Search { get; set; }

        public DelegateCommand<object> ItemAppearingCommand { get; private set; }
        public DelegateCommand<object> ItemSelectedCommand { get; private set; }
        public DelegateCommand TextChangedCommand { get; private set; }
        public DelegateCommand LoadDataCommand { get; set; }

        public MainPageViewModel(INavigationService navigationService,
                                 ITMDbService tMDbService,
                                 IPageDialogService pageDialogService)
            : base(navigationService)
        {
            Title = "Upcoming Movies";
            UpcomingMovie = new ObservableCollection<UpcomingMovie>();
            _tMDbService = tMDbService;
            _pageDialogService = pageDialogService;
            ItemAppearingCommand = new DelegateCommand<object>(ItemAppearing);
            ItemSelectedCommand = new DelegateCommand<object>(ItemSelected);
            TextChangedCommand = new DelegateCommand(TextChanged);
            LoadDataCommand = new DelegateCommand(LoadData);

        }

        private async void LoadData()
        {
            await AddNextPageData();
        }

        public async void ItemAppearing(object parameter)
        {
            var upcomingMovie = parameter as UpcomingMovie;
            int upcomingMovieIndex = UpcomingMovie.IndexOf(upcomingMovie);

            if (UpcomingMovie.Count - 2 <= upcomingMovieIndex)
            {
                await AddNextPageData();
            }
        }

        private async void TextChanged()
        {
            Page = 0;
            await AddNextPageData();
        }

        public async void ItemSelected(object parameter)
        {
            var upcoming = parameter as UpcomingMovie;

            NavigationParameters navParams = new NavigationParameters
            {
                { "Movie", upcoming }
            };

            await NavigationService.NavigateAsync("Details", navParams);
        }

        public async Task AddNextPageData()
        {
            try
            {


                if (Page == 0) UpcomingMovie.Clear();

                if (Page <= TotalPages)
                {
                    Page++;
                    UpcomingMovieRequest upcomingMovie = new UpcomingMovieRequest()
                    {
                        page = Page,
                        api_key = Settings.TMDbApiKey
                    };
                    UpcomingMovieResult upcomingMovies;

                    if (!String.IsNullOrWhiteSpace(Search))
                    {
                        upcomingMovie.query = Search;
                        upcomingMovies = await _tMDbService.GetUpcomingMoviesSearchAsync(upcomingMovie);
                    }
                    else
                    {
                        upcomingMovies = await _tMDbService.GetUpcomingMovies(upcomingMovie);
                    }


                    TotalPages = upcomingMovies.TotalPages;
                    Page = upcomingMovies.Page;
                    foreach (var item in upcomingMovies.Results)
                    {
                        item.Genres = await GetGenrer(item.GenreIds);
                        UpcomingMovie.Add(item);
                    }
                }
            }
            catch (ServiceAuthenticationException ex)
            {
               await _pageDialogService.DisplayAlertAsync("Error", "Some error occurs when try access the Server, please try again!", "OK");
            }
            catch (HttpRequestExceptionEx ex)
            {
                await _pageDialogService.DisplayAlertAsync("Error", "Service is not respond, please try again later!", "OK");

            }
            catch (Exception ex)
            {
                await _pageDialogService.DisplayAlertAsync("Error", "Unexpected error, please try again!", "OK");
            }
        }

        public async Task<string> GetGenrer(List<int> Ids)
        {
            if (Genres == null)
            {
                var genrerResult = await _tMDbService.GetGenrer();
                Genres = genrerResult.Genres;
            }

            return string.Join(", ", Genres.Where(f => Ids.Contains(f.Id)).Select(f => f.Name).ToList());
        }

    }
}
