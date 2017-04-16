namespace HierarchyVisualiser.ViewModels.ClassMembers
{
    /// <summary>
    /// Base Class for all MemberViewModels (Property, Method, Constructor, Event).
    /// </summary>
    internal abstract class ClassMemberViewModel : ViewModelBase
    {
        public abstract MemberType MemberType { get; }
    }
}