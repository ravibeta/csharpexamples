using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Schema;

namespace XmlCsvReaderNS
{   
    public class XmlCsvReader : XmlReader
    {
        protected string[] lines { get; set; }
        protected int index { get; set; }
        protected int column { get; set; }
        public bool FirstRowHasColumnNames { get; set; }
        public string value { get; set; }
        public string RootName { get; set; }
        public string RowName { get; set; }
        protected int depth { get; set; }
        protected int tag { get; set; }
        protected bool rowCreated { get; set; }
        protected bool colCreated { get; set; }

        ReadState rs { get; set; }

        public XmlCsvReader() { }
        public XmlCsvReader(Uri location,
                     XmlNameTable nametable):base()
        {
            if (File.Exists(location.AbsolutePath))
            {
                lines = File.ReadAllLines(location.AbsolutePath);
                index = 0;
                column = 0;
                depth = 0;
                tag = 0;
                rs = ReadState.Initial;
            }
        }

        public override int AttributeCount
        {
            get { throw new NotImplementedException(); }
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
            get { throw new NotImplementedException(); }
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
            get { // if (tag == 0)
                    return false;
                // else
                // return true;
            }
        }

        public override string LocalName
        {
            get {
                    return value;
                //var columnnames = lines[0].Split(new char[] { ',' });
                //var columnname = (FirstRowHasColumnNames && column < columnnames.Count()) ? columnnames[column] : ("Element" + column.ToString());
                //return columnname;
            }
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
            throw new NotImplementedException();
        }

        public override bool MoveToFirstAttribute()
        {
            return false;
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
            get { return string.Empty; }
        }

        public override XmlNodeType NodeType
        {
            get {
                // return XmlNodeType.Element;
                switch (tag)
                {
                    case 0: { return XmlNodeType.Element; }
                    case 1: { return XmlNodeType.Text; }
                    case 2: { return XmlNodeType.EndElement; }
                    default: return XmlNodeType.Element;
                } 
            }
        }

        public override string Prefix
        {
            get { return string.Empty; }
        }

        public override bool Read()
        {
            if (rs == ReadState.Initial)
            {
                rs = ReadState.Interactive;
                value = RootName;
                tag = 0;
                return true;
            }

            var columnnames = lines[0].Split(new char[] { ',' });
            int maxColumns = lines[0] != null ? columnnames.Count() : 0;
            if (FirstRowHasColumnNames)
            {
                if (index == 0)
                    index++;
            }

            var columnname = (FirstRowHasColumnNames && column < columnnames.Count()) ? columnnames[column] : ("Element" + column.ToString());
            columnname = columnname.Trim(new char[] { '"' });

            if (index < lines.Count() && column == 0 && !rowCreated)
            {
                value = RowName;
                rowCreated = true;
                tag = 0;
                return true;
            }
            
            if (index < lines.Count() && column >= maxColumns)
            {
                column = 0;
                index++;

                if (rowCreated)
                {
                    value = RowName;
                    rowCreated = false;
                    tag = 2;
                    return true;
                }
            }

            if (index < lines.Count())
            {
                var line = lines[index];
                var values = line.Split(new char[] { ',' });
                if (!colCreated) { value = columnname; tag = 0; colCreated = true;  return true; }
                if (tag == 1 && colCreated) { value = columnname; tag = 2; column++; colCreated = false; return true; }
                tag = 1;
                value = values[column];
                value = value.Trim(new char[] { '"' });
                return true;
            }
            return false; 
        }

        public override bool ReadAttributeValue()
        {
            throw new NotImplementedException();
        }

        public override ReadState ReadState
        {
            get
            {
                if (index > 0)
                    rs = ReadState.Interactive;
                else
                    rs = ReadState.Initial;
                return rs;
            }
        }

        public override void ResolveEntity()
        {
            throw new NotImplementedException();
        }

        public override string Value
        {
            get { return value; }
        }

