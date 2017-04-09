using System.Reflection;

namespace HierarchyVisualiser.ViewModels
{
    /// <summary>
    /// ViewModel for a Class Property.
    /// </summary>
    public class PropertyInfoViewModel : ViewModelBase
    {
        private PropertyInfo _pi;

        public PropertyInfoViewModel(PropertyInfo pi)
        {
            _pi = pi;
        }

        public string Name => _pi.Name;
    }
}