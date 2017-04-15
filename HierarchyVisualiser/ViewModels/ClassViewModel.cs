using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HierarchyVisualiser.ViewModels
{
    /// <summary>
    /// Represents a Class, identiefied by its Name, as a Collection of Methods and Properties.
    /// </summary>
    internal class ClassViewModel : ViewModelBase
    {
        private ObservableCollection<MethodInfoViewModel> _methods;
        private ObservableCollection<PropertyInfoViewModel> _properties;

        public ClassViewModel(string className, IEnumerable<PropertyInfoViewModel> properties, IEnumerable<MethodInfoViewModel> methods)
        {
            _className = className;
            _properties = new ObservableCollection<PropertyInfoViewModel>(properties);
            _methods = new ObservableCollection<MethodInfoViewModel>(methods);
        }

        /// <summary>
        /// Class Name.
        /// </summary>
        private string _className;
        public string ClassName
        {
            get
            {
                return _className;
            }
            set
            {
                if (value == _className)
                    return;
                _className = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Collection of Class Properties.
        /// </summary>
        public ObservableCollection<PropertyInfoViewModel> Properties
        {
            get
            {
                return _properties;
            }
            set
            {
                if (_properties == value)
                    return;

                _properties = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Collection of Class Methods.
        /// </summary>
        public ObservableCollection<MethodInfoViewModel> Methods
        {
            get
            {
                return _methods;
            }
            set
            {
                if (_methods == value)
                    return;

                _methods = value;
                RaisePropertyChanged();
            }
        }
    }
}
