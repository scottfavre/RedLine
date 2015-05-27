using Microsoft.Office.Interop.Word;
using RedLine.Homonym;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace RedLine.Scanners
{
    [Export(typeof(IScanner))]
    public class HomonymScanner: IScanner
    {
        [Import]
        public IHomonymService HomonymService { private get; set; }

        [Import]
        public ISettingsService Settings { private get; set; }


        public string Name
        {
            get { return "Homonyms"; }
        }

        public IEnumerable<string> Patterns
        {
            get
            {
                return HomonymService.Homonyms.SelectMany(h => h);
            }
        }

        public bool Enabled
        {
            get { return Settings.HomonymScannerEnabled; }
            set { Settings.HomonymScannerEnabled = value; }
        }

        public void InitFinder(Find find)
        {
            find.MatchWholeWord = true;
        }
    }
}
