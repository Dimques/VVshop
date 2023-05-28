/*using VVapp.Models;

namespace VVapp.DbProviders;

public class InMemoryDbProvider : IDbProvider
{
    private readonly IWebHostEnvironment _environment;
    
    public InMemoryDbProvider(IWebHostEnvironment environment)
    {
        _environment = environment;
    }
    
    public IEnumerable<string> GetOutfitsUrls(OutfitMeta outfitMeta)
    {
        var rootPath = _environment.ContentRootPath;
        var urls = new string[6];
        for (var i = 0; i < 6; i++)
        {
            urls[i] = rootPath + @$"\TestResources\cat{i}.jpg";
        }

        return urls;
    }

    public bool TryAddOutfit(string id, OutfitMeta meta)
    {
        throw new NotImplementedException();
    }

    public bool TryAddWear(string id, Meta meta)
    {
        throw new NotImplementedException();
    }
}*/