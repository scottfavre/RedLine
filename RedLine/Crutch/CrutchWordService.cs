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
        IEnumerable<string> CrutchWords { get; }
        IEnumerable<string> SetNames { get; }

        void AddWord(string word);
        void RemoveWord(string word);
        void SetCrutchList(string name);
        void CreateCrutchList(string name);
        void DeleteCrutchList(string name);
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
                _currentList.Crutches.Add("saw");
                _currentList.Crutches.Add("began");
                _currentList.Crutches.Add("started");
                _currentList.Crutches.Add("thought");
            }

            _loading = false;
            _loadComplete.Set();
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

        public IEnumerable<string> SetNames
        {
            get
            {
                WaitForLoad();

                return _lists.Keys.OrderBy(name => name);
            }
        }

        public void AddWord(string word)
        {
            WaitForLoad();

            if(_currentList.Crutches.Add(word.ToLower(CultureInfo.CurrentCulture)))
            {
                CrutchData.Update(_currentList);
            }
        }

        public void RemoveWord(string word)
        {
            WaitForLoad();

            if (_currentList.Crutches.Remove(word.ToLower(CultureInfo.CurrentCulture)))
            {
                CrutchData.Update(_currentList);
            }
        }

        public void SetCrutchList(string name)
        {
            WaitForLoad();

            CrutchWordList list;

            if (_lists.TryGetValue(name.ToLower(CultureInfo.CurrentCulture), out list))
            {
                _currentList = list;
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
            _currentList.Crutches.Add("saw");
            _currentList.Crutches.Add("began");
            _currentList.Crutches.Add("started");
            _currentList.Crutches.Add("thought");

            _lists[key] = _currentList;

            CrutchData.Create(_currentList);            
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
        }

        private void WaitForLoad()
        {
            if(_loading)
            {
                _loadComplete.WaitOne();
            }
        }
    }
}
