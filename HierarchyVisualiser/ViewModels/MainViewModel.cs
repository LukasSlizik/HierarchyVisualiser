using HierarchyVisualiser.Commands;
using HierarchyVisualiser.Contracts;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace HierarchyVisualiser.ViewModels
{
    /// <summary>
    /// MainViewModel Class. Contains a Collection of all visualised Classes.
    /// </summary>
    internal class MainViewModel : ViewModelBase, IAssemblyFileLoader
    {
        private ObservableCollection<AssemblyViewModel> _assemblies = new ObservableCollection<AssemblyViewModel>();
        private ObservableCollection<ClassViewModel> _shownClasses;
        private ObservableCollection<ConnectionViewModel> _connections = new ObservableCollection<ConnectionViewModel>();

        public MainViewModel()
        {
            ShownClasses = new ObservableCollection<ClassViewModel>();
            RegisterEventHandlersOnAssemblies();
            RegistCommands();

            TryLoadAssemblyFromFile(@"C:\Users\Lukas\Desktop\nblackbox.dll");
        }

        /// <summary>
        /// EventHandlers for assemblies are registered.
        /// </summary>
        private void RegisterEventHandlersOnAssemblies()
        {
            foreach (var assembly in Assemblies)
            {
                assembly.SelectionChanged -= OnSelectionChanged;
                assembly.SelectionChanged += OnSelectionChanged;

                assembly.AssemblyRemoved -= OnAssemblyRemoved;
                assembly.AssemblyRemoved += OnAssemblyRemoved;
            }
        }

        private void OnAssemblyRemoved(object sender, EventArgs args)
        {
            Assemblies.Remove((AssemblyViewModel)sender);
        }

        private void OnSelectionChanged(object sender, EventArgs args)
        {
            var classViewModel = (ClassViewModel)sender;
            if (classViewModel.IsSelected)
                ShownClasses.Add(classViewModel);
            else
                ShownClasses.Remove(classViewModel);
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

        public GenericRelayCommand<ClassViewModel> ShowBaseCommand { get; set; }
        public GenericRelayCommand<ClassViewModel> ShowInterfacesCommand { get; set; }

        private void RegistCommands()
        {
            ShowBaseCommand = new GenericRelayCommand<ClassViewModel>(OnShowBaseCommandExecute);
            ShowInterfacesCommand = new GenericRelayCommand<ClassViewModel>(OnShowInterfacesCommandExecute);
        }

        private void OnShowInterfacesCommandExecute(ClassViewModel classVm)
        {
            if (classVm == null)
                throw new ArgumentNullException(nameof(classVm));

            var wrappedType = classVm.WrappedType;

            foreach (var @interface in wrappedType.GetInterfaces())
            {
                var newInterface = new ClassViewModel(@interface);
                if (ShownClasses.Contains(newInterface))
                {
                    var searchedInterface = ShownClasses.Single(c => c.WrappedType == newInterface.WrappedType);
                    CreateNewConnection(classVm, searchedInterface);
                    continue;
                }

                CreateNewConnection(classVm, newInterface);
                ShownClasses.Add(newInterface);
            }
        }

        /// <summary>
        /// ToDo: Refactoring
        /// </summary>
        private void OnShowBaseCommandExecute(ClassViewModel classVm)
        {
            if (classVm == null)
                throw new ArgumentNullException(nameof(classVm));

            var baseType = classVm.WrappedType.BaseType;

            // if t is already object, then there is no base type
            if (baseType == null)
                return;

            var newClassVm = new ClassViewModel(baseType);
            if (ShownClasses.Contains(newClassVm))
            {
                var baseClass = ShownClasses.Single(c => c.WrappedType == newClassVm.WrappedType);
                classVm.ParentClassViewModel = baseClass;

                CreateNewConnection(classVm, baseClass);
                return;
            }
            else
            {
                classVm.ParentClassViewModel = newClassVm;
                ShownClasses.Add(newClassVm);

                CreateNewConnection(classVm, newClassVm);
                return;
            }
        }

        private void CreateNewConnection(ClassViewModel start, ClassViewModel end)
        {
            var connectionVm = new ConnectionViewModel();
            connectionVm.Start = start;
            connectionVm.End = end;
            Connections.Add(connectionVm);
        }

        public void LoadAssemblyFromFile(string file)
        {
            try
            {
                var a = Assembly.LoadFile(file);
                Assemblies.Add(new AssemblyViewModel(a));
                RegisterEventHandlersOnAssemblies();
            }
            catch (Exception ex)
            {
                // todo: Log Exception
            }

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

        public ObservableCollection<ClassViewModel> ShownClasses
        {
            get
            {
                return _shownClasses;
            }
            set
            {
                if (_shownClasses == value)
                    return;

                _shownClasses = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ConnectionViewModel> Connections
        {
            get
            {
                return _connections;
            }
            set
            {
                if (_connections == value)
                    return;

                _connections = value;
                RaisePropertyChanged();
            }
        }
    }
}
