using System.ComponentModel;
using TestMobile.Models;
using TestMobile.Tools;
using TestMobile.ViewModels;

namespace TestMobile.Views.Vehicles;

public partial class VehicleDetails : ContentPage
{
    public VehicleDetails(Vehicle vehicle)
	{
		InitializeComponent();
        BindingContext = new VehicleDetailsViewModel(vehicle);
    }

  

}