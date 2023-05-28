using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using VVapp.Models;

namespace VVappTests.Models;

[TestFixture]
public class ModelsSerializing
{
    private string _expectedSerializedParams = string.Empty;
    private OutfitMeta _expectedDeserializedParams = new();

    [SetUp]
    public void SetUp()
    {
        _expectedSerializedParams = "{" +
                                    "\"Seasons\":[0,2]," +
                                    "\"Styles\":[0,3]," +
                                    "\"Colors\":[5,7]," +
                                   "\"ClothKinds\":[1,2,3]," +
                                   "\"Budget\":5000," +
                                   "\"ForWomen\":true," +
                                   "\"ForMan\":true," +
                                    "\"ShoeSize\":40," +
                                   "\"TopSize\":3," +
                                   "\"BottomSize\":3" +
                                    "}";
        
        //TODO: переделать позже
        _expectedDeserializedParams = new OutfitMeta()
        {
            Budget = 5000,
            ForWomen = true,
            ForMan = true,
            TopSize = Size.L,
            BottomSize = Size.L,
            ShoeSize = 40,
            Seasons = new [] { Season.Summer, Season.Winter },
            Styles = new [] { Style.Casual, Style.Evening },
            Colors = new[] { ClothColors.Black , ClothColors.White},
            ClothKinds = new[] { ClothesKind.Top, ClothesKind.Bottom, ClothesKind.Shoes }
        };
    }
        
    [Test]
    public void OutfitParametersShould_BeSerializable()
    {
        var serializedParams = JsonConvert.SerializeObject(_expectedDeserializedParams);

        serializedParams.Should().Be(_expectedSerializedParams);
    }

    [Test]
    public void OutfitParametersShould_BeDeserializable()
    {
        var deserializedParams = JsonConvert.DeserializeObject<OutfitMeta>(_expectedSerializedParams);

        deserializedParams.Should().BeEquivalentTo(_expectedDeserializedParams);
    }
}