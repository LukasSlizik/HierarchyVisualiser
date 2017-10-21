using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HierarchyVisualiser.Helpers
{
    public static class Extensions
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            foreach (var i in items)
                collection.Add(i);
        }
    }
}
