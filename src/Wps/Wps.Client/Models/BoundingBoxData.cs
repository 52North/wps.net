using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models
{
    [XmlRoot("BoundingBoxData", Namespace = ModelNamespaces.Wps)]
    public class BoundingBoxData : Data.Data
    {

        /// <summary>
        /// List of the supported Coordinate Reference Systems.
        /// </summary>
        [XmlElement("SupportedCRS", Namespace = ModelNamespaces.Wps)]
        public CoordinateReferenceSystem[] SupportedCrs { get; set; }

    }
}
