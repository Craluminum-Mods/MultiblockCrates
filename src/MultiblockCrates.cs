using MultiblockContainers.Content;
using Vintagestory.API.Common;

[assembly: ModInfo("MultiblockCrates",
  Authors = new[] { "Craluminum2413" })]

namespace MultiblockCrates
{
  class MultiblockCrates : ModSystem
  {
    public override void Start(ICoreAPI api)
    {
      base.Start(api);
      api.RegisterBlockClass("BlockMultiblockCrate", typeof(BlockMultiblockCrate));
      api.World.Logger.Event("started 'Multiblock Crates' mod");
    }
  }
}