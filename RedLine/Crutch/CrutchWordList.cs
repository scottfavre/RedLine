using System.Collections.Generic;
using System.Globalization;

namespace RedLine.Crutch
{
    public class CrutchWordList
    {
        public CrutchWordList()
        {
        }

        public CrutchWordList(string name)
        {
            Name = name;
            Crutches = new HashSet<string>();
        }

        public string Name;
        public HashSet<string> Crutches;

        internal string Key
        {
            get
            {
                return Name.ToLower(CultureInfo.CurrentCulture);
            }
        }
    }
}
