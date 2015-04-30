using Microsoft.Office.Interop.Word;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace RedLine.Scanners
{
    [Export(typeof(IScanner))]
    public class AdverbScanner: IScanner
    {
        [Import]
        public ISettingsService Settings { private get; set; }

        public AdverbScanner()
        {
        }

        public string Name
        {
            get { return "Adverbs"; }
        }

        public IEnumerable<string> Patterns
        {
            get { yield return "ly"; }
        }

        public bool Enabled 
        {
            get { return Settings.AdverbScannerEnabled; }
            set { Settings.AdverbScannerEnabled = value; }
        }

        public void InitFinder(Find find)
        {
            find.MatchSuffix = true;
            find.MatchWholeWord = false;
        }
    }
}
