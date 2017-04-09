using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HierarchyVisualiser.ViewModels
{
    /// <summary>
    /// Base Class for all ViewModels.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}