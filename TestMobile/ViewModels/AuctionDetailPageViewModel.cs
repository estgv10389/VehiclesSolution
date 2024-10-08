using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMobile.Models;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using TestMobile.Models.Components;
using System.Windows.Input;
using TestMobile.Views.Vehicles;
using System.Collections.ObjectModel;

namespace TestMobile.ViewModels
{
   public class AuctionDetailPageViewModel : INotifyPropertyChanged
    {

        private int _selectedVehiclesPerPage;
        private int _currentPage = 1;
        private bool _isLoadingMoreVehicles = false;
        public decimal _filterStartingBid;
        public string _filterMake;
        public string _filterModel;
        private string _selectedSortOption;
        private bool _isDescending;
        private bool _isBusy;
        private bool _canGoToNextPage;
        private bool _canGoToPreviousPage;

        private Auction _auction;
        public ObservableCollection<Vehicle> FilteredVehicles { get; set; } = new ObservableCollection<Vehicle>();
        public ObservableCollection<string> Makes { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> Models { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<int> VehiclesPerPageOptions { get; set; } = new ObservableCollection<int> { 5, 10, 20, 50 };
        public List<string> SortOptions { get; set; } = new List<string>
        {
            "Make", "Starting Bid", "Mileage", "Auction Date"
        };
        public bool ShowFavouritesOnly { get; set; }

        public decimal MaxBid
        {
            get
            {
                
                return SelectedAuction.Vehicles.Max(v => v.StartingBid);
            }
           
        }

        public List<Vehicle> Vehicles => SelectedAuction?.Vehicles ?? new List<Vehicle>();
        public ICommand VehicleTappedCommand { get; }
        public ICommand ApplyFilterCommand { get; }
        public ICommand LoadMoreVehiclesCommand { get; }
        public ICommand ApplySortCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }

        public Auction SelectedAuction
        {
            get => _auction;
            set
            {
                _auction = value;
                OnPropertyChanged();
                PopulateMakesAndModels();
            }
        }

        private void PopulateMakesAndModels()
        {
            Makes.Clear();
            Models.Clear();

            if (SelectedAuction?.Vehicles != null)
            {
                var uniqueMakes = SelectedAuction.Vehicles.Select(v => v.Make).Distinct().ToList();
                foreach (var make in uniqueMakes)
                {
                    Makes.Add(make);
                }

                var uniqueModels = SelectedAuction.Vehicles.Select(v => v.Model).Distinct().ToList();
                foreach (var model in uniqueModels)
                {
                    Models.Add(model);
                }
            }
        }

        public string FilterMake
        {
            get => _filterMake;
            set
            {
                _filterMake = value;
                OnPropertyChanged();
            }
        }

        public string FilterModel
        {
            get => _filterModel;
            set
            {
                _filterModel = value;
                OnPropertyChanged();
            }
        }

        public decimal FilterStartingBid
        {
            get => _filterStartingBid;
            set
            {
                if (_filterStartingBid != value)
                {
                    _filterStartingBid = value;
                    OnPropertyChanged(nameof(FilterStartingBid));
                }
            }
        }

        public bool IsDescending
        {
            get => _isDescending;
            set
            {
                _isDescending = value;
                OnPropertyChanged();
            }
        }

        public string SelectedSortOption
        {
            get => _selectedSortOption;
            set
            {
                _selectedSortOption = value;
                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public int SelectedVehiclesPerPage
        {
            get => _selectedVehiclesPerPage;
            set
            {
                _selectedVehiclesPerPage = value;
                OnPropertyChanged();
                _currentPage = 1;
                LoadInitialVehicles();
            }
        }

        public int TotalPages => (int)Math.Ceiling((double)Vehicles.Count / SelectedVehiclesPerPage);
        public string CurrentPageDisplay => $"Page {_currentPage} of {TotalPages}";

        public bool CanGoToNextPage
        {
            get => _canGoToNextPage;
            set
            {
                _canGoToNextPage = value;
                OnPropertyChanged();
            }
        }

        public bool CanGoToPreviousPage
        {
            get => _canGoToPreviousPage;
            set
            {
                _canGoToPreviousPage = value;
                OnPropertyChanged();
            }
        }

        public AuctionDetailPageViewModel(Auction auction)
        {
      
            SelectedAuction = auction;
            SelectedVehiclesPerPage = 5;
            VehicleTappedCommand = new Command<Vehicle>(OnVehicleTapped);
            ApplyFilterCommand = new Command(ApplyFilters);
            ApplySortCommand = new Command(ApplySort);
            NextPageCommand = new Command(GoToNextPage);
            PreviousPageCommand = new Command(GoToPreviousPage);
            LoadInitialVehicles();
        }

        private void LoadInitialVehicles()
        {
            if (SelectedAuction == null || !SelectedAuction.Vehicles.Any()) return;

            _isLoadingMoreVehicles = true;
            FilteredVehicles.Clear();
            var initialVehicles = Vehicles.Skip((_currentPage - 1) * SelectedVehiclesPerPage).Take(SelectedVehiclesPerPage).ToList(); 
            foreach (var vehicle in initialVehicles)
            {
                FilteredVehicles.Add(vehicle);
            }

            CanGoToNextPage = _currentPage < TotalPages;
            CanGoToPreviousPage = _currentPage > 1;

            OnPropertyChanged(nameof(CurrentPageDisplay));
            _isLoadingMoreVehicles = false;
        }

        private async void OnVehicleTapped(Vehicle vehicle)
        {
            if (vehicle == null)
                return;

            await Application.Current.MainPage.Navigation.PushAsync(new VehicleDetails(vehicle));
        }

        private void ApplyFilters()
        {
            IsBusy = true;
            FilteredVehicles.Clear();
            var filtered = Vehicles.Where(v =>
                (string.IsNullOrEmpty(FilterMake) || v.Make == FilterMake) &&
                (string.IsNullOrEmpty(FilterModel) || v.Model == FilterModel) &&
                (FilterStartingBid == 0 || v.StartingBid >= FilterStartingBid) &&
                (!ShowFavouritesOnly || v.Favourite == ShowFavouritesOnly)
            ).ToList();

            foreach (var vehicle in filtered)
            {
                FilteredVehicles.Add(vehicle);
            }

            IsBusy = false;
        }


        private async void ApplySort()
        {
            IsBusy = true;
            await Task.Run(() =>
            {
                IEnumerable<Vehicle> sortedVehicles = Vehicles;

                switch (SelectedSortOption)
                {
                    case "Make":
                        sortedVehicles = IsDescending
                            ? sortedVehicles.OrderByDescending(v => v.Make)
                            : sortedVehicles.OrderBy(v => v.Make);
                        break;
                    case "Starting Bid":
                        sortedVehicles = IsDescending
                            ? sortedVehicles.OrderByDescending(v => v.StartingBid)
                            : sortedVehicles.OrderBy(v => v.StartingBid);
                        break;
                    case "Mileage":
                        sortedVehicles = IsDescending
                            ? sortedVehicles.OrderByDescending(v => v.Mileage)
                            : sortedVehicles.OrderBy(v => v.Mileage);
                        break;
                    case "Auction Date":
                        sortedVehicles = IsDescending
                            ? sortedVehicles.OrderByDescending(v => v.AuctionDateAndTime)
                            : sortedVehicles.OrderBy(v => v.AuctionDateAndTime);
                        break;
                }

                var sortedList = sortedVehicles.ToList();

                Application.Current!.Dispatcher.Dispatch(() =>
                {
                    FilteredVehicles.Clear();

                    foreach (var vehicle in sortedList)
                    {
                        FilteredVehicles.Add(vehicle);
                    }
                });
            });

            IsBusy = false;
        }

        private void GoToNextPage()
        {
            if (_currentPage < TotalPages)
            {
                _currentPage++;
                LoadInitialVehicles();
            }
        }

        private void GoToPreviousPage()
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                LoadInitialVehicles();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
