using System.Collections.ObjectModel;
using System.Reflection;

namespace HierarchyVisualiser.ViewModels
{
    /// <summary>
    /// MainViewModel Class. Contains a Collection of all visualised Classes.
    /// </summary>
    internal class MainViewModel : ViewModelBase
    {
        private ObservableCollection<AssemblyViewModel> _assemblies;

        public MainViewModel()
        {
            PopulateAssemblies();
        }

        private void PopulateAssemblies()
        {
            var ass1 = Assembly.LoadFile(@"C:\Users\Lukas\Source\Repos\HierarchyVisualiser\HierarchyVisualiser\bin\Debug\TestLibrary1.dll");
            var ass2 = Assembly.LoadFile(@"C:\Users\Lukas\Source\Repos\HierarchyVisualiser\HierarchyVisualiser\bin\Debug\TestLibrary2.dll");

            Assemblies = new ObservableCollection<AssemblyViewModel>(new AssemblyViewModel[] { new AssemblyViewModel(ass1), new AssemblyViewModel(ass2) });
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
    }
}
