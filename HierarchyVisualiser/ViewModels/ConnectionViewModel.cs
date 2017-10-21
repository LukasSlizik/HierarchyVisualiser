namespace HierarchyVisualiser.ViewModels
{
    /// <summary>
    /// Represents the Connection between ClassViewModels.
    /// </summary>
    internal class ConnectionViewModel : ViewModelBase
    {
        public TypeViewModel Start { get; set; }
        public TypeViewModel End { get; set; }
    }
}
