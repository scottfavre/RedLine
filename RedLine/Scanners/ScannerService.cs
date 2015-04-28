using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace RedLine.Scanners
{
    public interface IScannerService
    {
        IEnumerable<IScanner> Scanners { get; }

        ScanResults StartScan(Document document);
    }

    [Export(typeof(IScannerService))]
    public class ScannerService : IScannerService, IPartImportsSatisfiedNotification
    {
        private IScanner[] _scanners;

        [ImportMany]
        public IEnumerable<IScanner> ScannerImport { get; set; }

        public ScannerService()
        {
        }

        public void OnImportsSatisfied()
        {
            _scanners = ScannerImport.ToArray();
        }

        public IEnumerable<IScanner> Scanners
        {
            get { return _scanners; }
        }

        public ScanResults StartScan(Document document)
        {
            var results = new ScanResults();

            foreach(var scanner in _scanners.Where(scanner => scanner.Enabled))
            {
                foreach (var pattern in scanner.Patterns)
                {
                    var current = document.Content;
                    var find = current.Find;
                    find.ClearFormatting();
                    find.Text = pattern;
                    find.MatchCase = false;

                    scanner.InitFinder(find);

                    while(find.Execute())
                    {
                        results.Add(current.Duplicate);
                    }
                }
            }

            results.Complete();

            return results;
        }
    }
}
