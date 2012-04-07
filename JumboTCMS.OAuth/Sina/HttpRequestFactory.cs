namespace JumboTCMS.OAuth.Sina
{
    public class HttpRequestFactory
    {
        private HttpRequestFactory(){}

        public static BaseHttpRequest CreateHttpRequest(Method method)
        {
            if(method == Method.GET)
            {
                return new HttpGet();
            }
            return new HttpPost();
        }
    }
}