using Vintagestory.API.Common;
using System.Collections.Generic;
using MultiblockCrates;
using Vintagestory.GameContent;

namespace WoodChests;

public class AdvancedPatches : ModSystem
{
    public override void AssetsFinalize(ICoreAPI api)
    {
        List<string> woodTypes = api.GetTypesFromWorldProperties("worldproperties/block/wood.json", extraTypesAtStart: "aged").ConvertAll(type => "wood-" + type);

        Dictionary<string, LabelProps> vanillaCrateLabels = new();
        Dictionary<string, CrateTypeProperties> vanillaCrateTypeProperties = new();

        List<BlockMultiblockCrate> crates = new();

        foreach (Block block in api.World.Blocks)
        {
            if (block.Code == new AssetLocation("crate"))
            {
                vanillaCrateLabels = block.Attributes["labels"].AsObject<Dictionary<string, LabelProps>>();
                vanillaCrateTypeProperties = block.Attributes["properties"].AsObject<Dictionary<string, CrateTypeProperties>>();

                foreach (LabelProps val in vanillaCrateLabels.Values)
                {
                    val.Shape.Base.Domain = "mcrate";
                }
            }
            else if (block is BlockMultiblockCrate)
            {
                crates.Add(block as BlockMultiblockCrate);
            }
        }

        foreach (BlockMultiblockCrate crate in crates)
        {
            Dictionary<string, CrateTypeProperties> properties = vanillaCrateTypeProperties.DeepClone();
            Dictionary<string, CrateTypeProperties> newProperties = new();

            foreach (KeyValuePair<string, CrateTypeProperties> type in properties)
            {
                string dimensions = crate.FirstCodePart().Replace("crate", "");
                int result = dimensions.MultiplyFromText();
                int quantitySlots = type.Value.QuantitySlots * result;

                newProperties.Add(type.Key, new()
                {
                    RotatatableInterval = "22.5degnot45deg",
                    QuantitySlots = quantitySlots,
                    Shape = new CompositeShape() { Base = new AssetLocation($"mcrate:block/wood/crate/{dimensions}/normal-closed") }
                });
            }

            crate.ChangeAttribute("crate", "inventoryClassName");
            crate.ChangeAttribute("wood-aged", "defaultType");
            crate.ChangeAttribute(vanillaCrateLabels, "labels");
            crate.ChangeAttribute(woodTypes, "types");
            crate.ChangeAttribute(newProperties, "properties");

            if (crate.Variant["side"] == "east")
            {
                crate.AddToCreativeInventory(api.World, woodTypes);
            }
        }
    }

}