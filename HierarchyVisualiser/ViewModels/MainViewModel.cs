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
        private ObservableCollection<TypeViewModel> _shownTypes;
        private ObservableCollection<ConnectionViewModel> _connections = new ObservableCollection<ConnectionViewModel>();

        public MainViewModel()
        {
            ShownTypes = new ObservableCollection<TypeViewModel>();
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
            var classViewModel = (TypeViewModel)sender;
            if (classViewModel.IsSelected)
                ShownTypes.Add(classViewModel);
            else
                ShownTypes.Remove(classViewModel);
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

        public GenericRelayCommand<TypeViewModel> ShowBaseCommand { get; set; }
        public GenericRelayCommand<TypeViewModel> ShowInterfacesCommand { get; set; }

        private void RegistCommands()
        {
            ShowBaseCommand = new GenericRelayCommand<TypeViewModel>(OnShowBaseCommandExecute, OnShowBaseCommandCanExecute);
            ShowInterfacesCommand = new GenericRelayCommand<TypeViewModel>(OnShowInterfacesCommandExecute);
        }

        private bool OnShowBaseCommandCanExecute(TypeViewModel classVm)
        {
            // Object doesn't have any base class -> disable
            return classVm?.WrappedType?.BaseType != null;
        }
            
        /// <summary>
        /// Connects the class with it's interfaces.
        /// </summary>
        private void OnShowInterfacesCommandExecute(TypeViewModel child)
        {
            foreach (var @interface in child.WrappedType.GetInterfaces())
            {
                var newInterface = GetOrCreateViewModel(@interface);
                Connect(child, newInterface);
            }
        }

        /// <summary>
        /// Connects the child with it's parent.
        /// </summary>
        private void OnShowBaseCommandExecute(TypeViewModel child)
        {
            var parent = GetOrCreateViewModel(child.WrappedType.BaseType);
            Connect(child, parent);
        }

        /// <summary>
        /// Get the TypeViewModel representing the type or create new ViewModel
        /// </summary>
        private TypeViewModel GetOrCreateViewModel(Type t)
        {
            var newInterface = ShownTypes.SingleOrDefault(c => c.FullName == t.FullName);
            if (newInterface == null)
            {
                newInterface = new TypeViewModel(t);
                ShownTypes.Add(newInterface);
            }

            return newInterface;
        }

        /// <summary>
        /// Creates new ConnectionViewModel and connects the child with it's parent
        /// </summary>
        private void Connect(TypeViewModel child, TypeViewModel parent)
        {
            Connections.Add(new ConnectionViewModel(child, parent, Models.ConnectionType.Inheritance));
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

        public ObservableCollection<TypeViewModel> ShownTypes
        {
            get
            {
                return _shownTypes;
            }
            set
            {
                if (_shownTypes == value)
                    return;

                _shownTypes = value;
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
