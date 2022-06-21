namespace TKeazirian.HTTPServer.Handler;

using Response;
using Request;

public class RedirectHandler : IHandler
{
    public List<string> AllowedHttpMethods()
    {
        return new List<string>() { "GET" };
    }

    public Response HandleResponse(Request requestObject)
    {
        ResponseBuilder responseBuilder = new ResponseBuilder();

        var httpVersion = Constants.HttpVersion;
        var responseStatusCode = HandleStatusCode();
        var responseStatusText = HandleResponseText();
        var responseStatusLine = HandleStatusLine(httpVersion, responseStatusCode, responseStatusText);
        var responseHeaders = HandleHeaders();
        var responseBody = HandleBody();

        return responseBuilder.BuildNewResponse(responseStatusLine, responseHeaders, responseBody);
    }

    private int HandleStatusCode()
    {
        return (int)HttpStatusCode.Moved;
    }

    private string HandleResponseText()
    {
        return Constants.Moved;
    }

    public string HandleStatusLine(string httpVersion, int responseStatusCode, string responseStatusText)
    {
        return httpVersion + Constants.Space + responseStatusCode + Constants.Space + responseStatusText;
    }

    private string HandleHeaders()
    {
        return Constants.NewLine +
               $"Location: http://{Server.Server.LocalIpAddress}:{Server.Server.Port}/simple_get" +
               Constants.NewLine + Constants.NewLine;
    }

    private string HandleBody()
    {
        return "";
    }
}
