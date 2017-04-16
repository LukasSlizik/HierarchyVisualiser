using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace HierarchyVisualiser.ViewModels
{
    /// <summary>
    /// Represents an .NET Assembly as a collection of Namespaces, identified by it's name.
    /// </summary>
    internal class AssemblyViewModel : ViewModelBase
    {
        private Assembly _wrapeedAssembly;
        private ObservableCollection<NamespaceViewModel> _namespaces;

        public AssemblyViewModel(Assembly assembly)
        {
            WrappedAssembly = assembly;
            PopulateNamespaces();
        }

        /// <summary>
        /// Populates the collection of all namespaces contained in this assembly.
        /// </summary>
        private void PopulateNamespaces()
        {
            Namespaces = new ObservableCollection<NamespaceViewModel>(WrappedAssembly.GetTypes().Select(t => new NamespaceViewModel(t.Namespace, WrappedAssembly)).Distinct());
        }

        /// <summary>
        /// FullName of the assembly.
        /// </summary>
        public string FullName => WrappedAssembly.FullName;

        /// <summary>
        /// Underlying assembly.
        /// </summary>
        public Assembly WrappedAssembly
        {
            get
            {
                return _wrapeedAssembly;
            }
            set
            {
                if (_wrapeedAssembly == value)
                    return;
                _wrapeedAssembly = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Collection of all Namespaces contained within the Assembly.
        /// </summary>
        public ObservableCollection<NamespaceViewModel> Namespaces
        {
            get
            {
                return _namespaces;
            }
            set
            {
                if (_namespaces == value)
                    return;

                _namespaces = value;
                RaisePropertyChanged();
            }
        }

    }
}
