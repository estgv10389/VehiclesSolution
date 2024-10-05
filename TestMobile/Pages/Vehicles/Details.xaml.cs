using System.ComponentModel;
using TestMobile.Models;
using TestMobile.Tools;

namespace TestMobile.Pages.Vehicles;

public partial class Details : ContentPage, INotifyPropertyChanged
{

    private ImageSource _imageSource;
    public ImageSource ImageSource
    {
        get => _imageSource;
        set
        {
            if (_imageSource != value)
            {
                _imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }

    }

    public Details(Vehicle vehicle)
	{
		InitializeComponent();

        var tools =  new Helper();
        if(vehicle.Image != null)
        {
            ImageSource = tools.ConvertByteArrayToImage(vehicle.Image);
        }
        else
        {
            ImageSource = ImageSource.FromFile("dotnet_bot.png");
        }

        Brand.Text = vehicle.Brand;
        Model.Text = vehicle.Model;

        this.BindingContext = this;

    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


}