using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VVapp.DbProviders;
using VVapp.Loggers;
using VVapp.Models;

namespace VVapp.Controllers;

[Route("outfits")]
public class OutfitsController : BaseController
{
    private readonly IHttpContextAccessor contextAccessor;
    private readonly IDbProvider dbProvider;
    private readonly ILog log;
    
    public OutfitsController(IHttpContextAccessor contextAccessor, 
        IWebHostEnvironment env,
        IDbProvider dbProvider,
        ILog log)
        : base(env, log)
    {
        this.dbProvider = dbProvider;
        this.contextAccessor = contextAccessor;
        this.log = log.ForContext("OutfitsController");
    }
    
    [HttpGet]
    [Route("random")]
    public IActionResult GetRandomOutfit()
    {
        throw new InvalidOperationException("Invalid operation dude");
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
    
    [HttpPost]
    [Route("construct")]
    public async Task<IActionResult> BuildOutfit()
    {
        using var reader = new StreamReader(Request.Body, Encoding.UTF8);
        var json = await reader.ReadToEndAsync();
        log.Info($"Got json: {json}");

        var outfitParameters = JsonConvert.DeserializeObject<OutfitMeta>(json);
        if (!Validator.ValidateOutfitParameters(outfitParameters))
            return new BadRequestResult();

        /*var outfitUrls = dbProvider.GetOutfitsUrls(outfitParameters!);
        return Ok(outfitUrls);*/
        return Ok();
    }
}