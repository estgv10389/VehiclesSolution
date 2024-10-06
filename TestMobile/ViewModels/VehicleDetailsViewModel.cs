using System;
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
        private Auction _auction;
        private bool _favourite;

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

        public Auction Auction
        {
            get => _auction;
            set
            {
                _auction = value;
                OnPropertyChanged(nameof(Auction));
            }
        }

        public string AuctionDate => _auction.DateTime.ToString("dd/MM/yyyy");
        public string AuctionTime => _auction.DateTime.ToString("hh:mm tt");


        public string TimeUntilAuction
        {
            get
            {
                var timeRemaining = _auction.DateTime - DateTime.Now;

                if (timeRemaining.TotalSeconds <= 0)
                    return "Auction has already ended";

                return $"{timeRemaining.Days} days, {timeRemaining.Hours} hours remaining";
            }
        }

        public VehicleDetailsViewModel(Vehicle vehicle, Auction auction)
        {
            ImageSource = ImageSource.FromFile("dotnet_bot.png");
            Vehicle = vehicle;
            Auction = auction;
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
