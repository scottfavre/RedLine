using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedLine.Scanners
{
    public interface IScanner
    {
        string Name { get; }

        IEnumerable<string> Patterns { get; }

        bool Enabled { get; set; }

        void InitFinder(Find find);
    }
}
