using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace MultiblockCrates.Content
{
  internal class RemoveLabelFromCrate : ModSystem
  {
    ICoreServerAPI sapi;

    public override bool ShouldLoad(EnumAppSide side)
    {
      return side == EnumAppSide.Server;
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
      sapi = api;
      sapi.RegisterCommand("removelabel", Lang.Get("mcrate:RemoveLabelFromCrate.Description"), "", onCrateRemoveLabelSetCmd, null);
    }

    private void onCrateRemoveLabelSetCmd(IServerPlayer player, int groupId, CmdArgs args)
    {
      IServerWorldAccessor world = sapi.World;
      ItemSlot activeSlot = player.InventoryManager.ActiveHotbarSlot;
      var labelstack = new ItemStack(world.GetItem(new AssetLocation("paper-parchment")));

      if (player.CurrentBlockSelection?.Position != null)
      {
        var bect = world.BlockAccessor.GetBlockEntity(player.CurrentBlockSelection.Position) as BlockEntityCrate;

        if (bect == null)
        {
          player.SendMessage(groupId, Lang.Get("mcrate:RemoveLabelFromCrate.NoCrate"), EnumChatType.CommandError);
          return;
        }

        if (bect.label != "paper-empty")
        {
          player.SendMessage(groupId, Lang.Get("mcrate:RemoveLabelFromCrate.CrateWithoutLabel"), EnumChatType.CommandError);
          return;
        }

        bect.label = null;
        bect.MarkDirty(true);

        if (!player.InventoryManager.TryGiveItemstack(labelstack))
        {

          world.SpawnItemEntity(labelstack, player.Entity.Pos.XYZ);
        }

        player.SendMessage(groupId, Lang.Get("mcrate:RemoveLabelFromCrate.DidRemoveLabel"), EnumChatType.CommandSuccess);
      }
    }
  }
}