using RedLine.Crutch;
using RedLine.Scanners;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Forms;

namespace RedLine
{
    public partial class ScannerTaskPanel : UserControl, IPartImportsSatisfiedNotification
    {
        private ScanResults _results;
        private IScanner[] _scanners;

        [Import]
        public IScannerService ScannerService { private get; set; }

        [Import]
        public ICrutchWordService CrutchService { private get; set; }
        
        public ScannerTaskPanel()
        {
            InitializeComponent();
        }

        public void OnImportsSatisfied()
        {
            LoadCrutchWords();

            _scanners = ScannerService.Scanners.ToArray();

            for (int idx = 0; idx < _scanners.Length; idx++)
            {
                clbScanners.Items.Add(_scanners[idx].Name);
                clbScanners.SetItemChecked(idx, _scanners[idx].Enabled);
                clbScanners.ItemCheck += clbScanners_ItemCheck;
            }
        }

        private void LoadCrutchWords()
        {
            var crutches = CrutchService.CrutchWords.ToArray();

            lbCrutches.DataSource = crutches;
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