        public override bool CanReadBinaryContent { get{ throw new NotImplementedException();} }
        public override bool CanReadValueChunk { get{ throw new NotImplementedException();} }
        public override bool CanResolveEntity { get{ throw new NotImplementedException();} }
        public override bool HasAttributes { get{ throw new NotImplementedException();} }
        public override bool HasValue { get{ throw new NotImplementedException();} }
        public override bool IsDefault { get{ throw new NotImplementedException();} }
        public override string Name { get{ throw new NotImplementedException();} }
        public override char QuoteChar { get{ throw new NotImplementedException();} }
        public override IXmlSchemaInfo SchemaInfo { get{ return null;} }
        // public override XmlReaderSettings Settings { get{ throw new NotImplementedException();} }
        public override Type ValueType { get{ throw new NotImplementedException();} }
        public override string XmlLang { get{ throw new NotImplementedException();} }
        public override XmlSpace XmlSpace { get{ throw new NotImplementedException();} }
        public override string this[int i] { get{ throw new NotImplementedException();} }
        public override string this[string name] { get{ throw new NotImplementedException();} }
        public override string this[string name, string namespaceURI] { get{ throw new NotImplementedException();} }
        protected override void Dispose(bool disposing){ throw new NotImplementedException();}
        public override bool IsStartElement(){ throw new NotImplementedException();}
        public override bool IsStartElement(string name){ throw new NotImplementedException();}
        public override bool IsStartElement(string localname, string ns){ throw new NotImplementedException();}
        public override void MoveToAttribute(int i){ throw new NotImplementedException();}
        public override XmlNodeType MoveToContent(){ throw new NotImplementedException();}
        public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver){ throw new NotImplementedException();}
        public override int ReadContentAsBase64(byte[] buffer, int index, int count){ throw new NotImplementedException();}
        public override int ReadContentAsBinHex(byte[] buffer, int index, int count){ throw new NotImplementedException();}
        public override bool ReadContentAsBoolean(){ throw new NotImplementedException();}
        public override DateTime ReadContentAsDateTime(){ throw new NotImplementedException();}
        public override decimal ReadContentAsDecimal(){ throw new NotImplementedException();}
        public override double ReadContentAsDouble(){ throw new NotImplementedException();}
        public override float ReadContentAsFloat(){ throw new NotImplementedException();}
        public override int ReadContentAsInt(){ throw new NotImplementedException();}
        public override long ReadContentAsLong(){ throw new NotImplementedException();}
        public override object ReadContentAsObject(){ throw new NotImplementedException();}
        public override string ReadContentAsString(){ throw new NotImplementedException();}
        public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver){ throw new NotImplementedException();}
        public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver, string localName, string namespaceURI){ throw new NotImplementedException();}
        public override int ReadElementContentAsBase64(byte[] buffer, int index, int count){ throw new NotImplementedException();}
        public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count){ throw new NotImplementedException();}
        public override bool ReadElementContentAsBoolean(){ throw new NotImplementedException();}
        public override bool ReadElementContentAsBoolean(string localName, string namespaceURI){ throw new NotImplementedException();}
        public override DateTime ReadElementContentAsDateTime(){ throw new NotImplementedException();}
        public override DateTime ReadElementContentAsDateTime(string localName, string namespaceURI){ throw new NotImplementedException();}
        public override decimal ReadElementContentAsDecimal(){ throw new NotImplementedException();}
        public override decimal ReadElementContentAsDecimal(string localName, string namespaceURI){ throw new NotImplementedException();}
        public override double ReadElementContentAsDouble(){ throw new NotImplementedException();}
        public override double ReadElementContentAsDouble(string localName, string namespaceURI){ throw new NotImplementedException();}
        public override float ReadElementContentAsFloat(){ throw new NotImplementedException();}
        public override float ReadElementContentAsFloat(string localName, string namespaceURI){ throw new NotImplementedException();}
        public override int ReadElementContentAsInt(){ throw new NotImplementedException();}
        public override int ReadElementContentAsInt(string localName, string namespaceURI){ throw new NotImplementedException();}
        public override long ReadElementContentAsLong(){ throw new NotImplementedException();}
        public override long ReadElementContentAsLong(string localName, string namespaceURI){ throw new NotImplementedException();}
        public override object ReadElementContentAsObject(){ throw new NotImplementedException();}
        public override object ReadElementContentAsObject(string localName, string namespaceURI){ throw new NotImplementedException();}
        public override string ReadElementContentAsString(){ throw new NotImplementedException();}
        public override string ReadElementContentAsString(string localName, string namespaceURI){ throw new NotImplementedException();}
        public override string ReadElementString(){ throw new NotImplementedException();}
        public override string ReadElementString(string name){ throw new NotImplementedException();}
        public override string ReadElementString(string localname, string ns){ throw new NotImplementedException();}
        public override void ReadEndElement(){ throw new NotImplementedException();}
        public override string ReadInnerXml(){ throw new NotImplementedException();}
        public override string ReadOuterXml(){ throw new NotImplementedException();}
        public override void ReadStartElement(){ throw new NotImplementedException();}
        public override void ReadStartElement(string name){ throw new NotImplementedException();}
        public override void ReadStartElement(string localname, string ns){ throw new NotImplementedException();}
        public override string ReadString(){ throw new NotImplementedException();}
        public override XmlReader ReadSubtree(){ throw new NotImplementedException();}
        public override bool ReadToDescendant(string name){ throw new NotImplementedException();}
        public override bool ReadToDescendant(string localName, string namespaceURI){ throw new NotImplementedException();}
        public override bool ReadToFollowing(string name){ throw new NotImplementedException();}
        public override bool ReadToFollowing(string localName, string namespaceURI){ throw new NotImplementedException();}
        public override bool ReadToNextSibling(string name){ throw new NotImplementedException();}
        public override bool ReadToNextSibling(string localName, string namespaceURI){ throw new NotImplementedException();}
        public override int ReadValueChunk(char[] buffer, int index, int count){ throw new NotImplementedException();}
        public override void Skip(){ throw new NotImplementedException();}

    }
}
