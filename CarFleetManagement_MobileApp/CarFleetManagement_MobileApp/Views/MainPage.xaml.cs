using CarFleetManagement_MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CarFleetManagement_MobileApp
{
    public partial class MainPage : ContentPage
    {
        VehicleViewModel _viewModel;
        public MainPage()
        {
            InitializeComponent();
            BindingContext =_viewModel= new VehicleViewModel(Navigation);
        }
        protected override void OnAppearing()
        {
            _viewModel.OnAppearing();
            base.OnAppearing();
        }
    }
}
