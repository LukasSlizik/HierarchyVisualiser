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
    internal class ClassViewModel : ViewModelBase
    {
        private ObservableCollection<ClassMemberViewModel> _members = new ObservableCollection<ClassMemberViewModel>();
        private bool _isSelected;
        private string _className;
        internal event EventHandler SelectionChanged;
        private double _yCoord;
        private double _xCoord;
        private double _width;

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
