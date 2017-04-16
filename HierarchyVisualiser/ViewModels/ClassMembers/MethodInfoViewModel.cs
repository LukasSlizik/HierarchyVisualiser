using System.Reflection;

namespace HierarchyVisualiser.ViewModels.ClassMembers
{
    /// <summary>
    /// ViewModel for a Class Method.
    /// </summary>
    internal class MethodInfoViewModel : ClassMemberViewModel
    {
        private MethodInfo _mi;

        public MethodInfoViewModel(MethodInfo mi)
        {
            _mi = mi;
        }

        public string Name => _mi.Name;

        public override MemberType MemberType => MemberType.Method;
    }
}