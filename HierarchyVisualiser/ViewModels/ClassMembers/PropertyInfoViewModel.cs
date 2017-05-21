using System.Reflection;

namespace HierarchyVisualiser.ViewModels.ClassMembers
{
    /// <summary>
    /// ViewModel for a Class Property.
    /// </summary>
    internal class PropertyInfoViewModel : ClassMemberViewModel
    {
        private PropertyInfo _pi;

        public PropertyInfoViewModel(PropertyInfo pi)
        {
            _pi = pi;
        }

        public string Name => _pi.Name;

        public override MemberType MemberType => MemberType.Property;
    }
}