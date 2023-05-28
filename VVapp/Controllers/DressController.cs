using Microsoft.AspNetCore.Mvc;
using VVapp.Loggers;
using VVapp.Models;

namespace VVapp.Controllers;

[Route("/dress")]
public class DressController : BaseController
{
    private readonly IHttpContextAccessor contextAccessor;

    public DressController(IHttpContextAccessor contextAccessor, IWebHostEnvironment env, ILog log)
        : base(env, log)
    {
        this.contextAccessor = contextAccessor;
    }

    [HttpGet]
    public IActionResult GetDress()
    {
        contextAccessor.HttpContext.Response.Headers.CacheControl = CacheControl.NoCache;
        var outfitPath = GetRandomResourcePath();
        return PhysicalFile(outfitPath, ContentType.ImageJpeg);
    }
}