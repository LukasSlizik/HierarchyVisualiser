using HierarchyVisualiser.Helpers;
using HierarchyVisualiser.ViewModels.ClassMembers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace HierarchyVisualiser.ViewModels
{
    /// <summary>
    /// Represents a Class, identiefied by its Name, as a Collection of Class Members (Methods, Events, Constructors, Properties).
    /// </summary>
    internal class ClassViewModel : ViewModelBase, IEquatable<ClassViewModel>
    {
        private ObservableCollection<ClassMemberViewModel> _members = new ObservableCollection<ClassMemberViewModel>();
        private bool _isSelected;
        private string _className;
        internal event EventHandler SelectionChanged;
        private ClassViewModel _parentClassViewModel;
        private double _yCoord;
        private double _xCoord;
        private double _width;
        private double _height;

        public ClassViewModel(Type t)
        {
            WrappedType = t;
            ClassName = t.Name;

            PopulateWithClassMembers();
        }

        /// <summary>
        /// Actual Width of the ClassView
        /// </summary>
        public double Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Actual Height of the ClassView
        /// </summary>
        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                RaisePropertyChanged();
            }
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
        /// Populates this instance with its class members.
        /// </summary>
        private void PopulateWithClassMembers()
        {
            var eventInfo = WrappedType.GetEvents(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            Members.AddRange(eventInfo.Select(ei => new ClassMemberViewModel(ei, MemberType.Event)));

            var methodInfo = WrappedType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Where(m => !m.IsSpecialName);
            Members.AddRange(methodInfo.Select(mi => new ClassMemberViewModel(mi, MemberType.Method)));

            var propInfo = WrappedType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            Members.AddRange(propInfo.Select(pi => new ClassMemberViewModel(pi, MemberType.Property)));

            var ctors = ((TypeInfo)WrappedType).DeclaredConstructors.Cast<ConstructorInfo>();
            Members.AddRange(ctors.Select(ci => new ClassMemberViewModel(ci, MemberType.Constructor)));
        }

        /// <summary>
        /// ToDo: Object.Equals, Object.GetHashCode, Equality und Inequality Op implementieren
        /// </summary>
        public bool Equals(ClassViewModel other)
        {
            if (other == null)
                return false;

            if (WrappedType == other.WrappedType)
                return true;

            return false;
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

        public Type WrappedType { get; }

        public ClassViewModel ParentClassViewModel
        {
            get
            {
                return _parentClassViewModel;
            }
            set
            {
                _parentClassViewModel = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// X Coordinate on the Canvas.
        /// </summary>
        public double Y
        {
            get { return _yCoord; }
            set
            {
                if (value < 0)
                    return;

                _yCoord = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// X Coordinate on the Canvas.
        /// </summary>
        public double X
        {
            get { return _xCoord; }
            set
            {
                if (value < 0)
                    return;

                _xCoord = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Collection of Class Properties.
        /// </summary>
        public ObservableCollection<ClassMemberViewModel> Members
        {
            get
            {
                return _members;
            }
            set
            {
                if (_members == value)
                    return;

                _members = value;
                RaisePropertyChanged();
            }
        }
    }
}
