using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace MultiblockCrates;

public static class Extensions
{
    public static T GetBlockEntityExt<T>(this IBlockAccessor blockAccessor, BlockPos pos) where T : BlockEntity
    {
        if (blockAccessor.GetBlockEntity<T>(pos) is T becrate)
        {
            return becrate;
        }

        if (blockAccessor.GetBlock(pos) is BlockMultiblock multiblock)
        {
            BlockPos multiblockPos = new(pos.X + multiblock.OffsetInv.X, pos.Y + multiblock.OffsetInv.Y, pos.Z + multiblock.OffsetInv.Z);

            return blockAccessor.GetBlockEntity<T>(multiblockPos);
        }

        return null;
    }
}