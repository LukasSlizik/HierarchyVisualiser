using System.Collections.Generic;
using System.Collections.ObjectModel;
using HierarchyVisualiser.ViewModels.ClassMembers;
using System;

namespace HierarchyVisualiser.ViewModels
{
    /// <summary>
    /// Represents a Class, identiefied by its Name, as a Collection of Class Members (Methods, Events, Constructors, Properties).
    /// </summary>
    internal class ClassViewModel : ViewModelBase
    {
        private ObservableCollection<MethodInfoViewModel> _methods;
        private ObservableCollection<PropertyInfoViewModel> _properties;
        private bool _isSelected;
        private string _className;
        internal event EventHandler SelectionChanged;

        public ClassViewModel(string className, IEnumerable<PropertyInfoViewModel> properties, IEnumerable<MethodInfoViewModel> methods)
        {
            _className = className;
            _properties = new ObservableCollection<PropertyInfoViewModel>(properties);
            _methods = new ObservableCollection<MethodInfoViewModel>(methods);
        }

        /// <summary>
        /// Gets or sets if the Class was selected in the Navigation Tree.
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (_isSelected == value)
                    return;
                _isSelected = value;

                SelectionChanged?.Invoke(this, null);
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets of Sets the Class Name.
        /// </summary>
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
