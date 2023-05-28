namespace VVapp.Models;

public class Meta
{
    public int Id { get; set; }
    public int Budget { get; set; }
    public Gender Gender { get; set; }
    public Season[] Seasons { get; set; }
    public Style[] Styles { get; set; }
    public ClothColors[] Colors { get; set; }
}