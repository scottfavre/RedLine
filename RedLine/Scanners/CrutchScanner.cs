using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedLine.Scanners
{
    public interface ICrutchWordService
    {
        string[] Crutches { get; }
    }

    public class CrutchScanner : IScanner, ICrutchWordService
    {
        public CrutchScanner()
        {
            Enabled = true;
        }

        public string Name
        {
            get { return "Crutch Words"; }
        }

        private string[] _patterns = new[] {
            "started",
            "began",
            "saw",
            "thought"            
        };

        public IEnumerable<string> Patterns
        {
            get { return _patterns; }
        }

        public string[] Crutches
        {
            get { return _patterns; }
        }


        public bool Enabled { get; set; }

        public void InitFinder(Microsoft.Office.Interop.Word.Find find)
        {
            find.MatchWholeWord = true;
        }

    }
}
