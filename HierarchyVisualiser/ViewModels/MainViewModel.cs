using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace HierarchyVisualiser.ViewModels
{
    /// <summary>
    /// MainViewModel Class. Contains a Collection of all visualised Classes.
    /// </summary>
    internal class MainViewModel : ViewModelBase
    {
        private ObservableCollection<ClassViewModel> _classes;

        public MainViewModel()
        {
            PopulateClasses();
        }

        private void PopulateClasses()
        {
            Classes = new ObservableCollection<ClassViewModel>();

            var t = Type.GetType("HierarchyVisualiser.TestClass1");
            var propInfos = t.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            var methodInfos = t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Where(m => !m.IsSpecialName);
            var testClass1 = new ClassViewModel(t.Name, propInfos.Select(pi => new PropertyInfoViewModel(pi)), methodInfos.Select(mi => new MethodInfoViewModel(mi)));

            t = Type.GetType("HierarchyVisualiser.TestClass2");
            propInfos = t.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            methodInfos = t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Where(m => !m.IsSpecialName);
            var testClass2 = new ClassViewModel(t.Name, propInfos.Select(pi => new PropertyInfoViewModel(pi)), methodInfos.Select(mi => new MethodInfoViewModel(mi)));

            Classes.Add(testClass1);
            Classes.Add(testClass2);
        }

        /// <summary>
        /// All Classes that are being visualised.
        /// </summary>
        public ObservableCollection<ClassViewModel> Classes
        {
            get { return _classes; }
            set
            {
                if (value == _classes)
                    return;

                _classes = value;
                RaisePropertyChanged();
            }
        }
    }
}
