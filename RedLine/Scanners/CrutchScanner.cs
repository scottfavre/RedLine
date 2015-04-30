using RedLine.Crutch;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace RedLine.Scanners
{
    [Export(typeof(IScanner))]
    public class CrutchScanner : IScanner
    {
        [Import]
        public ICrutchWordService CrutchService { private get; set; }

        [Import]
        public ISettingsService Settings { private get; set; }

        public CrutchScanner()
        {
        }

        public string Name
        {
            get { return "Crutch Words"; }
        }

        public IEnumerable<string> Patterns
        {
            get { return CrutchService.CrutchWords; }
        }

        public bool Enabled
        {
            get { return Settings.CrutchScannerEnabled; }
            set { Settings.CrutchScannerEnabled = value; }
        }

        public void InitFinder(Microsoft.Office.Interop.Word.Find find)
        {
            find.MatchWholeWord = true;
        }
    }
}
