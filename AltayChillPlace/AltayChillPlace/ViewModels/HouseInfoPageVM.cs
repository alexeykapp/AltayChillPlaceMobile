using AltayChillPlace.ApiResponses;
using AltayChillPlace.Services;
using AltayChillPlace.Helpers;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using AltayChillPlace.Interface;
using System.Collections.ObjectModel;

namespace AltayChillPlace.ViewModels
{
    public class HouseInfoPageVM : BindableBase
    {
        private readonly IHouseDataService _houseDataService;
        private HouseResponse _house;
        private HouseResponse _houseResponse;
        private PhotosHouseResponse _photosHouseResponse;
        private ObservableCollection<ImageSource> _photosHouse;

        private bool _isLoadingVisible = true;
        public HouseInfoPageVM(IHouseDataService houseDataSevice)
        {
            _houseDataService = houseDataSevice;
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
            }
            else
            {
                PhotosHouse.Add(ImageSource.FromFile("@drawable/logo.png"));
                IsLoadingVisible = false;
            }
        }
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
    }
}
