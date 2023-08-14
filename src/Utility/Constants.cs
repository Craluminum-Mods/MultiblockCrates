using System.Collections.Generic;

namespace MultiblockCrates;

public static class Constants
{
    public const string Namespace = nameof(MultiblockCrates);

    public static readonly List<string> Sizes = new() { "1x2", "1x3", "2x1", "2x2x2", "2x2x3", "3x1", "3x2x2", "3x2x3", "3x3x2", "3x3x3" };
}