using System.Collections.Generic;
using System.Text;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.ServerMods.NoObf;
using static MultiblockCrates.Constants;

namespace MultiblockCrates;

public static class JsonPatchExtensions
{
    public static void ApplyJsonPatches(this ICoreAPI api, List<JsonPatch> patches)
    {
        ModJsonPatchLoader loader = api.ModLoader.GetModSystem<ModJsonPatchLoader>();

        int appliedCount = 0;
        int notfoundCount = 0;
        int errorCount = 0;
        int totalCount = 0;

        if (patches != null)
        {
            for (int i = 0; i < patches.Count; i++)
            {
                JsonPatch patch = patches[i];

                totalCount++;
                loader.ApplyPatch(i, new AssetLocation(nameof(JsonPatches)), patch, ref appliedCount, ref notfoundCount, ref errorCount);
            }
        }

        StringBuilder sb = new();
        if (appliedCount > 0)
        {
            sb.Append(Namespace + ": JsonPatch Loader: ");
            sb.Append(Lang.Get(", successfully applied {0} patches", appliedCount));
        }

        api.Logger.Notification(sb.ToString());
        api.Logger.VerboseDebug(Namespace + ": Patchloader finished");
    }
}