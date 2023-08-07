using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace MultiblockCrates;

public static class Extensions
{
    public static BlockEntityCrate GetCrate(this IWorldAccessor world, BlockPos pos)
    {
        BlockEntity blockEntity = world.BlockAccessor.GetBlockEntity(pos);
        BlockBehaviorMultiblock bhMultiblock = blockEntity?.GetBehavior<BlockBehaviorMultiblock>();

        if (blockEntity == null) return null;

        return blockEntity is BlockEntityCrate _becrate
            ? _becrate
            : world.BlockAccessor.GetBlockEntity<BlockEntityCrate>(bhMultiblock?.GetField<Vec3i>("ControllerPositionRel")?.AsBlockPos);
    }
}