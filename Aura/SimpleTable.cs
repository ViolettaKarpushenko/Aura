using System.Collections.Generic;

namespace Aura
{
    internal class SimpleTable
    {
        public string TableName { get; set; }
        public string Caption { get; set; }
        public Dictionary<string,string> Columns { get; set; }
    }
}