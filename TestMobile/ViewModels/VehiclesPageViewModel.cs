using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestMobile.Models;
using TestMobile.Models.Components;

namespace TestMobile.ViewModels
{
    public class VehiclesPageViewModel
    {
        private List<Vehicle> _vehicles;
        private List<CustomColumnDefinition> _auctionColumnDefinitions;
        private Vehicle _selectedVehicle;

        public List<Vehicle> Vehicles
        {
            get => _vehicles;
            set
            {
                _vehicles = value;
            }
        }

        public Vehicle SelectedVehicle
        {
            get => _selectedVehicle;
            set
            {
                if (_selectedVehicle != value)
                {
                    _selectedVehicle = value;
                    OnPropertyChanged();
                    if (_selectedVehicle != null)
                    {
                        NavigateToDetails(_selectedVehicle);
                    }
                }
            }
        }

        public VehiclesPageViewModel()
        {
            InitializeColumnDefinitions();
            LoadAuctions();

        }

        private void LoadAuctions()
        {
            var auctionList = App.AuctionList;
            if (auctionList != null)
            {
                Vehicles = new List<Vehicle>();
                foreach (var auction in auctionList)
                {
                    foreach (var vehicle in auction.Vehicles)
                    {
                        Vehicles.Add(vehicle);
                    }
                }
            }
        }

        public List<CustomColumnDefinition> AuctionColumnDefinitions
        {
            get => _auctionColumnDefinitions;
            set
            {
                if (_auctionColumnDefinitions != value)
                {
                    _auctionColumnDefinitions = value;
                    OnPropertyChanged();
                }
            }
        }

        private void InitializeColumnDefinitions()
        {
            AuctionColumnDefinitions = new List<CustomColumnDefinition>
            {
                new CustomColumnDefinition { Header = "Make", BindingProperty = "Make", IsBold = true, Width = 100 },
                new CustomColumnDefinition { Header = "Model", BindingProperty = "Model", IsBold = true, Width = 150 },
                new CustomColumnDefinition { Header = "Auction", BindingProperty = "AuctionDateAndTime", IsBold = true, Width = 100 },
            };
        }

        private async void NavigateToDetails(Vehicle vehicle)
        {
            if (vehicle == null)
                return;
            SelectedVehicle = null;
            await Application.Current.MainPage.Navigation.PushAsync(new Views.Vehicles.VehicleDetails(vehicle));
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
