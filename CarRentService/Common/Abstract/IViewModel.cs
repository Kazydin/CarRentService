using System.ComponentModel;
using CarRentService.Common.Attributes;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.Common.Abstract;

[InjectDI]
public abstract class IViewModel : ObservableObject;