using CarFleetManagement_MobileApp.Models;
using CarFleetManagement_MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarFleetManagement_MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddVehicleInpection : ContentPage
    {
        public AddVehicleInpection(Vehicle vehicle)
        {
            InitializeComponent();
            BindingContext = new AddVehicleInpectionViewModel(vehicle);
        }
    }
}