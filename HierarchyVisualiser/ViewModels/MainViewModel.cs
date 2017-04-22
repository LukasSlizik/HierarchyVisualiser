using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace HierarchyVisualiser.ViewModels
{
    /// <summary>
    /// MainViewModel Class. Contains a Collection of all visualised Classes.
    /// </summary>
    internal class MainViewModel : ViewModelBase, IAssemblyFileLoader
    {
        private ObservableCollection<AssemblyViewModel> _assemblies;
        private ObservableCollection<ClassViewModel> _selectedClasses;

        public MainViewModel()
        {
            SelectedClasses = new ObservableCollection<ClassViewModel>();

            PopulateAssemblies();
        }

        private void PopulateAssemblies()
        {
            var ass1 = Assembly.LoadFile(@"C:\Users\Lukas\Source\Repos\HierarchyVisualiser\HierarchyVisualiser\bin\Debug\TestLibrary1.dll");
            var ass2 = Assembly.LoadFile(@"C:\Users\Lukas\Source\Repos\HierarchyVisualiser\HierarchyVisualiser\bin\Debug\TestLibrary2.dll");

            Assemblies = new ObservableCollection<AssemblyViewModel>(new AssemblyViewModel[] { new AssemblyViewModel(ass1), new AssemblyViewModel(ass2) });
            RegisterEventHandlersOnAssemblies();
        }

        private void RegisterEventHandlersOnAssemblies()
        {
            foreach (var assembly in Assemblies)
            {
                assembly.SelectionChanged -= OnSelectionChanged;
                assembly.SelectionChanged += OnSelectionChanged;
            }
        }

        private void OnSelectionChanged(object sender, EventArgs args)
        {
            var classViewModel = (ClassViewModel)sender;
            if (classViewModel.IsSelected)
                SelectedClasses.Add(classViewModel);
            else
                SelectedClasses.Remove(classViewModel);
        }

        public bool TryLoadAssemblyFromFile(string file)
        {
            try
            {
                LoadAssemblyFromFile(file);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public void LoadAssemblyFromFile(string file)
        {
            var a = Assembly.LoadFile(file);
            var vm = new AssemblyViewModel(a);
            Assemblies.Add(new AssemblyViewModel(a));
            RegisterEventHandlersOnAssemblies();
        }

        /// <summary>
        /// Collection of all assemblies that are shown in the Navigation Tree.
        /// </summary>
        public ObservableCollection<AssemblyViewModel> Assemblies
        {
            get { return _assemblies; }
            set
            {
                if (value == _assemblies)
                    return;

                _assemblies = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ClassViewModel> SelectedClasses
        {
            get
            {
                return _selectedClasses;
            }
            set
            {
                if (_selectedClasses == value)
                    return;

                _selectedClasses = value;
                RaisePropertyChanged();
            }
        }
    }
}
