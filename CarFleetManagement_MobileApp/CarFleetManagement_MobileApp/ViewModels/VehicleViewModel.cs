using CarFleetManagement_MobileApp.Models;
using CarFleetManagement_MobileApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CarFleetManagement_MobileApp.ViewModels
{
    public class VehicleViewModel : BaseViewModel
    {
        #region Fields
        private ObservableCollection<Vehicle> _vehicles;
        private Vehicle _selectedItem;
        private string _searchText;
        private bool isBusy = false;
        private int count = 0;
        #endregion

        #region Constructor
        public VehicleViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Vehicles = new ObservableCollection<Vehicle>();
            LoadVehicles = new Command(async () => await GetVehicles());
            VehicleTapCommand = new Command<Vehicle>(ExecuteVehicleTapCommand);
            SearchCommand = new Command(ExecuteSearchCommand);
        }
        #endregion Constructor

        #region Properties
        public INavigation Navigation { get; set; }
        public ObservableCollection<Vehicle> Vehicles 
        {
            get => _vehicles;
            private set => SetProperty(ref _vehicles, value);
        }
        public Vehicle SelectedItem 
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        public int Count 
        {
            get => count;
            set => SetProperty(ref count, value);
        }
        public string SearchText 
        {
            get => _searchText;
            set 
            {
                if(_searchText != value) 
                {
                    SetProperty(ref _searchText, value);
                    ExecuteSearchCommand();
                }
            }
        }
        #endregion

        #region Commands
        public Command VehicleTapCommand { get; }
        public Command LoadVehicles { get; }
        public Command SearchCommand { get; }
        #endregion Commands

        #region Methods
        /// <summary>
        /// Retreives vehicles/cars from server
        /// </summary>
        async Task GetVehicles() 
        {
            IsBusy = true;

            try
            {
                Vehicles.Clear();
                var client = new HttpClient();
                string API = "http://192.168.1.46:82/api/vehicles";
                var vehicleDetails = await client.GetAsync(API);
                string vehicleJson = vehicleDetails.Content.ReadAsStringAsync().Result;
                Vehicles = JsonConvert.DeserializeObject<ObservableCollection<Vehicle>>(vehicleJson);
                Count = Vehicles.Count;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error!", "Dear User,\nAn error occurred while processing.\nPlease contact with support team.\nThanks", "OK");
                Debug.WriteLine(ex.Message);
            }
            finally 
            {
                IsBusy = false;
            }
        }
        /// <summary>
        /// Retreives vehicles/cars from server
        /// </summary>
        /// <param name="registrationNo"></param>
        async void GetVehicles(string search)
        {
            try
            {
                Vehicles.Clear();
                var client = new HttpClient();
                string API = "http://192.168.1.46:82/api/Vehicles/search?search=" + search + "";
                var vehicleDetails = await client.GetAsync(API);
                string vehicleJson = vehicleDetails.Content.ReadAsStringAsync().Result;
                if (vehicleDetails.IsSuccessStatusCode)
                    Vehicles = JsonConvert.DeserializeObject<ObservableCollection<Vehicle>>(vehicleJson);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error!","Dear User,\nAn error occurred while processing.\nPlease contact with support team.\nThanks","OK");
                Debug.WriteLine(ex.Message);
            }
        }

        public void OnAppearing() 
        {
            IsBusy = true;
            SelectedItem = null;
        }
        async void ExecuteVehicleTapCommand(Vehicle vehicle)
        {
            await Navigation.PushModalAsync(new AddVehicleInpection(vehicle));
        }
        void ExecuteSearchCommand() 
        {
            GetVehicles(SearchText);
        }
        #endregion Methods
    }
}
