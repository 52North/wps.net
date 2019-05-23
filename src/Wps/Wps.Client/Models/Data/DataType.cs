using System;
using System.Xml.Schema;
using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Data
{
    [Serializable]
    [XmlRoot(ElementName = "DataType", Namespace = ModelNamespaces.Ows)]
    public class DataType
    {

        [XmlAttribute(AttributeName = "reference", Form = XmlSchemaForm.Qualified, Namespace = ModelNamespaces.Ows)]
        public string Reference { get; set; }

        public Type GetReferenceType()
        {
            if (Reference.Equals("string", StringComparison.InvariantCultureIgnoreCase))
            {
                return typeof(string);
            }

            if (Reference.Equals("integer", StringComparison.InvariantCultureIgnoreCase))
            {
                return typeof(int);
            }

            if (Reference.Equals("decimal", StringComparison.InvariantCultureIgnoreCase))
            {
                return typeof(decimal);
            }

            if (Reference.Equals("boolean", StringComparison.InvariantCultureIgnoreCase))
            {
                return typeof(bool);
            }

            if (Reference.Equals("double", StringComparison.InvariantCultureIgnoreCase))
            {
                return typeof(double);
            }

            if (Reference.Equals("float", StringComparison.InvariantCultureIgnoreCase))
            {
                return typeof(float);
            }

            return null;
        }
    }
}
