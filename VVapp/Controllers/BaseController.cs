using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using VVapp.Loggers;

namespace VVapp.Controllers;

public class BaseController : Controller
{
    protected readonly string RootPath;
    private readonly ILog log;
    
    public BaseController(IWebHostEnvironment environment, ILog log)
    {
        RootPath = environment.ContentRootPath;
        this.log = log.ForContext("BaseController");
    }
    
    protected StreamContent GetStreamContentFromFile(string path, string mime)
    {
        var fileName = path.Split("/").LastOrDefault(string.Empty);
        // TODO: using???
        using var stream = System.IO.File.OpenRead(path);
        using var streamContent = new StreamContent(stream);
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
        var path = RootPath + @$"\TestResources\cat{catId}.jpg";
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