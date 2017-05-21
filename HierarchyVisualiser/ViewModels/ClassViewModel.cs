using System.Collections.Generic;
using System.Collections.ObjectModel;
using HierarchyVisualiser.ViewModels.ClassMembers;
using System.Reflection;
using System.Linq;
using System;

namespace HierarchyVisualiser.ViewModels
{
    /// <summary>
    /// Represents a Class, identiefied by its Name, as a Collection of Class Members (Methods, Events, Constructors, Properties).
    /// </summary>
    internal class ClassViewModel : ViewModelBase
    {
        private ObservableCollection<MethodInfoViewModel> _methods = new ObservableCollection<MethodInfoViewModel>();
        private ObservableCollection<PropertyInfoViewModel> _properties = new ObservableCollection<PropertyInfoViewModel>();
        private ObservableCollection<ConstructorInfoViewModel> _ctors = new ObservableCollection<ConstructorInfoViewModel>();
        private Type _wrappedType;
        private bool _isSelected;
        private string _className;
        internal event EventHandler SelectionChanged;

        public ClassViewModel(Type t)
        {
            _wrappedType = t;
            ClassName = t.Name;

            PopulateWithClassMembers();
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
        /// Initialize the Collection of all Classes contained within this Namespace.
        /// </summary>
        private void PopulateWithClassMembers()
        {
            var ctors = ((TypeInfo)_wrappedType).DeclaredConstructors;
            Ctors = new ObservableCollection<ConstructorInfoViewModel>(ctors.Select(ci => new ConstructorInfoViewModel(ci)));

            var propInfos = _wrappedType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            Properties = new ObservableCollection<PropertyInfoViewModel>(propInfos.Select(pi => new PropertyInfoViewModel(pi)));

            var methodInfos = _wrappedType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Where(m => !m.IsSpecialName);
            Methods = new ObservableCollection<MethodInfoViewModel>(methodInfos.Select(mi => new MethodInfoViewModel(mi)));
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

        /// <summary>
        /// Collection of Class Constructors.
        /// </summary>
        public ObservableCollection<ConstructorInfoViewModel> Ctors
        {
            get
            {
                return _ctors;
            }
            set
            {
                if (_ctors == value)
                    return;

                _ctors = value;
                RaisePropertyChanged();
            }
        }
    }
}
