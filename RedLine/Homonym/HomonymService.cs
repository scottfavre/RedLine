using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;

namespace RedLine.Homonym
{
    public interface IHomonymService
    {
        List<string[]> Homonyms { get; }
    }

    [Export(typeof(IHomonymService))]
    public class HomonymService: IHomonymService
    {
        public const string HomonymFileName = "homonyms.txt";
        public static readonly char[] DelimiterChars = new[] { '|' };
        public static readonly string DelimiterStr = new string(DelimiterChars);

        [Import]
        public IFileSystemService FileSystem { private get; set; }

        private List<string[]> _homonyms;
        public List<string[]> Homonyms
        {
            get 
            {
                if (_homonyms == null)
                    Load();

                return _homonyms;
            }
        }

        private void Load()
        {
            if (!FileSystem.FileExists(HomonymFileName))
            {
                _homonyms = new List<string[]>
                {
                    new[] { "it's", "its" },
                    new[] { "there", "they're", "their" }
                };

                Save();
            }
            else
            {
                using(var stream = FileSystem.ReadFile(HomonymFileName))
                using(var reader = new StreamReader(stream))
                {
                    _homonyms = new List<string[]>();

                    while(!reader.EndOfStream)
                    {
                        var entries = reader.ReadLine().Split(DelimiterChars, StringSplitOptions.RemoveEmptyEntries);

                        if (entries.Length == 0) continue;

                        _homonyms.Add(entries);
                    }
                }
            }
        }

        private void Save()
        {
            using(var stream = FileSystem.CreateFile(HomonymFileName))
            using(var writer = new StreamWriter(stream))
            {
                foreach(var hom in _homonyms)
                {
                    writer.WriteLine(string.Join(DelimiterStr, hom));
                }
            }
        }
    }
}
