using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestMobile.Models;
using TestMobile.Models.Components;

namespace TestMobile.ViewModels
{
    public class AuctionPageViewModel: INotifyPropertyChanged
    {
        private List<Auction> _auctions;
        private List<CustomColumnDefinition> _auctionColumnDefinitions;
        private Auction _selectedAuction;

        public List<Auction> Auctions
        {
            get => _auctions;
            set
            {
                _auctions = value;
                OnPropertyChanged();
            }
        }

        public Auction SelectedAuction
        {
            get => _selectedAuction;
            set
            {
                if (_selectedAuction != value)
                {
                    _selectedAuction = value;
                    OnPropertyChanged();
                    if (_selectedAuction != null)
                    {
                        NavigateToDetails(_selectedAuction);
                    }
                }
            }
        }

        public ICommand NavigateToDetailsCommand { get; }
        public AuctionPageViewModel()
        {
            InitializeColumnDefinitions();
            LoadAuctions();
            NavigateToDetailsCommand = new Command<Auction>(NavigateToDetails);
        }

        private void LoadAuctions()
        {
            Auctions = App.AuctionList;
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
                new CustomColumnDefinition { Header = "Id", BindingProperty = "Id", IsBold = true, Width = 100 },
                new CustomColumnDefinition { Header = "DateTime", BindingProperty = "DateTime", IsBold = true, Width = 150 },
            };
        }

        private async void NavigateToDetails(Auction auction)
        {
            if (auction == null)
                return;
            SelectedAuction = null;
            await Application.Current.MainPage.Navigation.PushAsync(new Views.Auctions.AuctionDetails(auction));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
