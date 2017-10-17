using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HierarchyVisualiser.ViewModels
{
    internal class ConnectionViewModel : ViewModelBase
    {
        public ClassViewModel Start { get; set; }
        public ClassViewModel End { get; set; }
    }
}
