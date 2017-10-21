using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq;
using System;
using HierarchyVisualiser.ViewModels.ClassMembers;

namespace HierarchyVisualiser.ViewModels
{
    /// <summary>
    /// Represents a Namespace as a collection of .NET Types (Classes, Enums, Delegates, ...)
    /// </summary>
    internal class NamespaceViewModel : ViewModelBase
    {
        private string _namespace;
        private ObservableCollection<TypeViewModel> _classes;
        private Assembly _assembly;
        public event EventHandler SelectionChanged;

        public NamespaceViewModel(string @namespace, Assembly @assembly)
        {
            _namespace = @namespace;
            _assembly = @assembly;

            PopulateClasses();
        }

        private void OnSelectionChanged(object sender, EventArgs args)
        {
            SelectionChanged?.Invoke(sender, null);
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
        public ObservableCollection<TypeViewModel> Classes
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
            Classes = new ObservableCollection<TypeViewModel>();
            var exportedTypes = _assembly.GetExportedTypes().Where(t => t.Namespace == Namespace);
            
            foreach(var t in exportedTypes)
            {
                var c = new TypeViewModel(t);
                c.SelectionChanged += OnSelectionChanged;
                Classes.Add(c);
            }
        }
    }
}
