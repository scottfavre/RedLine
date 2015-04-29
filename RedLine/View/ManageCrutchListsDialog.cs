using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RedLine.View
{
    public partial class ManageCrutchListsDialog : Form
    {
        private List<string> _names;
        private HashSet<string> _deletedLists;

        public ManageCrutchListsDialog()
        {
            InitializeComponent();
            _deletedLists = new HashSet<string>();
        }

        public void SetLists(string current, IEnumerable<string> listNames)
        {
            _names = listNames.OrderBy(name => name).ToList();
            lbCrutchLists.Items.Clear();
            lbCrutchLists.Items.AddRange(_names.ToArray());

            if(lbCrutchLists.Items.Count > 0 && current != null)
            {
                btnSelect.Enabled = true;
                lbCrutchLists.SelectedItem = current;
                lbCrutchLists.Focus();
            }
            else
            {
                btnSelect.Enabled = false;
                tbNewTextBox.Focus();
            }
        }

        public string SelectedList { get; private set; }
        public bool CreateNew { get; private set; }
        public IEnumerable<string> DeletedLists { get { return _deletedLists; } }

        private void tbNewTextBox_TextChanged(object sender, EventArgs e)
        {
            var name = GetNewListName();

            if(name == string.Empty)
            {
                btnSelect.Text = "Select";
                btnSelect.Enabled = (lbCrutchLists.SelectedItem != null);
                return;
            }

            btnSelect.Text = "Create";

            if(_names.Any(n => n.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
            {
                btnSelect.Enabled = false;
            }
            else
            {
                btnSelect.Enabled = true;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            var name = GetNewListName();

            if (name == string.Empty)
            {
                SelectedList = lbCrutchLists.SelectedItem as string;
                CreateNew = false;
            }
            else
            {
                SelectedList = name;
                CreateNew = true;
            }

            DialogResult = DialogResult.OK;

            Close();
        }

        private string GetNewListName()
        {
            var name = (tbNewTextBox.Text ?? string.Empty).Trim();
            return name;
        }

        private void lbCrutchLists_DoubleClick(object sender, MouseEventArgs e)
        {
            if (lbCrutchLists.SelectedIndex != -1)
            {
                var rect = lbCrutchLists.GetItemRectangle(lbCrutchLists.SelectedIndex);
                if (rect.Contains(e.Location))
                {
                    btnSelect.PerformClick();
                }
            }
        }
        
        private void lbCrutchLists_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete && lbCrutchLists.SelectedIndex >= 0)
            {
                var selectedName = lbCrutchLists.SelectedItem as string;

                var res = MessageBox.Show(
                    "Delete crutch list \"" + selectedName + "\"?",
                    "Delete Crutch List",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    _deletedLists.Add(selectedName);
                    _names.Remove(selectedName);
                    var idx = lbCrutchLists.SelectedIndex;
                    lbCrutchLists.Items.RemoveAt(lbCrutchLists.SelectedIndex);

                    if (idx > lbCrutchLists.Items.Count - 1)
                    {
                        lbCrutchLists.SelectedIndex = lbCrutchLists.Items.Count - 1;
                    }
                    else
                    {
                        lbCrutchLists.SelectedIndex = idx;
                    }
                }
            }
        }
    }
}
