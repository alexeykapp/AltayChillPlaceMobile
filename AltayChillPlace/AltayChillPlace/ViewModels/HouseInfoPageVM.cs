﻿using AltayChillPlace.ApiResponses;
using AltayChillPlace.Services;
using AltayChillPlace.Helpers;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using AltayChillPlace.Interface;
using System.Collections.ObjectModel;
using Prism.Commands;
using AltayChillPlace.NavigationFile;
using AltayChillPlace.Views;

namespace AltayChillPlace.ViewModels
{
    public class HouseInfoPageVM : BindableBase
    {
        private readonly IHouseDataService _houseDataService;
        private HouseResponse _house;
        private HouseResponse _houseResponse;
        private PhotosHouseResponse _photosHouseResponse;
        private ObservableCollection<ImageSource> _photosHouse;
        private ObservableCollection<ListImageSource> _listImageSources;

        private bool _isLoadingVisible = true;
        public HouseInfoPageVM(IHouseDataService houseDataSevice)
        {
            _houseDataService = houseDataSevice;
            _listImageSources = new ObservableCollection<ListImageSource>();
            ShowBookingPageCommand = new DelegateCommand(ShowBookingPage);
        }

        public HouseResponse ItemHouse
        {
            get => _house;
            set
            {
                SetProperty(ref _house, value);
                LoadingAllDataHouse();
            }
        }

        private async void LoadingAllDataHouse()
        {
            try
            {
                _photosHouseResponse = await _houseDataService.GetPhotoHouseByIdAsync(_house.IdHouse);
            }
            catch { }
            if (_photosHouseResponse != null && _photosHouseResponse.Photos.Count > 0)
            {
                var listPhoto = await ImageConverter.ConvertBase64StringToImages(_photosHouseResponse.Photos);
                PhotosHouse = listPhoto;
                FillList();
            }
            else
            {
                var image = ImageSource.FromFile("logo.png");
                var photo = new ListImageSource { ImageSource  = image };
                ListImageSources.Add(photo);
                IsLoadingVisible = false;
            }
        }
        public DelegateCommand ShowBookingPageCommand{ get; set; }
        public ObservableCollection<ImageSource> PhotosHouse
        {
            get => _photosHouse;
            set
            {
                SetProperty(ref _photosHouse, value);
                if (value != null && value.Count > 0)
                {
                    IsLoadingVisible = false;
                }
            }
        }
        public bool IsLoadingVisible
        {
            get => _isLoadingVisible;
            set => SetProperty(ref _isLoadingVisible, value);
        }
        public ObservableCollection<ListImageSource> ListImageSources
        {
            get => _listImageSources;
            set => SetProperty(ref _listImageSources, value);
        }
        private void FillList()
        {
            ListImageSources = new ObservableCollection<ListImageSource>();
            foreach (var photo in PhotosHouse)
            {
                ListImageSources.Add(new ListImageSource { ImageSource = photo });
            }
        }
        private async void ShowBookingPage()
        {
            await NavigationDispatcher.Instance.Navigation.PushAsync(new Booking(_house));
        }
    }
}
