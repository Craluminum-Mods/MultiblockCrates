using System.Collections.Generic;
using Vintagestory.API.Config;

namespace MultiblockCrates;

public static class Constants
{
    public const string Namespace = nameof(MultiblockCrates);
    public const string DefaultLabel = "paper-empty";

    public static readonly List<string> Sizes = new() { "1x2", "1x3", "2x1", "2x2x2", "2x2x3", "3x1", "3x2x2", "3x2x3", "3x3x2", "3x3x3" };

    public static readonly string RemoveOrAddLabelName = Lang.Get("mcrate:RemoveOrAddLabel.Name");

    public static readonly string NoCrate = Lang.Get("mcrate:Error.NoCrate");
    public static readonly string NoLabel = Lang.Get("mcrate:Error.NoLabel");
    public static readonly string HasDiffLabel = Lang.Get("mcrate:Error.HasDiffLabel");

    public static readonly string LabelRemoved = Lang.Get("mcrate:Success.LabelRemoved");
    public static readonly string LabelAdded = Lang.Get("mcrate:Success.LabelAdded");
}