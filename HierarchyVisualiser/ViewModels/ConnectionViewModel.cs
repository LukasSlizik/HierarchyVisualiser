using HierarchyVisualiser.Models;

namespace HierarchyVisualiser.ViewModels
{
    /// <summary>
    /// Represents the Connection between ClassViewModels.
    /// </summary>
    internal class ConnectionViewModel : ViewModelBase
    {
        public ConnectionViewModel(TypeViewModel start, TypeViewModel end, ConnectionType type)
        {
            Start = start;
            End = end;
            ConnectionType = type;
        }

        public TypeViewModel Start { get; }
        public TypeViewModel End { get; }
        public ConnectionType ConnectionType { get; }
    }
}
