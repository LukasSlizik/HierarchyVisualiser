using System.Reflection;
using System.ComponentModel;

namespace HierarchyVisualiser.ViewModels
{
    /// <summary>
    /// ViewModel for a Class Method.
    /// </summary>
    public class MethodInfoViewModel : ViewModelBase
    {
        private MethodInfo _mi;

        public MethodInfoViewModel(MethodInfo mi)
        {
            _mi = mi;
        }

        public string Name => _mi.Name;
    }
}