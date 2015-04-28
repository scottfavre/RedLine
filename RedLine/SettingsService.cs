using System.ComponentModel.Composition;

namespace RedLine
{
    public interface ISettingsService
    {
        string LastCrutchListKey { get; set; }
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
    }
}
