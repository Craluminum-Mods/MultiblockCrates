using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace MultiblockCrates;

public class BlockMultiblockCrate : BlockCrate
{
    public override ItemStack OnPickBlock(IWorldAccessor world, BlockPos pos)
    {
        ItemStack itemStack = new(world.GetBlock(CodeWithVariant("side", "east")), 1);
        BlockEntityCrate blockEntityCrate = world.BlockAccessor.GetBlockEntity<BlockEntityCrate>(pos);
        if (blockEntityCrate != null)
        {
            itemStack.Attributes.SetString("type", blockEntityCrate.type);
            if (!string.IsNullOrEmpty(blockEntityCrate.label))
            {
                itemStack.Attributes.SetString("label", blockEntityCrate.label);
            }
            itemStack.Attributes.SetString("lidState", blockEntityCrate.preferredLidState);
        }
        else
        {
            itemStack.Attributes.SetString("type", Props.DefaultType);
        }
        return itemStack;
    }

    public override string GetHeldItemName(ItemStack itemStack)
    {
        string size = itemStack.Collectible.Code.FirstCodePart().Replace("crate", "");
        string wood = GetTranslatedWood(itemStack, Props.DefaultType);
        string state = GetState(itemStack);

        return Lang.GetMatching($"block-woodencrate-{state}") + string.Format(" ({0}) ({1})", wood, size);
    }

    private static string GetTranslatedWood(ItemStack itemStack, string defaultType)
    {
        string type = itemStack.Attributes.GetString("type", defaultType).Replace("wood-", "");
        return Lang.Get("material-" + type);
    }

    private static string GetState(ItemStack itemStack)
    {
        string state = itemStack.Attributes.GetString("lidState", "closed");

        return state.Length switch
        {
            0 => "closed",
            _ => state,
        };
    }
}