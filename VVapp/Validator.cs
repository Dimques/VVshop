using VVapp.Models;

namespace VVapp;

public static class Validator
{
    public static bool ValidateOutfitParameters(OutfitMeta? parameters)
    {
        if (parameters is null) 
            return false;
        
        var isBudgetCorrect = parameters.Budget is >= 0 and <= int.MaxValue;

        return isBudgetCorrect;
    }
}