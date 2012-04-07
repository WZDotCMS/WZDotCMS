namespace JumboTCMS.OAuth.Sina
{
    public interface IHttpRequestMethod
    {
        string Request(string uri, string postData);        
    }
}