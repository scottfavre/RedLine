using RedLine.Crutch;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Office = Microsoft.Office.Core;

// TODO:  Follow these steps to enable the Ribbon (XML) item:

// 1: Copy the following code block into the ThisAddin, ThisWorkbook, or ThisDocument class.

//  protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
//  {
//      return new RedLineRibbon();
//  }

// 2. Create callback methods in the "Ribbon Callbacks" region of this class to handle user
//    actions, such as clicking a button. Note: if you have exported this Ribbon from the Ribbon designer,
//    move your code from the event handlers to the callback methods and modify the code to work with the
//    Ribbon extensibility (RibbonX) programming model.

// 3. Assign attributes to the control tags in the Ribbon XML file to identify the appropriate callback methods in your code.  

// For more information, see the Ribbon XML documentation in the Visual Studio Tools for Office Help.


namespace RedLine.View
{
    [ComVisible(true)]
    public class RedLineRibbon : Office.IRibbonExtensibility, IPartImportsSatisfiedNotification
    {
        private Office.IRibbonUI ribbon;

        [Import]
        public IPanelVisiblity PanelVisibility { private get; set; }

        public RedLineRibbon()
        {
        }

        public void OnImportsSatisfied()
        {
            PanelVisibility.VisibilityChanged += (s, a) => ribbon.Invalidate();
        }

        public event EventHandler StartScan;
        private void RaiseStartScan()
        {
            var handler = StartScan;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs<string>> AddCrutch;
        private void RaiseAddCrutch(string crutch)
        {
            var handler = AddCrutch;
            if (handler != null)
                handler(this, new EventArgs<string>(crutch));
        }

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID)
        {
            return GetResourceText("RedLine.View.RedLineRibbon.xml");
        }

        #endregion

        #region Ribbon Callbacks
        //Create callback methods here. For more information about adding callback methods, visit http://go.microsoft.com/fwlink/?LinkID=271226

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;
        }

        public Bitmap GetButtonImage(Office.IRibbonControl control)
        {
            return Properties.Resources.search;
        }

        public void OnStartScanClicked(Office.IRibbonControl control)
        {
            RaiseStartScan();
        }

        public bool GetShowPanelChecked(Office.IRibbonControl control)
        {
            return PanelVisibility.ScannerPanel;
        }

        public void ShowPanelChecked(Office.IRibbonControl control, bool isChecked)
        {
            PanelVisibility.ScannerPanel = isChecked;
        }


        public bool CanAddCrutchWord(Office.IRibbonControl control)
        {
            var currentRange = Globals.ThisAddIn.Application.Selection.Range;

            return currentRange.Words.Count == 1;
        }

        public void AddCrutchWord(Office.IRibbonControl control)
        {
            var currentRange = Globals.ThisAddIn.Application.Selection.Range;

            var wordRange = currentRange.Words.First;

            var word = wordRange.Text.Trim().ToLower(CultureInfo.CurrentCulture);

            if (word.Any(c => Char.IsLetter(c)))
            {
                RaiseAddCrutch(word);
            }
        }

        #endregion

        #region Helpers

        private static string GetResourceText(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for (int i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }

        #endregion
    }
}
