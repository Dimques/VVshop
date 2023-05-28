using VVapp.Models;

namespace VVapp.DbProviders;

public interface IDbProvider
{
    public bool TryAddOutfit(string outfitJson, IFormFile outfitImage);
    public bool TryAddWear(string wearJson, IFormFile wearImage);
    public bool TryAddOuterwear(string outerwearJson, IFormFile outerwearImage);
    public bool TryAddTopWear(string topWearJson, IFormFile topWearImage);
    public bool TryAddBottomWear(string bottomWearJson, IFormFile bottomWearImage);
    public bool TryAddShoes(string shoesJson, IFormFile shoesImage);
    public bool TryAddAccessory(string accessoryJson, IFormFile accessoryImage);
    
    
}