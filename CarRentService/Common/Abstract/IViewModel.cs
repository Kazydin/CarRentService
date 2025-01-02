using System.ComponentModel;
using CarRentService.Common.Attributes;

namespace CarRentService.Common.Abstract;

[InjectDI]
public abstract class IViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}