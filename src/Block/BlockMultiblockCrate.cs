using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace MultiblockCrates.Content
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

		public override string GetHeldItemName(ItemStack itemStack)
		{
			string @string = itemStack.Attributes.GetString("type", this.Props.DefaultType);
			string text = itemStack.Attributes.GetString("lidState", "closed");
			if (text.Length == 0)
			{
				text = "closed";
			}
			string[] array = new string[5];
			int num = 0;
			AssetLocation code = this.Code;
			array[num] = ((code != null) ? code.Domain : null);
			array[1] = ":block-";
			array[2] = @string;
			array[3] = "-";
			int num2 = 4;
			AssetLocation code2 = this.Code;
			array[num2] = ((code2 != null) ? code2.Path : null);
			return Lang.GetMatching(string.Concat(array), new object[]
			{
				Lang.Get("cratelidstate-" + text, new object[]
				{
					"closed"
				}
				),
				Lang.Get("mcrate:cratewood-" + @string, new object[]
				{
					"wood"
				}
				)
			});
		}
  }
}