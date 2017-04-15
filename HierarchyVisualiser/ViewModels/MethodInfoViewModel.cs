using System.Reflection;

namespace HierarchyVisualiser.ViewModels
{
    /// <summary>
    /// ViewModel for a Class Method.
    /// </summary>
    internal class MethodInfoViewModel : ClassMemberViewModel
    {
        private MethodInfo _mi;

        public MethodInfoViewModel(MethodInfo mi) : this()
        {
            _mi = mi;
        }

        public MethodInfoViewModel() { }

        public string Name => _mi.Name;

        public override MemberType MemberType => MemberType.Method;
    }
}