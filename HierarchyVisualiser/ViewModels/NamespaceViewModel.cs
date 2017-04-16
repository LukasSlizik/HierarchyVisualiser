using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq;
using HierarchyVisualiser.ViewModels.ClassMembers;

namespace HierarchyVisualiser.ViewModels
{
    /// <summary>
    /// Represents a Namespace as a collection of .NET Types (Classes, Enums, Delegates, ...)
    /// </summary>
    internal class NamespaceViewModel : ViewModelBase
    {
        private string _namespace;
        private ObservableCollection<ClassViewModel> _classes;
        private Assembly _assembly;

        public NamespaceViewModel(string @namespace, Assembly @assembly)
        {
            _namespace = @namespace;
            _assembly = @assembly;

            PopulateClasses();
        }

        /// <summary>
        /// Gets of Sets the Name of the Namespace.
        /// </summary>
        public string Namespace
        {
            get
            {
                return _namespace;
            }
            set
            {
                if (_namespace == value)
                    return;
                _namespace = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Collection of classes contained within the Namespace.
        /// </summary>
        public ObservableCollection<ClassViewModel> Classes
        {
            get
            {
                return _classes;
            }
            set
            {
                if (_classes == value)
                    return;
                _classes = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Initialize the Collection of all Classes contained within this Namespace.
        /// </summary>
        private void PopulateClasses()
        {
            Classes = new ObservableCollection<ClassViewModel>();
            var exportedTypes = _assembly.GetExportedTypes().Where(t => t.Namespace == Namespace);
            
            foreach(var t in exportedTypes)
            {
                var propInfos = t.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
                var methodInfos = t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Where(m => !m.IsSpecialName);
                var testClass = new ClassViewModel(t.Name, propInfos.Select(pi => new PropertyInfoViewModel(pi)), methodInfos.Select(mi => new MethodInfoViewModel(mi)));
                Classes.Add(testClass);
            }
        }
    }
}
