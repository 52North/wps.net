using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Execution
{
    [XmlRoot("BoundingBox", Namespace = ModelNamespaces.Ows)]
    public class BoundingBoxValue : IXmlSerializable
    {

        public double[] LowerCornerPoints { get; set; }
        public double[] UpperCornerPoints { get; set; }
        public string CrsUri { get; set; }
        
        /// <summary>
        /// Number of dimensions in this bounding box.
        /// </summary>
        public int DimensionCount { get; set; }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            /*
             * TODO: Add manual deserialization for this object. Careful, the XmlReader has a confusing cursor system that once shifted (even by 1!) will break the serialization for the rest of the objects and will give you a completely wrong XML for the containing paren too!
             */
            throw new System.NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            if (CrsUri != null)
            {
                writer.WriteAttributeString("crs", ModelNamespaces.Ows, CrsUri);
            }

            if (DimensionCount >= 0)
            {
                writer.WriteAttributeString("dimensions", ModelNamespaces.Ows, DimensionCount.ToString());
            }
            else
            {
                throw new InvalidOperationException("The dimension count inside a bounding box cannot be negative.");
            }

            if (LowerCornerPoints != null)
            {
                var content = string.Join(" ", LowerCornerPoints);
                writer.WriteElementString("ows", "LowerCorner", ModelNamespaces.Ows, content);
            }

            if (UpperCornerPoints != null)
            {
                var content = string.Join(" ", UpperCornerPoints);
                writer.WriteElementString("ows", "UpperCorner", ModelNamespaces.Ows, content);
            }
        }
    }
}
