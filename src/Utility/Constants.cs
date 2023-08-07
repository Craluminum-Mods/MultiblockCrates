using Vintagestory.API.Config;

namespace MultiblockCrates;

public static class Constants
{
    public const string DefaultLabel = "paper-empty";

    public static readonly string RemoveOrAddLabelName = Lang.Get("mcrate:RemoveOrAddLabel.Name");

    public static readonly string NoCrate = Lang.Get("mcrate:Error.NoCrate");
    public static readonly string NoLabel = Lang.Get("mcrate:Error.NoLabel");
    public static readonly string HasDiffLabel = Lang.Get("mcrate:Error.HasDiffLabel");

    public static readonly string LabelRemoved = Lang.Get("mcrate:Success.LabelRemoved");
    public static readonly string LabelAdded = Lang.Get("mcrate:Success.LabelAdded");
}