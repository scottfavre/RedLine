using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Schema;

namespace RedLine.Crutch
{
    public class CrutchFileSerializer
    {
        public static readonly XNamespace ns = XNamespace.Get("http://redline.com/CrutchList_1.0.xsd");
        public static readonly XName xeCrutchList = ns + "CrutchList";
        public static readonly XName xeWord = ns + "Word";

        public static readonly XName xaName = "Name";

        public CrutchFileSerializer()
        {
        }

        public XDocument Serialize(CrutchWordList list)
        {
            var doc = new XDocument(new XElement(xeCrutchList,
                new XAttribute(xaName, list.Name),
                list.Crutches.Select(word => new XElement(xeWord, word.ToLower(CultureInfo.CurrentCulture)))));

            Validate(doc);

            return doc;
        }

        public CrutchWordList Deserialize(XDocument document)
        {
            Validate(document);

            var list = new CrutchWordList(document.Root.Attribute(xaName).Value);

            foreach(var wordElem in document.Root.Elements(xeWord))
            {
                list.Crutches.Add(wordElem.Value);
            }

            return list;
        }

        private static void Validate(XDocument document)
        {
            var schemaSet = new XmlSchemaSet();

            using (var stream = typeof(CrutchFileSerializer).Assembly.GetManifestResourceStream("RedLine.Crutch.CrutchList_1_0.xsd"))
            {
                var schema = XmlSchema.Read(stream, (s, a) => { });
                schemaSet.Add(schema);
            }

            document.Validate(schemaSet, (s, a) => { throw new FormatException(a.Message); });
        }
    }
}
