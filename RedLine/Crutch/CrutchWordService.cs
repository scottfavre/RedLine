using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedLine.Crutch
{
    public interface ICrutchWordService
    {
        string CurrentList { get; }
        IEnumerable<string> CrutchWords { get; }
        IEnumerable<string> ListNames { get; }

        void AddWord(string word);
        void RemoveWord(string word);
        void SetCrutchList(string name);
        void CreateCrutchList(string name);
        void DeleteCrutchList(string name);

        event EventHandler<EventArgs<CrutchWordList>> CurrentListUpdated;
    }

    [Export(typeof(ICrutchWordService))]
    public class CrutchWordService: ICrutchWordService, IPartImportsSatisfiedNotification
    {
        private CrutchWordList _currentList;
        private Dictionary<string, CrutchWordList> _lists;
        private AutoResetEvent _loadComplete;
        private bool _loading;

        [Import]
        public ICrutchWordDataStore CrutchData { private get; set; }

        public CrutchWordService()
        {
            _lists = new Dictionary<string, CrutchWordList>();
            _loadComplete = new AutoResetEvent(false);
            _currentList = null;
        }

        public void OnImportsSatisfied()
        {
            _loading = true;
            var task = new Task(LoadLists);
            task.Start();
        }

        private void LoadLists()
        {
            foreach(var list in CrutchData.Load())
            {
                _lists[list.Name.ToLower(CultureInfo.CurrentCulture)] = list;
            }

            if (_lists.Any())
                _currentList = _lists.First().Value;
            else
            {
                _currentList = new CrutchWordList("Default");

                InitNewList(_currentList);
            }

            _loading = false;
            _loadComplete.Set();
        }
        
        public string CurrentList
        {
            get
            {
                WaitForLoad();

                return _currentList.Name;
            }
        }

        public IEnumerable<string> CrutchWords
        {
            get 
            {
                WaitForLoad();

                if (_currentList == null)
                    return Enumerable.Empty<string>();
                else
                    return _currentList.Crutches.OrderBy(crutch => crutch);
            }
        }

        public IEnumerable<string> ListNames
        {
            get
            {
                WaitForLoad();

                return _lists.Values
                    .Select(list => list.Name)
                    .OrderBy(name => name);
            }
        }

        public void AddWord(string word)
        {
            WaitForLoad();

            if(_currentList.Crutches.Add(word.ToLower(CultureInfo.CurrentCulture)))
            {
                CrutchData.Update(_currentList);

                RaiseCurrentListUpdated();
            }
        }

        public void RemoveWord(string word)
        {
            WaitForLoad();

            if (_currentList.Crutches.Remove(word.ToLower(CultureInfo.CurrentCulture)))
            {
                CrutchData.Update(_currentList);

                RaiseCurrentListUpdated();
            }
        }

        public void SetCrutchList(string name)
        {
            WaitForLoad();

            CrutchWordList list;

            if (_lists.TryGetValue(name.ToLower(CultureInfo.CurrentCulture), out list))
            {
                _currentList = list;

                RaiseCurrentListUpdated();
            }
        }

        public void CreateCrutchList(string name)
        {
            WaitForLoad();

            CrutchWordList list;
            var key = name.ToLower(CultureInfo.CurrentCulture);

            if(_lists.TryGetValue(key, out list))
            {
                _currentList = list;
                return;
            }

            _currentList = new CrutchWordList(name); 
            InitNewList(_currentList);

            _lists[key] = _currentList;

            CrutchData.Create(_currentList);

            RaiseCurrentListUpdated();
        }

        private void InitNewList(CrutchWordList list)
        {
            list.Crutches.Add("saw");
            list.Crutches.Add("began");
            list.Crutches.Add("started");
            list.Crutches.Add("thought");
            _lists[list.Key] = list;
        }

        public void DeleteCrutchList(string name)
        {
            WaitForLoad();

            var key = name.ToLower(CultureInfo.CurrentCulture);

            CrutchWordList list;

            if (!_lists.TryGetValue(key, out list)) return;

            _lists.Remove(key);

            CrutchData.Delete(list);

            if(_lists.Any())
            {
                _currentList = _lists.First().Value;
            }
            else
            {
                CreateCrutchList("Default");
            }

            RaiseCurrentListUpdated();
        }

        private void WaitForLoad()
        {
            if(_loading)
            {
                _loadComplete.WaitOne();
            }
        }

        public event EventHandler<EventArgs<CrutchWordList>> CurrentListUpdated;
        private void RaiseCurrentListUpdated()
        {
            var handler = CurrentListUpdated;
            if (handler != null)
                handler(this, new EventArgs<CrutchWordList>(_currentList));
        }
    }
}
