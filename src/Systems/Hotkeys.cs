using Vintagestory.API.Common;
using Vintagestory.API.Client;
using static MultiblockCrates.Constants;

namespace MultiblockCrates;

public class Hotkeys : ModSystem
{
    public override bool ShouldLoad(EnumAppSide forSide) => forSide == EnumAppSide.Client;

    public override void StartClientSide(ICoreClientAPI api)
    {
        base.StartClientSide(api);
        api.Input.RegisterHotKey("removeoraddcratelabel", RemoveOrAddLabelName, GlKeys.X, HotkeyType.GUIOrOtherControls, shiftPressed: true);
        api.Input.SetHotKeyHandler("removeoraddcratelabel", _ => RemoveOrAddLabel(api));
    }

    public bool RemoveOrAddLabel(ICoreClientAPI api)
    {
        api.SendChatMessage("/racratelbl");
        return true;
    }
}