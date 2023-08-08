using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;

namespace MultiblockCrates;

public static class CollectibleExtensions
{
    public static void ChangeAttribute(this CollectibleObject obj, object val, params string[] path)
    {
        obj.Attributes ??= new JsonObject(new JObject());

        switch (path.Length)
        {
            case 1:
                obj.Attributes.Token[path[0]] = JToken.FromObject(val);
                break;
            case 2:
                obj.Attributes.Token[path[0]][path[1]] = JToken.FromObject(val);
                break;
        }
    }

    public static void AddToCreativeInventory(this CollectibleObject obj, IWorldAccessor world, List<string> types)
    {
        JsonItemStack[] stacks = types.ConvertAll(type => obj.GenJstack(world, $"{{ lidState: \"closed\", type: \"{type}\" }}")).ToArray();

        obj.CreativeInventoryStacks = new CreativeTabAndStackList[]
        {
            new CreativeTabAndStackList() { Stacks = stacks, Tabs = new string[] { "general", "decorative" } }
        };
    }

    public static JsonItemStack GenJstack(this CollectibleObject obj, IWorldAccessor world, string jsonAttributes)
    {
        JsonItemStack jsonItemStack = new()
        {
            Code = obj.Code,
            Type = obj.ItemClass,
            Attributes = new JsonObject(JToken.Parse(jsonAttributes))
        };
        jsonItemStack.Resolve(world, "");
        return jsonItemStack;
    }

    public static List<string> GetTypesFromWorldProperties(this ICoreAPI api, string pathToWorldProperties, params string[] extraTypesAtStart)
    {
        List<string> newList = api.Assets
            .Get<StandardWorldProperty>(new AssetLocation(pathToWorldProperties)).Variants
            .Select(x => x.Code.Path)
            .ToArray()
            .ToList();

        if (extraTypesAtStart?.Length != 0)
        {
            foreach (string type in extraTypesAtStart)
            {
                newList.Insert(0, type);
            }
        }

        return newList;
    }
}