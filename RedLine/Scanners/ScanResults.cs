using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedLine.Scanners
{
    public class ScanResults
    {
        private List<Range> _results;
        private int _currentIdx;

        public ScanResults()
        {
            _results = new List<Range>();
            _currentIdx = -1;
        }

        public void Add(Range results)
        {
            _results.Add(results);
        }

        public void Complete()
        {
            if (_results.Any())
            {
                _results.Sort(new RangeComparer());
                MoveNext();
            }
        }

        public bool CanMoveNext
        {
            get { return _results.Count > 0 && _currentIdx < _results.Count - 1; }
        }

        public bool CanMovePrev
        {
            get { return _results.Count > 0 && _currentIdx > 0; }
        }

        public bool MoveNext()
        {
            if(CanMoveNext)
            {
                _currentIdx++;
                _results[_currentIdx].Select();
            }

            return CanMoveNext;
        }

        public bool MovePrev()
        {
            if(CanMovePrev)
            {
                _currentIdx--;
                _results[_currentIdx].Select();
            }

            return CanMovePrev;
        }

        private class RangeComparer: IComparer<Range>
        {
            public int Compare(Range x, Range y)
            {
                return x.Start - y.Start;
            }
        }
    }
}
