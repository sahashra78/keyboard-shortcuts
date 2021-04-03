using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keyboard_Shortcut_API.Models
{
    public class Shortcut
    {
        public int Id { get; set; }
        public string Keys { get; set; }
        public string Function { get; set; }
        public int OsId { get; set; }
        public string Source { get; set; }

        [JsonIgnore]
        public virtual OperatingSystem OperatingSystem { get; set; }
    }
}
