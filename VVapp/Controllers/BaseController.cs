using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace VVapp.Controllers;

public class BaseController : Controller
{
    private readonly string rootPath;
    
    public BaseController(IWebHostEnvironment environment)
    {
        rootPath = environment.ContentRootPath;
    }

    // TODO: possible memory leak, better to remove this shit
    protected StreamContent GetStreamContentFromFile(string path, string mime)
    {
        var fileName = path.Split("/").LastOrDefault(string.Empty);
        // TODO: using???
        var stream = System.IO.File.OpenRead(path);
        var streamContent = new StreamContent(stream);
        // TODO: add exception handler - in case of ANE return empty streamContent
        streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse(mime);
        streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        {
            FileName = fileName
        };

        return streamContent;
    }
    
    protected string GetRandomResourcePath()
    {
        var catId = GetRandomResourceId();
        var path = rootPath + @$"\TestResources\cat{catId}.jpg";
        Console.WriteLine($"Generated path: '{path}'");
        return path;
    }

    private int GetRandomResourceId()
    {
        var seed = DateTimeOffset.Now.Second;
        var rnd = new Random(seed);
        var result = rnd.Next(1, 6);
        Console.WriteLine($"Generated outfit number: '{result}'");
        return result;
    }
}