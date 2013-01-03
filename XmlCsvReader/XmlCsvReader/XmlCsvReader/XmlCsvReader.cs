using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace XmlCsvReaderNS
{
    public class XmlCsvReader : XmlReader
    {
        protected string data { get; set; }
        protected int index { get; set; }

        public XmlCsvReader() { }
        public XmlCsvReader( Uri location,
                             XmlNameTable nametable)
        {
            if (location.IsFile && File.Exists(location.AbsolutePath))
            {
                data = File.ReadAllText(location.AbsolutePath);
                index = 0;
            }
            else
                throw new FileNotFoundException("XmlCsvReader could not find file", location.AbsolutePath);

        }
        public XmlCsvReader( Stream input,
                             Uri baseUri,
                             XmlNameTable nametable)
        {
            throw new NotImplementedException();
        }
        public XmlCsvReader(TextReader input,
                             Uri baseUri,
                             XmlNameTable nametable)
        {
            throw new NotImplementedException();
        }

        public override int AttributeCount
        {
            get { return 0; }
        }

        public override string BaseURI
        {
            get { return string.Empty; }
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override int Depth
        {
            get { return 1; }
        }

        public override bool EOF
        {
            get { throw new NotImplementedException(); }
        }

        public override string GetAttribute(int i)
        {
            throw new NotImplementedException();
        }

        public override string GetAttribute(string name, string namespaceURI)
        {
            throw new NotImplementedException();
        }

        public override string GetAttribute(string name)
        {
            throw new NotImplementedException();
        }

        public override bool IsEmptyElement
        {
            get { throw new NotImplementedException(); }
        }

        public override string LocalName
        {
            get { throw new NotImplementedException(); }
        }

        public override string LookupNamespace(string prefix)
        {
            throw new NotImplementedException();
        }

        public override bool MoveToAttribute(string name, string ns)
        {
            throw new NotImplementedException();
        }

        public override bool MoveToAttribute(string name)
        {
            throw new NotImplementedException();
        }

        public override bool MoveToElement()
        {
            index = data.IndexOf(',', index);
            index++;
            if (index < data.Length)
                return true;
            return false;
        }

        public override bool MoveToFirstAttribute()
        {
            throw new NotImplementedException();
        }

        public override bool MoveToNextAttribute()
        {
            throw new NotImplementedException();
        }

        public override XmlNameTable NameTable
        {
            get { throw new NotImplementedException(); }
        }

        public override string NamespaceURI
        {
            get { throw new NotImplementedException(); }
        }

        public override XmlNodeType NodeType
        {
            get { throw new NotImplementedException(); }
        }

        public override string Prefix
        {
            get { throw new NotImplementedException(); }
        }

        public override bool Read()
        {
            throw new NotImplementedException();
        }

        public override bool ReadAttributeValue()
        {
            throw new NotImplementedException();
        }

        public override ReadState ReadState
        {
            get { throw new NotImplementedException(); }
        }

        public override void ResolveEntity()
        {
            throw new NotImplementedException();
        }

        public override string Value
        {
            get { throw new NotImplementedException(); }
        }
    }
}
