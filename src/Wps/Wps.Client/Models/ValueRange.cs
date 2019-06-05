using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models
{
    [XmlRoot("Range", Namespace = ModelNamespaces.Ows)]
    public class ValueRange : IXmlSerializable
    {

        public RangeClosure RangeClosure { get; set; }

        public string MinimumValue { get; set; }

        public string MaximumValue { get; set; }

        public string Spacing { get; set; }

        // -------------------------------------------------------------------

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            RangeClosure = RangeClosureFromString(reader.GetAttribute("rangeClosure", ModelNamespaces.Ows));

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.NamespaceURI.Equals(ModelNamespaces.Ows))
                {
                    if (reader.LocalName.Equals("MinimumValue"))
                    {
                        MinimumValue = reader.ReadString();
                    }
                    else if (reader.LocalName.Equals("MaximumValue"))
                    {
                        MaximumValue = reader.ReadString();
                    }
                    else if (reader.LocalName.Equals("Spacing"))
                    {
                        Spacing = reader.ReadString();
                    }
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("ows", "rangeClosure", ModelNamespaces.Ows, RangeClosureToString(RangeClosure));
            writer.WriteElementString("ows", "MinimumValue", ModelNamespaces.Ows, MinimumValue);
            writer.WriteElementString("ows", "MaximumValue", ModelNamespaces.Ows, MaximumValue);
            writer.WriteElementString("ows", "Spacing", ModelNamespaces.Ows, Spacing);
        }

        private RangeClosure RangeClosureFromString(string str)
        {
            if (str == null)
            {
                return RangeClosure.Closed;
            }

            if (str.Equals("closed", StringComparison.InvariantCultureIgnoreCase))
            {
                return RangeClosure.Closed;
            }

            if (str.Equals("open", StringComparison.InvariantCultureIgnoreCase))
            {
                return RangeClosure.Open;
            }

            if (str.Equals("open-closed", StringComparison.InvariantCultureIgnoreCase))
            {
                return RangeClosure.OpenClosed;
            }

            if (str.Equals("closed-open", StringComparison.InvariantCultureIgnoreCase))
            {
                return RangeClosure.ClosedOpen;
            }

            return RangeClosure.Closed; // Value by default
        }

        private string RangeClosureToString(RangeClosure rc)
        {
            switch (rc)
            {
                case RangeClosure.Closed:
                    return "closed";
                case RangeClosure.Open:
                    return "open";
                case RangeClosure.OpenClosed:
                    return "open-closed";
                case RangeClosure.ClosedOpen:
                    return "closed-open";
                default:
                    return "closed"; // Value by default
            }
        }

    }
}
