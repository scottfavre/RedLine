using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using RedLine.Scanners;
using Microsoft.Office.Tools;

namespace RedLine
{
    public interface IPanelVisiblity
    {
        bool ScannerPanel { get; set; }

        event EventHandler VisibilityChanged;
    }

    public partial class ThisAddIn: IPanelVisiblity
    {
        private RedLineRibbon _ribbon;
        private AdverbScanner _adverbScanner;
        private CrutchScanner _crutchScanner;
        private ScannerService _scannerService;

        private ScannerTaskPanel _scannerPanel;
        private CustomTaskPane _scannerContainer;

        protected override Office.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            _ribbon = new RedLineRibbon(this);
            _ribbon.StartScan += OnRibbon_StartScan;

            return _ribbon;
        }

        void OnRibbon_StartScan(object sender, EventArgs e)
        {
            var results = _scannerService.StartScan(Application.ActiveDocument);

            _scannerPanel.SetResults(results);
        }

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            _adverbScanner = new AdverbScanner();
            _crutchScanner = new CrutchScanner();
            _scannerService = new ScannerService(_adverbScanner, _crutchScanner);

            Application.DocumentOpen += Application_DocumentOpen;

            CreateScannerPanel();
        }

        void Application_DocumentOpen(Word.Document Doc)
        {
            CreateScannerPanel();
        }

        private void CreateScannerPanel()
        {
            bool visible = false;

            if(_scannerContainer != null)
            {
                visible = _scannerContainer.Visible;
            }

            _scannerPanel = new ScannerTaskPanel(_scannerService, _crutchScanner);
            _scannerContainer = CustomTaskPanes.Add(_scannerPanel, "RedLine");
            _scannerContainer.Visible = visible;
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion

        public bool ScannerPanel
        {
            get
            {
                return _scannerContainer.Visible;
            }
            set
            {
                if (_scannerContainer.Visible != value)
                {
                    _scannerContainer.Visible = value;
                    RaiseVisiblityChanged();
                }
            }
        }

        public event EventHandler VisibilityChanged;
        private void RaiseVisiblityChanged()
        {
            var handler = VisibilityChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}
