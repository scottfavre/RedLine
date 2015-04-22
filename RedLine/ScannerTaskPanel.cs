using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RedLine.Scanners;

namespace RedLine
{
    public partial class ScannerTaskPanel : UserControl
    {
        private IScannerService _scannerService;
        private ICrutchWordService _crutchService;

        private ScanResults _results;
        private IScanner[] _scanners;
        
        public ScannerTaskPanel(IScannerService scannerService, ICrutchWordService crutchService)
        {
            InitializeComponent();

            _scannerService = scannerService;
            _crutchService = crutchService;

            lbCrutches.DataSource = crutchService.Crutches;

            _scanners = scannerService.Scanners.ToArray();

            for (int idx = 0; idx < _scanners.Length; idx++)
            {
                clbScanners.Items.Add(_scanners[idx].Name);
                clbScanners.SetItemChecked(idx, _scanners[idx].Enabled);
                clbScanners.ItemCheck += clbScanners_ItemCheck;
            }
        }

        void clbScanners_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            _scanners[e.Index].Enabled = (e.NewValue == CheckState.Checked);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            btnPrev.Enabled = _results.MovePrev();
            btnNext.Enabled = _results.CanMoveNext;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            btnNext.Enabled = _results.MoveNext();
            btnPrev.Enabled = _results.CanMovePrev;
        }

        public void SetResults(ScanResults results)
        {
            _results = results;

            btnNext.Enabled = results.CanMoveNext;
            btnPrev.Enabled = results.CanMovePrev;
        }
    }
}
