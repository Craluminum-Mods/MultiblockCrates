using System.Collections.Generic;
using Vintagestory.API.Common;
using Vintagestory.ServerMods.NoObf;
using System;
using System.Linq;
using static MultiblockCrates.Constants;

namespace MultiblockCrates;

public class JsonPatches : ModSystem
{
    public override double ExecuteOrder() => 0.2;

    public override void AssetsLoaded(ICoreAPI api)
    {
        int TickCount = Environment.TickCount;
        List<JsonPatch> patches = CreatePatches(api);
        api.ApplyJsonPatches(patches);
        api.Logger.Debug(Namespace + ": {0} took {1} ms", ToString(), Environment.TickCount - TickCount);
    }

    private List<JsonPatch> CreatePatches(ICoreAPI api)
    {
        List<string> woodTypes = api.GetTypesFromWorldProperties("worldproperties/block/wood.json", extraTypesAtStart: "aged");

        List<string> crateWoodTypes = woodTypes.Take(13).ToList();
        List<string> notCrateWoodTypes = woodTypes.Skip(13).ToList();

        List<JsonPatch> patches = new();

        foreach (string size in Sizes)
        {
            AssetLocation file = new($"mcrate:blocktypes/crate{size}.json");

            int TickCount1 = Environment.TickCount;

            foreach (string type in crateWoodTypes)
            {
                try
                {
                    patches.Add(new JsonPatch()
                    {
                        Op = EnumJsonPatchOp.Add,
                        Value = new { @base = $"game:block/wood/crate/{type}-inside" }.Parse(),
                        Path = $"/textures/wood-{type}-inside",
                        File = file
                    });
                    patches.Add(new JsonPatch()
                    {
                        Op = EnumJsonPatchOp.Add,
                        Value = new { @base = $"game:block/wood/crate/{type}-sides" }.Parse(),
                        Path = $"/textures/wood-{type}-sides",
                        File = file
                    });
                }
                catch (Exception e)
                {
                    api.Logger.Error(Namespace + ": Failed to patch file {0}: {1}", file, e);
                }
            }

            foreach (string type in notCrateWoodTypes)
            {
                try
                {
                    patches.Add(new JsonPatch()
                    {
                        Op = EnumJsonPatchOp.Add,
                        Value = new { @base = $"game:block/wood/planks/{type}1", rotation = 90 }.Parse(),
                        Path = $"/textures/wood-{type}-inside",
                        File = file
                    });
                    patches.Add(new JsonPatch()
                    {
                        Op = EnumJsonPatchOp.Add,
                        Value = new { @base = $"game:block/wood/planks/{type}1", rotation = 90 }.Parse(),
                        Path = $"/textures/wood-{type}-sides",
                        File = file
                    });
                }
                catch (Exception e)
                {
                    api.Logger.Error(Namespace + ": Failed to patch file {0}: {1}", file, e);
                }
            }

            api.Logger.Debug(Namespace + ": {0} took {1} ms", nameof(CreatePatches) + " for crate textures", Environment.TickCount - TickCount1);
        }

        return patches;
    }
}