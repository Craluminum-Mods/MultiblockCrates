using Vintagestory.API.Common;

[assembly: ModInfo("Multiblock Crates")]

namespace MultiblockCrates;

public class MultiblockCrates : ModSystem
{
    public override void Start(ICoreAPI api)
    {
        base.Start(api);
        api.RegisterBlockClass("BlockMultiblockCrate", typeof(BlockMultiblockCrate));
        api.World.Logger.Event("started '{0}' mod", Mod.Info.Name);
    }
}