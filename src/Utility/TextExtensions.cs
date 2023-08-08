using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Vintagestory.API.Datastructures;

namespace MultiblockCrates;

public static class TextExtensions
{
    public static JsonObject Parse(this object obj)
    {
        return new(JToken.FromObject(obj));
    }

    public static int MultiplyFromText(this string text)
    {
        List<int> dimensions = text.Split('x').ToList().ConvertAll(int.Parse);
        return dimensions.Aggregate((a, b) => a * b);
    }
}