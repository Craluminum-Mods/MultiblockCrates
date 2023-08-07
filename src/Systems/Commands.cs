using Vintagestory.API.Common;
using Vintagestory.API.Server;
using Vintagestory.GameContent;
using Vintagestory.API.MathTools;
using static MultiblockCrates.Constants;

namespace MultiblockCrates;

public class Commands : ModSystem
{
    private ItemStack LabelStack { get; set; }

    public override bool ShouldLoad(EnumAppSide forSide) => forSide == EnumAppSide.Server;

    public override void StartServerSide(ICoreServerAPI api)
    {
        LabelStack = new(api.World.GetItem(new AssetLocation("paper-parchment")));

        api.ChatCommands.GetOrCreate("racratelbl")
            .WithName(RemoveOrAddLabelName)
            .RequiresPlayer()
            .RequiresPrivilege("useblock")
            .HandleWith(RemoveOrAddLabel);
    }

    public TextCommandResult RemoveOrAddLabel(TextCommandCallingArgs args)
    {
        IServerPlayer player = args.Caller.Player as IServerPlayer;
        IWorldAccessor world = args.Caller.Player.Entity.World;
        BlockPos pos = player?.CurrentBlockSelection?.Position;

        if (pos == null)
        {
            return TextCommandResult.Success(NoCrate);
        }

        ItemSlot activeSlot = player.InventoryManager.ActiveHotbarSlot;

        BlockEntityCrate becrate = world.GetCrate(pos);

        if (becrate == null)
        {
            return TextCommandResult.Error(NoCrate);
        }

        switch (becrate.label)
        {
            case "paper-empty":
                return RemoveLabel(player, becrate);
            case not null and not "":
                return TextCommandResult.Error(HasDiffLabel);
            default:
                if (activeSlot?.Itemstack?.Collectible?.Code == LabelStack.Clone().Collectible.Code)
                {
                    return AddLabel(player, becrate);
                }
                else
                {
                    return TextCommandResult.Error(NoLabel);
                }
        }
    }

    public TextCommandResult RemoveLabel(IServerPlayer player, BlockEntityCrate bect)
    {
        if (!player.InventoryManager.TryGiveItemstack(LabelStack.Clone(), true))
        {
            bect.Api.World.SpawnItemEntity(LabelStack.Clone(), player.Entity.ServerPos.XYZ);
        }

        bect.label = null;
        bect.MarkDirty(true);

        return TextCommandResult.Success(LabelRemoved);
    }

    public TextCommandResult AddLabel(IServerPlayer player, BlockEntityCrate bect)
    {
        player.Entity.ActiveHandItemSlot.TakeOut(1);
        player.Entity.ActiveHandItemSlot.MarkDirty();

        bect.label = "paper-empty";
        bect.MarkDirty(true);

        return TextCommandResult.Success(LabelAdded);
    }
}