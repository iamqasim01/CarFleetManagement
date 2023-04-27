using CarFleetManagement_MobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CarFleetManagement_MobileApp.ViewModels
{
    public class AddVehicleInpectionViewModel : BaseViewModel
    {
        #region Fields
        private long? VehicleID = 0;
        private string _model = string.Empty;
        private string _brand = string.Empty;
        private string _registrationNo = string.Empty;
        private string _url = string.Empty;
        private DateTime _dateOfInspection = DateTime.Now.Date;
        private DateTime _dateOfNextInspection = DateTime.Now.Date;
        private bool _isOperational;

        #endregion Fields

        #region Constructor

        public AddVehicleInpectionViewModel(Vehicle vehicle)
        {
            VehicleID = vehicle.VehicleID;
            Model = vehicle.Model;
            Brand = vehicle.Brand;
            RegistrationNo = vehicle.RegistrationNo;
            URL = vehicle.Photo;
            SubmitCommand = new Command(ExecuteSubmitCommand);
        }

        #endregion Constructor

        #region Properties

        public string Model 
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }
        public string Brand
        {
            get => _brand;
            set => SetProperty(ref _brand, value);
        }
        public string RegistrationNo
        {
            get => _registrationNo;
            set => SetProperty(ref _registrationNo, value);
        }
        public string URL
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }
        public DateTime DateOfInspection
        {
            get => _dateOfInspection;
            set => SetProperty(ref _dateOfInspection, value);
        }        
        public DateTime DateOfNextInspection 
        {
            get => _dateOfNextInspection;
            set => SetProperty(ref _dateOfNextInspection, value);
        }
        public bool IsOperational 
        {
            get => _isOperational;
            set => SetProperty(ref _isOperational, value);
        }

        #endregion Properties

        #region Commands

        public Command SubmitCommand { get; }

        #endregion Commands

        #region Methods
        async void ExecuteSubmitCommand() 
        {
            try
            {
                string API = "http://192.168.1.46:82/api/vehicles";
                var values = new Dictionary<string, object>()
                {
                    {"VehicleID", VehicleID },
                    {"RegistrationNo", RegistrationNo },
                    {"Brand", Brand },
                    {"Model", Model },
                    {"CurrentInspectionDate", DateOfInspection.ToString("yyyy-MM-dd")},
                    {"NextInspectionDate", DateOfNextInspection.ToString("yyyy-MM-dd") },
                    {"IsRoadWorthy", IsOperational },
                };
                using (HttpClient client = new HttpClient()) 
                {
                    // Set the Content - Type header to "application/json"
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var json = JsonConvert.SerializeObject(values);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(API, content);
                    string result = await response.Content.ReadAsStringAsync();
                    string val = JsonConvert.DeserializeObject<string>(result);
                    if (response.IsSuccessStatusCode)
                    {
                        await Application.Current.MainPage.DisplayAlert("Successful!", "The inspection details have been added successfully.", "OK");
                        await Application.Current.MainPage.Navigation.PopModalAsync();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Failure!", val, "OK");
                    }

                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error!", "An error occured while processing.\nKindly contact to our support team.", "OK");
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion Methods
    }
}
