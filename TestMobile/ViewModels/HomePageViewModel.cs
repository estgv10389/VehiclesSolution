using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestMobile.Models;

namespace TestMobile.ViewModels
{
    public class HomePageViewModel
    {
        private int _totalCars;
        private Auction _nextAuction;
        private int _nextAuctionCount;

        public int TotalCars
        {
            get => _totalCars;
            set
            {
                _totalCars = value;
                OnPropertyChanged();
            }
        }

        public Auction NextAuction
        {
            get => _nextAuction;
            set
            {
                _nextAuction = value;
                OnPropertyChanged();
            }
        }

        public int NextAuctionCount
        {
            get => _nextAuctionCount;
            set
            {
                _nextAuctionCount = value;
                OnPropertyChanged();
            }
        }

        public HomePageViewModel()
        {
            LoadData();
        }

        private void LoadData()
        {
            var auctions = App.AuctionList ?? new List<Auction>();
            TotalCars = auctions.Sum(a => a.Vehicles.Count);
            NextAuction = auctions
                .Where(a => a.DateTime >= DateTime.Now)
                .OrderBy(a => a.DateTime)
                .FirstOrDefault();
                
            NextAuctionCount = auctions
                .Where(a => a.DateTime >= DateTime.Now)
                .OrderBy(a => a.DateTime).SelectMany(a => a.Vehicles).Count();

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
