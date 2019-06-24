using System;
using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models
{
    /// <summary>
    /// Specifies the requirements for a process offering.
    /// </summary>
    [Serializable]
    [XmlRoot("Process", Namespace = ModelNamespaces.Wps)]
    public class Process : IdentifiableObject
    {

        /// <summary>
        /// The inputs of the process
        /// </summary>
        [XmlElement("Input", Namespace = ModelNamespaces.Wps)]
        public Input[] Inputs { get; set; }

        /// <summary>
        /// The outputs of the process
        /// </summary>
        [XmlElement("Output", Namespace = ModelNamespaces.Wps)]
        public Output[] Outputs { get; set; }

    }
}
