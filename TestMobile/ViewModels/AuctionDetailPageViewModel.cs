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
        private Auction _auction;
        private ObservableCollection<Vehicle> _filteredVehicles;

        public Auction SelectedAuction
        {
            get => _auction;
            set
            {
                _auction = value;
                OnPropertyChanged();
            }
        }

        public string FilterMake { get; set; }
        public string FilterModel { get; set; }
        public double FilterStartingBid { get; set; }
        public bool ShowFavouritesOnly { get; set; }

        public List<Vehicle> Vehicles => SelectedAuction?.Vehicles ?? new List<Vehicle>();
        public ICommand VehicleTappedCommand { get; }
        public ICommand ApplyFilterCommand { get; }

        public ObservableCollection<Vehicle> FilteredVehicles
        {
            get => _filteredVehicles;
            set
            {
                _filteredVehicles = value;
                OnPropertyChanged();
            }
        }


        public AuctionDetailPageViewModel(Auction auction)
        {
            SelectedAuction = auction;
            VehicleTappedCommand = new Command<Vehicle>(OnVehicleTapped);
            ApplyFilterCommand = new Command(ApplyFilters);
            FilteredVehicles = new ObservableCollection<Vehicle>(Vehicles);
        }

        private async void OnVehicleTapped(Vehicle vehicle)
        {
            if (vehicle == null)
                return;

            if (Application.Current?.MainPage?.Navigation != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new VehicleDetails(vehicle, _auction));
            }
        }

        private void ApplyFilters()
        {
            var filtered = Vehicles.Where(v =>
                (string.IsNullOrEmpty(FilterMake) || v.Make.Contains(FilterMake, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(FilterModel) || v.Model.Contains(FilterModel, StringComparison.OrdinalIgnoreCase)) &&
                (FilterStartingBid == 0 || v.StartingBid >= (decimal)FilterStartingBid) &&
                (v.Favourite == ShowFavouritesOnly)
            ).ToList();

            FilteredVehicles = new ObservableCollection<Vehicle>(filtered);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
