using System;
using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Requests
{
    [Serializable]
    [XmlRoot("GetCapabilities", Namespace = ModelNamespaces.Wps)]
    public class GetCapabilitiesRequest : RequestBase
    {

    }
}