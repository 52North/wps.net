namespace Wps.Client.Services
{
    public interface IXmlSerializer
    {

        string Serialize<T>(T obj);
        T Deserialize<T>(string xml);

    }
}
