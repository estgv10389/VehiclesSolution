﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public Auction? SelectedAuction
        {
            get => _selectedAuction;
            set
            {
                if (_selectedAuction != value && value != null)
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
            LoadAuctions();
            NavigateToDetailsCommand = new Command<Auction>(NavigateToDetails);
        }

        private void LoadAuctions()
        {
            Auctions = App.AuctionList ?? new List<Auction>();
        }

 
        private async void NavigateToDetails(Auction auction)
        {
            if (auction == null)
                return;
            SelectedAuction = null;
            await Application.Current.MainPage.Navigation.PushAsync(new Views.Auctions.AuctionDetails(auction));
            
         
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
