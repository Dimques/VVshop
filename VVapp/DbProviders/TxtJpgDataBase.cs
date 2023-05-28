using System.Collections.Concurrent;
using Newtonsoft.Json;
using VVapp.Loggers;
using VVapp.Models;

namespace VVapp.DbProviders;

public class TxtJpgDataBase : IDbProvider
{
    private readonly ILog log;
    private readonly Dictionary<Type, string> typeToPath;

    private readonly ConcurrentDictionary<int, OutfitMeta> outfitsStorage = new();
    private readonly ConcurrentDictionary<int, ShoesMeta> shoesStorage = new();
    private readonly ConcurrentDictionary<int, TopWearMeta> topWearStorage = new();
    private readonly ConcurrentDictionary<int, OuterwearMeta> outerwearStorage = new();
    private readonly ConcurrentDictionary<int, BottomWearMeta> bottomWearStorage = new();
    private readonly ConcurrentDictionary<int, AccessoriesMeta> accessoriesStorage = new();


    public TxtJpgDataBase(IWebHostEnvironment env, ILog log)
    {
        this.log = log.ForContext("TxtJpgDataBase");
        var rootPath = env.ContentRootPath; ;

        typeToPath = new Dictionary<Type, string>
        {
            {typeof(OutfitMeta), rootPath + @"\wwwroot\db\outfits"},
            { typeof(ShoesMeta), rootPath + @"\wwwroot\db\shoes" },
            { typeof(TopWearMeta), rootPath + @"\wwwroot\db\topwear" },
            { typeof(OuterwearMeta), rootPath + @"\wwwroot\db\outerwear" },
            { typeof(BottomWearMeta), rootPath + @"\wwwroot\db\bottomwear" },
            { typeof(AccessoriesMeta), rootPath + @"\wwwroot\db\accessories" }
        };

        CreateDirectoriesIfNotExist();

        InitOutfitsStorage();
        InitShoeStorage();
        InitTopWearStorage();
        InitOuterwearStorage();
        InitBottomWearStorage();
        InitAccessoriesStorage();
    }
    
    public IEnumerable<string> GetOutfitsUrls(OutfitMeta outfitMeta)
    {
        throw new NotImplementedException();
    }

    public bool TryAddOutfit(string outfitJson, IFormFile outfitImage)
    {
        OutfitMeta outfitMeta;
        try
        {
            outfitMeta = JsonConvert.DeserializeObject<OutfitMeta>(outfitJson) ?? new OutfitMeta();
        }
        catch (Exception e)
        {
            log.Error(e);
            throw;
        }

        var isSuccessful = outfitsStorage.TryAdd(outfitMeta.Id, outfitMeta);
        if (!isSuccessful)
        {
            var path = typeToPath[typeof(OutfitMeta)];
            var filePathWithoutExtension = Path.Combine(path, outfitMeta.Id.ToString());
            var txtPath = filePathWithoutExtension + ".txt";
            var jpgPath = filePathWithoutExtension + ".jpg";
                
            File.WriteAllText(txtPath, outfitJson);

            using var fileStream = new FileStream(jpgPath, FileMode.Create);
            outfitImage.CopyTo(fileStream);
        }
        else
        {
            log.Warn($"Outfit with id '{outfitMeta.Id}' is already in database!");
        }

        return isSuccessful;
    }

    public bool TryAddWear(string wearJson, IFormFile wearImage)
    {
        if (wearJson.Contains(DbConstants.Outerwear))
            return TryAddOuterwear(wearJson, wearImage);
        else if (wearJson.Contains(DbConstants.TopWear))
            return TryAddTopWear(wearJson, wearImage);
        else if (wearJson.Contains(DbConstants.BottomWear))
            return TryAddBottomWear(wearJson, wearImage);
        else if (wearJson.Contains(DbConstants.Shoes))
            return TryAddShoes(wearJson, wearImage);
        else if (wearJson.Contains(DbConstants.Accessories))
            return TryAddAccessory(wearJson, wearImage);

        var message = $"Unknown wear type: '{wearJson}'";
        log.Warn(message);
        
        throw new ArgumentException(message);
    }

