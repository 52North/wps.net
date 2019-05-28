using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models
{
    [XmlRoot("ProviderSite", Namespace = ModelNamespaces.Ows)]
    public class ProviderSite
    {

        /// <summary>
        /// href of the provider website.
        /// </summary>
        [XmlAttribute("href", Namespace = ModelNamespaces.Xlink)]
        public string HyperlinkReference { get; set; }

    }
}
