using System.Reflection;

/// <summary>
/// ViewModel for a class constructor.
/// </summary>
namespace HierarchyVisualiser.ViewModels.ClassMembers
{
    internal class ConstructorInfoViewModel : ClassMemberViewModel
    {
        private ConstructorInfo _ci;

        public override MemberType MemberType => MemberType.Constructor;

        public ConstructorInfoViewModel(ConstructorInfo ci)
        {
            _ci = ci;
        }

        public string Name => _ci.Name;
    }
}
