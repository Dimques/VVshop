using Microsoft.AspNetCore.Mvc;
using VVapp.DbProviders;
using VVapp.Loggers;

namespace VVapp.Controllers;

public class AdminController : BaseController
{
    private readonly IDbProvider dbProvider;
    private readonly ILog log;

    public AdminController(IWebHostEnvironment env, IDbProvider dbProvider, ILog log) : base(env, log)
    {
        this.dbProvider = dbProvider;
        this.log = log.ForContext("AdminController");
    }
    
    [HttpPost]
    [Route("upload-wear")]
    public async Task<IActionResult> UploadWear()
    {
        var form = await Request.ReadFormAsync();
        var wearDescription = form["wearData"];
        var wearImage = form.Files["wearImage"];

        log.Info($"Got json: '{wearDescription}'");
        
        dbProvider.TryAddWear(wearDescription, wearImage!);

        return Ok();
    }
    
    [HttpPost]
    [Route("upload-outfit")]
    public async Task<IActionResult> UploadOutfit()
    {
        var form = await Request.ReadFormAsync();
        var outfitDescription = form["outfitData"];
        var outfitImage = form.Files["outfitImage"];

        log.Info($"Got json: '{outfitDescription}'");
        
        dbProvider.TryAddOutfit(outfitDescription, outfitImage!);

        return Ok();
    }
}