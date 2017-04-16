using System.Reflection;

namespace HierarchyVisualiser.ViewModels.ClassMembers
{
    /// <summary>
    /// ViewModel for a Class Property.
    /// </summary>
    internal class PropertyInfoViewModel : ClassMemberViewModel
    {
        private PropertyInfo _pi;

        public PropertyInfoViewModel(PropertyInfo pi) : this()
        {
            _pi = pi;
        }

        public PropertyInfoViewModel() { }

        public string Name => _pi.Name;

        public override MemberType MemberType => MemberType.Property;
    }
}