    public bool TryAddOuterwear(string outerwearJson, IFormFile outerwearImage)
    {
        OuterwearMeta outerwearMeta;
        try
        {
            outerwearMeta = JsonConvert.DeserializeObject<OuterwearMeta>(outerwearJson) ?? new OuterwearMeta();
            log.Info($"Deserialized json: id={outerwearMeta.Id}, budget={outerwearMeta.Budget}," +
                     $" gender={outerwearMeta.Gender}, seasons={string.Join("|", outerwearMeta.Seasons)}," +
                     $" styles={string.Join("|", outerwearMeta.Styles)}, colors={string.Join("|", outerwearMeta.Colors)}," +
                     $" kind={outerwearMeta.Outerwear}");
        }
        catch (Exception e)
        {
            log.Error(e);
            throw;
        }

        var isSuccessful = outerwearStorage.TryAdd(outerwearMeta.Id, outerwearMeta);
        if (isSuccessful)
        {
            var path = typeToPath[typeof(OuterwearMeta)];
            var filePathWithoutExtension = Path.Combine(path, outerwearMeta.Id.ToString());
            var txtPath = filePathWithoutExtension + ".txt";
            var jpgPath = filePathWithoutExtension + ".jpg";
                
            File.WriteAllText(txtPath, outerwearJson);

            using var fileStream = new FileStream(jpgPath, FileMode.Create);
            outerwearImage.CopyTo(fileStream);
        }
        else
        {
            log.Warn($"Outerwear with id '{outerwearMeta.Id}' is already in database!");
        }

        return isSuccessful;
    }
    
    public bool TryAddTopWear(string topWearJson, IFormFile topWearImage)
    {
        TopWearMeta topWearMeta;
        try
        {
            topWearMeta = JsonConvert.DeserializeObject<TopWearMeta>(topWearJson) ?? new TopWearMeta();
        }
        catch (Exception e)
        {
            log.Error(e);
            throw;
        }

        var isSuccessful = topWearStorage.TryAdd(topWearMeta.Id, topWearMeta);
        if (isSuccessful)
        {
            var path = typeToPath[typeof(OuterwearMeta)];
            var filePathWithoutExtension = Path.Combine(path, topWearMeta.Id.ToString());
            var txtPath = filePathWithoutExtension + ".txt";
            var jpgPath = filePathWithoutExtension + ".jpg";
                
            File.WriteAllText(txtPath, topWearJson);

            using var fileStream = new FileStream(jpgPath, FileMode.Create);
            topWearImage.CopyTo(fileStream);
        }
        else
        {
            log.Warn($"TopWear with id '{topWearMeta.Id}' is already in database!");
        }

        return isSuccessful;
    }
    
    public bool TryAddBottomWear(string bottomWearJson, IFormFile bottomWearImage)
    {
        BottomWearMeta bottomWearMeta;
        try
        {
            bottomWearMeta = JsonConvert.DeserializeObject<BottomWearMeta>(bottomWearJson) ?? new BottomWearMeta();
        }
        catch (Exception e)
        {
            log.Error(e);
            throw;
        }

        var isSuccessful = bottomWearStorage.TryAdd(bottomWearMeta.Id, bottomWearMeta);
        if (isSuccessful)
        {
            var path = typeToPath[typeof(OuterwearMeta)];
            var filePathWithoutExtension = Path.Combine(path, bottomWearMeta.Id.ToString());
            var txtPath = filePathWithoutExtension + ".txt";
            var jpgPath = filePathWithoutExtension + ".jpg";
                
            File.WriteAllText(txtPath, bottomWearJson);

            using var fileStream = new FileStream(jpgPath, FileMode.Create);
            bottomWearImage.CopyTo(fileStream);
        }
        else
        {
            log.Warn($"BottomWear with id '{bottomWearMeta.Id}' is already in database!");
        }

        return isSuccessful;
    }
    
