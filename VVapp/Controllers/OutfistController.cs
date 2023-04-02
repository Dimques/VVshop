using Microsoft.AspNetCore.Mvc;
using VVapp.Models;

namespace VVapp.Controllers;

[Route("outfits")]
public class OutfitsController : BaseController
{
    private readonly IHttpContextAccessor contextAccessor;
    private readonly string outfitsPath;
    
    public OutfitsController(IHttpContextAccessor contextAccessor, IWebHostEnvironment env)
        : base(env)
    {
        this.contextAccessor = contextAccessor;
        outfitsPath = GetRandomResourcePath();
    }
    
    [HttpGet]
    [Route("random")]
    public IActionResult GetRandomOutfit()
    {
        contextAccessor.HttpContext.Response.Headers.CacheControl = CacheControl.NoCache;
        return PhysicalFile(outfitsPath, ContentType.ImageJpeg);
    }

    [HttpGet]
    [Route("several/{count:int?}")]
    public async Task<IActionResult> GetSeveralOutfits(int? count)
    {
        // TODO: при добавлении провайдера бд подумать о пейджинге
        var multipartContent = new MultipartContent(ContentType.ImageJpeg);

        for (var i = 0; i < (count ?? 6); i++)
        {
            var path = GetRandomResourcePath();
            var content = GetStreamContentFromFile(path, ContentType.ImageJpeg);
            multipartContent.Add(content);
        }

        return new ContentResult()
        {
            Content = await multipartContent.ReadAsStringAsync(),
            ContentType = ContentType.ImageJpeg,
            StatusCode = 200
        };
    }
}