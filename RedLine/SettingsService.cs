using System.ComponentModel.Composition;

namespace RedLine
{
    public interface ISettingsService
    {
        string LastCrutchListKey { get; set; }
        bool PanelVisible { get; set; }

        bool AdverbScannerEnabled { get; set; }
        bool CrutchScannerEnabled { get; set; }
    }

    [Export(typeof(ISettingsService))]
    public class SettingsService: ISettingsService
    {
        public string LastCrutchListKey
        {
            get
            {
                return Properties.Settings.Default.LastCrutchList;
            }
            set
            {
                Properties.Settings.Default.LastCrutchList = value;
                Properties.Settings.Default.Save();
            }
        }

        public bool PanelVisible
        {
            get { return Properties.Settings.Default.PanelVisible; }
            set
            {
                Properties.Settings.Default.PanelVisible = value;
                Properties.Settings.Default.Save();
            }
        }


        public bool AdverbScannerEnabled
        {
            get
            {
                return Properties.Settings.Default.AdverbScannerEnabled;
            }
            set
            {
                Properties.Settings.Default.AdverbScannerEnabled = value;
                Properties.Settings.Default.Save();
            }
        }

        public bool CrutchScannerEnabled
        {
            get
            {
                return Properties.Settings.Default.CrutchScannerEnabled;
            }
            set
            {
                Properties.Settings.Default.CrutchScannerEnabled = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}
