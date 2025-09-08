namespace Qrist.Interfaces
{
    public interface IQristUrlBuilder
    {
        string BuildFullUrl(string provider, string encodedRequestData);
    }
}