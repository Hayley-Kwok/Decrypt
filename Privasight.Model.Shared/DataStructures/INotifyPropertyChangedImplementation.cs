using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Privasight.Model.Shared.DataStructures;

/// <summary>
/// The implementation for INotifyPropertyChanged
/// </summary>
public abstract class INotifyPropertyChangedImplementation : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}