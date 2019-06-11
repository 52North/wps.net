namespace Wps.Client.Services
{
    public interface IXmlSerializer
    {

        string Serialize(object obj, bool omitHeaderDeclaration = false);
        T Deserialize<T>(string xml);

    }
}