    public bool TryAddShoes(string shoesJson, IFormFile shoesImage)
    {
        ShoesMeta shoesMeta;
        try
        {
            shoesMeta = JsonConvert.DeserializeObject<ShoesMeta>(shoesJson) ?? new ShoesMeta();
        }
        catch (Exception e)
        {
            log.Error(e);
            throw;
        }

        var isSuccessful = shoesStorage.TryAdd(shoesMeta.Id, shoesMeta);
        if (isSuccessful)
        {
            var path = typeToPath[typeof(OuterwearMeta)];
            var filePathWithoutExtension = Path.Combine(path, shoesMeta.Id.ToString());
            var txtPath = filePathWithoutExtension + ".txt";
            var jpgPath = filePathWithoutExtension + ".jpg";
                
            File.WriteAllText(txtPath, shoesJson);

            using var fileStream = new FileStream(jpgPath, FileMode.Create);
            shoesImage.CopyTo(fileStream);
        }
        else
        {
            log.Warn($"Shoes with id '{shoesMeta.Id}' is already in database!");
        }

        return isSuccessful;
    }
    
    public bool TryAddAccessory(string accessoryJson, IFormFile accessoryImage)
    {
        AccessoriesMeta accessoryMeta;
        try
        {
            accessoryMeta = JsonConvert.DeserializeObject<AccessoriesMeta>(accessoryJson) ?? new AccessoriesMeta();
        }
        catch (Exception e)
        {
            log.Error(e);
            throw;
        }

        var isSuccessful = accessoriesStorage.TryAdd(accessoryMeta.Id, accessoryMeta);
        if (isSuccessful)
        {
            var path = typeToPath[typeof(OuterwearMeta)];
            var filePathWithoutExtension = Path.Combine(path, accessoryMeta.Id.ToString());
            var txtPath = filePathWithoutExtension + ".txt";
            var jpgPath = filePathWithoutExtension + ".jpg";
                
            File.WriteAllText(txtPath, accessoryJson);

            using var fileStream = new FileStream(jpgPath, FileMode.Create);
            accessoryImage.CopyTo(fileStream);
        }
        else
        {
            log.Warn($"Accessory with id '{accessoryMeta.Id}' is already in database!");
        }

        return isSuccessful;
    }

    private void CreateDirectoriesIfNotExist()
    {
        foreach (var kvp in typeToPath)
        {
            var pathToStore = kvp.Value;
            if (!Directory.Exists(pathToStore))
            {
                log.Info($"Try to create directory: {pathToStore}...");
                Directory.CreateDirectory(pathToStore);
                log.Info($"Successful!");
            }
        }
    }

    private void InitOutfitsStorage()
    {
        var txtFilePaths = Directory.GetFiles(typeToPath[typeof(OutfitMeta)])
            .Where(path => path.EndsWith(".txt"));
        foreach (var path in txtFilePaths)
        {
            var outfitName = path.Split("\\").LastOrDefault();
            log.Info($"Try add outfit '{outfitName}' to the storage...");
            var json = File.ReadAllText(outfitName!);
            OutfitMeta outfitMeta;
            try
            {
                outfitMeta = JsonConvert.DeserializeObject<OutfitMeta>(json) ?? new OutfitMeta();
            }
            catch (Exception e)
            {
                log.Error(e);
                throw;
            }

            if (!outfitsStorage.TryAdd(outfitMeta.Id, outfitMeta))
                log.Warn($"Can't add outfit to storage while initializing: already have outfit with id '{outfitMeta.Id}'");
            else
                log.Info("Successful!");
        }
    }
    
    
    
    private void InitShoeStorage()
    {
        var txtFilePaths = Directory.GetFiles(typeToPath[typeof(ShoesMeta)])
            .Where(path => path.EndsWith(".txt"));;
        foreach (var path in txtFilePaths)
        {
            var shoe = path.Split("\\").LastOrDefault();
            log.Info($"Try add shoes '{shoe}' to the storage...");
            
            var json = File.ReadAllText(path);
            ShoesMeta shoesMeta;
            try
            {
                shoesMeta = JsonConvert.DeserializeObject<ShoesMeta>(json) ?? new ShoesMeta();
            }
            catch (Exception e)
            {
                log.Error(e);
                throw;
            }

            if (!shoesStorage.TryAdd(shoesMeta.Id, shoesMeta))
                log.Warn($"Can't add shoes to storage while initializing: already have id '{shoesMeta.Id}'");
            else
                log.Info("Successful!");
        }
    }
    
