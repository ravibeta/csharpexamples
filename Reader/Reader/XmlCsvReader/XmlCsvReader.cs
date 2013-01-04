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
                value = clean(RootName);
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
                value = clean(RowName);
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
                    value = clean(RowName);
                    rowCreated = false;
                    tag = 2;
                    return true;
                }
            }

            if (index < lines.Count())
            {
                var line = lines[index];
                var values = line.Split(new char[] { ',' });
                if (!colCreated) { value = clean(columnname); tag = 0; colCreated = true;  return true; }
                if (tag == 1 && colCreated) { value = clean(columnname); tag = 2; column++; colCreated = false; return true; }
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

        private string clean(string namevalue, bool skipComma = false)
        {
            string str = namevalue;
            if (!skipComma)
            {
                str = namevalue.Replace(" ", "");
                str = str.Replace(",", "");
            }
            str = str.Trim(new char[] { ',', '*' });
            str = str.Replace("/", "-");
            str = str.Replace("*", "-");
            return str;
        }
    }
}
