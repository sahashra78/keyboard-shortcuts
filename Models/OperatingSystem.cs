using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keyboard_Shortcut_API.Models
{
    public class OperatingSystem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Shortcut> Shortcuts { get; set; }

    }
}
