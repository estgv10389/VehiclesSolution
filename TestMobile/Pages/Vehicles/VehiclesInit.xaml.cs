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
    private readonly VehicleService _vehicleService;

    public VehiclesInit(VehicleService vehicleService)
	{
		InitializeComponent();
        _vehicleService = vehicleService;
        LoadVehicles();
    }

    private async void LoadVehicles()
    {
        var jsonResponse = await _vehicleService.GetVehiclesAsync();
        var grid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
            },
            RowDefinitions =
            {
                new RowDefinition { Height = GridLength.Auto }
            }

        };

        AddCellWithBorder(grid, "Brand", 0, 0, null ,true);
        AddCellWithBorder(grid, "Model", 1, 0, null ,true);
        AddCellWithBorder(grid, "Year", 2, 0, null ,true);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        ApiResponse<object>? resp = JsonSerializer.Deserialize<ApiResponse<object>>(jsonResponse, options);
        if (resp != null && resp.Success)
        {
            if (resp.Data != null)
            {
                int rowIndex = 1;
                var vehicles = JsonSerializer.Deserialize<List<Vehicle>>(resp.Data.ToString(), options);
                 foreach (var vehicle in vehicles)
                 {
             
                    grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                    AddCellWithBorder(grid, vehicle.Brand, 0, rowIndex, vehicle);
                    AddCellWithBorder(grid, vehicle.Model, 1, rowIndex, vehicle);
                    AddCellWithBorder(grid, vehicle.Year.ToString(), 2, rowIndex, vehicle);
                    rowIndex++;

                }

                scrollView.Content = grid;
                scrollView.IsVisible = true;
             
               // loadingLabel.IsVisible = false;
            }
        }
    }

    private void AddCellWithBorder(Grid grid, string text, int column, int row, Vehicle? vehicle, bool isHeader = false)
    {
        var label = new Label
        {
            Text = text,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontAttributes = isHeader ? FontAttributes.Bold : FontAttributes.None,
            Padding = new Thickness(10)
        };

        if (!isHeader)
        {
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) => OnRowTapped(vehicle!);  
            label.GestureRecognizers.Add(tapGesture);
        }

        var border = new Border
        {
            Stroke = Colors.Black,
            StrokeThickness = 1,
            Content = label
        };

        Grid.SetColumn(border, column);
        Grid.SetRow(border, row);
        grid.Children.Add(border);
    }

    private async void OnRowTapped(Vehicle vehicle)
    {
        await Navigation.PushAsync(new Details(vehicle));
    }
}