    private void InitOuterwearStorage()
    {
        var filePaths = Directory.GetFiles(typeToPath[typeof(OuterwearMeta)])
            .Where(path => path.EndsWith(".txt"));;
        foreach (var path in filePaths)
        {
            var outerwear = path.Split("\\").LastOrDefault();
            log.Info($"Try add outerwear '{outerwear}' to the storage...");
            
            var json = File.ReadAllText(path);
            OuterwearMeta outerwearMeta;
            try
            {
                outerwearMeta = JsonConvert.DeserializeObject<OuterwearMeta>(json) ?? new OuterwearMeta();
            }
            catch (Exception e)
            {
                log.Error(e);
                throw;
            }

            if (!outerwearStorage.TryAdd(outerwearMeta.Id, outerwearMeta))
                log.Warn($"Can't add outerwear to storage while initializing: already have id '{outerwearMeta.Id}'");
            else
                log.Info("Successful!");
        }
    }
    
    private void InitTopWearStorage()
    {
        var filePaths = Directory.GetFiles(typeToPath[typeof(TopWearMeta)])
            .Where(path => path.EndsWith(".txt"));;
        foreach (var path in filePaths)
        {
            var topWear = path.Split("\\").LastOrDefault();
            log.Info($"Try add topwear '{topWear}' to the storage...");
            
            var json = File.ReadAllText(path);
            TopWearMeta topWearMeta;
            try
            {
                topWearMeta = JsonConvert.DeserializeObject<TopWearMeta>(json) ?? new TopWearMeta();
            }
            catch (Exception e)
            {
                log.Error(e);
                throw;
            }

            if (!topWearStorage.TryAdd(topWearMeta.Id, topWearMeta))
                log.Warn($"Can't add topwear to storage while initializing: already have id '{topWearMeta.Id}'");
            else
                log.Info("Successful!");
        }
    }
    
    private void InitBottomWearStorage()
    {
        var filePaths = Directory.GetFiles(typeToPath[typeof(BottomWearMeta)])
            .Where(path => path.EndsWith(".txt"));;
        foreach (var path in filePaths)
        {
            var bottomWear = path.Split("\\").LastOrDefault();
            log.Info($"Try add bottomwear '{bottomWear}' to the storage...");
            
            var json = File.ReadAllText(path);
            BottomWearMeta bottomWearMeta;
            try
            {
                bottomWearMeta = JsonConvert.DeserializeObject<BottomWearMeta>(json) ?? new BottomWearMeta();
            }
            catch (Exception e)
            {
                log.Error(e);
                throw;
            }

            if (!bottomWearStorage.TryAdd(bottomWearMeta.Id, bottomWearMeta))
                log.Warn($"Can't add bottomwear to storage while initializing: already have id '{bottomWearMeta.Id}'");
            else
                log.Info("Successful!");
        }
    }
    
    private void InitAccessoriesStorage()
    {
        var filePaths = Directory.GetFiles(typeToPath[typeof(AccessoriesMeta)])
            .Where(path => path.EndsWith(".txt"));;
        foreach (var path in filePaths)
        {
            var accessory = path.Split("\\").LastOrDefault();
            log.Info($"Try add accessory '{accessory}' to the storage...");
            
            var json = File.ReadAllText(path);
            AccessoriesMeta accessoriesMeta;
            try
            {
                accessoriesMeta = JsonConvert.DeserializeObject<AccessoriesMeta>(json) ?? new AccessoriesMeta();
            }
            catch (Exception e)
            {
                log.Error(e);
                throw;
            }

            if (!accessoriesStorage.TryAdd(accessoriesMeta.Id, accessoriesMeta))
                log.Warn($"Can't add accessory to storage while initializing: already have id '{accessoriesMeta.Id}'");
            else
                log.Info("Successful!");
        }
    }
}