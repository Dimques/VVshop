namespace VVapp.Models;

public class OutfitMeta : Meta
{
    public Outerwear Outerwear { get; set; }
    public TopWear TopWear { get; set; }
    public BottomWear BottomWear { get; set; }
    public Shoes Shoes { get; set; }
    public Accessories Accessories { get; set; }
}