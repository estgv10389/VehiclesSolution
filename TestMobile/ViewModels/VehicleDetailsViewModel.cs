﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestMobile.Models;

namespace TestMobile.ViewModels
{

    public class VehicleDetailsViewModel : INotifyPropertyChanged
    {
        private ImageSource _imageSource;
        private Vehicle _vehicle;
        private string _auctionDate;
        private string _auctionTime;
        private DateTime _vehicleAuctionDateTime;

        public ImageSource ImageSource
        {
            get => _imageSource;
            set
            {
                if (_imageSource != value)
                {
                    _imageSource = value;
                    OnPropertyChanged(nameof(ImageSource));
                }
            }

        }

        public bool Favourite
        {
            get => _vehicle.Favourite;  
            set
            {
                if (_vehicle.Favourite != value)
                {
                    _vehicle.Favourite = value;
                    OnPropertyChanged(nameof(Favourite));
                    OnPropertyChanged(nameof(FavouriteIcon));
                }
            }
        }

        public string FavouriteIcon => Favourite ? "star_filled.png" : "star_outline.png";
        public ICommand ToggleFavouriteCommand { get; }

        public Vehicle Vehicle
        {
            get => _vehicle;
            set
            {

                _vehicle = value;
                OnPropertyChanged(nameof(Vehicle));

            }
        }

 
        public string TimeUntilAuction
        {
            get
            {
                var timeRemaining = _vehicleAuctionDateTime - DateTime.Now;

                if (timeRemaining.TotalSeconds <= 0)
                    return "Auction has already ended";

                return $"{timeRemaining.Days} days, {timeRemaining.Hours} hours remaining";
            }
        }

        public string AuctionDate => _auctionDate;
        public string AuctionTime => _auctionTime;

        public VehicleDetailsViewModel(Vehicle vehicle)
        {
            ImageSource = ImageSource.FromFile("dotnet_bot.png");
            Vehicle = vehicle;
            _auctionDate = vehicle.AuctionDateAndTime.ToString("dd/MM/yyyy");
            _auctionTime = vehicle.AuctionDateAndTime.ToString("hh:mm tt");
            _vehicleAuctionDateTime = vehicle.AuctionDateAndTime;
            ToggleFavouriteCommand = new Command(ToggleFavourite);

        }
        private void ToggleFavourite()
        {
            Favourite = !Favourite;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
