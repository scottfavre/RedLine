using Microsoft.Office.Interop.Word;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace RedLine.Scanners
{
    [Export(typeof(IScanner))]
    public class AdverbScanner: IScanner
    {
        public AdverbScanner()
        {
            Enabled = true;
        }

        public string Name
        {
            get { return "Adverbs"; }
        }

        public IEnumerable<string> Patterns
        {
            get { yield return "ly"; }
        }

        public bool Enabled { get; set; }

        public void InitFinder(Find find)
        {
            find.MatchSuffix = true;
            find.MatchWholeWord = false;
        }
    }
}
