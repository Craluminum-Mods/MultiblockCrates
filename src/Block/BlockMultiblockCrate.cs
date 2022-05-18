using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace MultiblockContainers.Content
{
	public class BlockMultiblockCrate : BlockCrate
  {
		public override ItemStack OnPickBlock(IWorldAccessor world, BlockPos pos)
		{
			ItemStack itemStack = new ItemStack(world.GetBlock(CodeWithVariant("side", "east")), 1);
			BlockEntityCrate blockEntityCrate = world.BlockAccessor.GetBlockEntity(pos) as BlockEntityCrate;
			if (blockEntityCrate != null)
			{
				itemStack.Attributes.SetString("type", blockEntityCrate.type);
				if (blockEntityCrate.label != null && blockEntityCrate.label.Length > 0)
				{
					itemStack.Attributes.SetString("label", blockEntityCrate.label);
				}
				itemStack.Attributes.SetString("lidState", blockEntityCrate.preferredLidState);
			}
			else
			{
				itemStack.Attributes.SetString("type", this.Props.DefaultType);
			}
			return itemStack;
		}
  }
}