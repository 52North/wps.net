using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Wps.Client.Services;
using Wps.Client.Utils;

namespace Wps.Client.Models
{
    [XmlRoot("Output", Namespace = ModelNamespaces.Wps)]
    public class ResultOutput<TData> : IXmlSerializable
    {

        /// <summary>
        /// Unambiguous identifier or name of an output item.
        /// </summary>
        [XmlAttribute("id", Namespace = ModelNamespaces.Wps)]
        public string Id { get; set; }

        /// <summary>
        /// The data provided by this output item.
        /// </summary>
        [XmlElement("Data", Namespace = ModelNamespaces.Wps)]
        public TData Data { get; set; }

        /// <summary>
        /// Nested output, child element.
        /// </summary>
        [XmlElement("Output", Namespace = ModelNamespaces.Wps)]
        public ResultOutput<TData> Output { get; set; }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            var serializer = new XmlSerializationService();
            Id = reader.GetAttribute("id");

            var subtreeReader = reader.ReadSubtree();
            subtreeReader.MoveToContent();
            while(subtreeReader.Read())
            {
                if (subtreeReader.NodeType == XmlNodeType.Element)
                {
                    if (subtreeReader.LocalName.Equals("Data"))
                    {
                        var content = subtreeReader.ReadInnerXml();
                        if (typeof(TData) == typeof(string))
                        {
                            Data = (TData) Convert.ChangeType(content, typeof(string));
                        }
                        else
                        {
                            Data = serializer.Deserialize<TData>(content);
                        }
                    }

                    if (subtreeReader.LocalName.Equals("Output"))
                    {
                        if(subtreeReader.Depth == 1)
                        {
                            var content = subtreeReader.ReadOuterXml();
                            Output = serializer.Deserialize<ResultOutput<TData>>(content);
                        }
                    }
                }
            }

            reader.Skip();
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}
