using Syncfusion.Maui.DataGrid;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using TestMobile.Models;
using TestMobile.Services;
using static System.Net.Mime.MediaTypeNames;

namespace TestMobile.Pages.Vehicles;

public partial class VehiclesInit : ContentPage
{


    public VehiclesInit(VehicleService vehicleService)
	{
		InitializeComponent();
    }




}