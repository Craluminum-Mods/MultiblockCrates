using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace MultiblockCrates;

public static class Extensions
{
    public static BlockEntityCrate GetCrate(this IWorldAccessor world, BlockPos pos)
    {
        if (world.BlockAccessor.GetBlockEntity<BlockEntityCrate>(pos) is BlockEntityCrate becrate)
        {
            return becrate;
        }

        if (world.BlockAccessor.GetBlock(pos) is BlockMultiblock multiblock)
        {
            BlockPos multiblockPos = new(pos.X + multiblock.OffsetInv.X, pos.Y + multiblock.OffsetInv.Y, pos.Z + multiblock.OffsetInv.Z);

            return world.BlockAccessor.GetBlockEntity<BlockEntityCrate>(multiblockPos);
        }

        return null;
    }
}