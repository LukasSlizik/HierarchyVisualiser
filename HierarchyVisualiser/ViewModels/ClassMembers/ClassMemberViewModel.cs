using System.Reflection;

namespace HierarchyVisualiser.ViewModels.ClassMembers
{
    /// <summary>
    /// Base Class for all MemberViewModels (Property, Method, Constructor, Event).
    /// </summary>
    internal class ClassMemberViewModel : ViewModelBase
    {
        public ClassMemberViewModel(MemberInfo mi, MemberType memberType)
        {
            MemberInfo = mi;
            MemberType = memberType;
        }

        public MemberType MemberType { get; }
        public MemberInfo MemberInfo { get; set; }
        public string Name => MemberInfo.Name;
        public string Info => MemberInfo.ToString();
    }
}