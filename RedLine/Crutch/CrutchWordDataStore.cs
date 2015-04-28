using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RedLine.Crutch
{
    public interface ICrutchWordDataStore
    {
        IEnumerable<CrutchWordList> Load();

        void Update(CrutchWordList list);
        void Create(CrutchWordList list);
        void Delete(CrutchWordList list);
    }

    [Export(typeof(ICrutchWordDataStore))]
    public class CrutchWordDataStore: ICrutchWordDataStore
    {
        private CrutchFileSerializer _serializer;

        public CrutchWordDataStore()
        {
            _serializer = new CrutchFileSerializer();
        }

        [Import]
        public IFileSystemService FileSystem { private get; set; }

        public IEnumerable<CrutchWordList> Load()
        {
            var lists = new List<CrutchWordList>();

            foreach (var file in FileSystem.ListFiles("*.crutch"))
            {
                try
                {
                    var doc = XDocument.Load(file);

                    var list = _serializer.Deserialize(doc);

                    lists.Add(list);
                }
                catch(Exception)
                {
                    // Empty, should probably log it somewhere
                }
            }

            return lists;
        }

        public void Update(CrutchWordList list)
        {
            var doc = _serializer.Serialize(list);

            var settings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t"
            };
            using (var stream = FileSystem.CreateFile(list.Name + ".crutch"))
            using (var writer = XmlWriter.Create(stream, settings))
            {
                doc.WriteTo(writer);
            }
        }

        public void Create(CrutchWordList list)
        {
            Update(list);
        }

        public void Delete(CrutchWordList list)
        {
            FileSystem.DeleteFile(list.Name + ".crutch");
        }
    }
}
