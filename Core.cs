using MelonLoader;
using HarmonyLib;
using ScheduleOne.UI.Phone.Delivery;
using ScheduleOne.UI.Shop;
using ScheduleOne;
using static ScheduleOne.Registry;
using ScheduleOne.ItemFramework;

[assembly: MelonInfo(typeof(StackBaseUnitShop.Core), StackBaseUnitShop.BuildInfo.Name, StackBaseUnitShop.BuildInfo.Version, StackBaseUnitShop.BuildInfo.Author, StackBaseUnitShop.BuildInfo.DownloadLink)]
[assembly: MelonColor()]
[assembly: MelonGame("TVGS", "Schedule I")]

namespace StackBaseUnitShop
{
    public static class BuildInfo
    {
        public const string Name = "StackBaseUnitShop";
        public const string Description = "";
        public const string Author = "ToPin060";
        public const string Company = null;
        public const string Version = "1.0";
        public const string DownloadLink = null;
    }

    public class Core : MelonMod
    {
        public override void OnInitializeMelon()
        {
            Melon<Core>.Logger.Msg("Initialized.");
        }

        // DEBUG
        //[HarmonyPatch(typeof(Registry), "AddToItemDictionary")]
        //public static class Registry_AddToItemDictionary_Patch
        //{
        //    public static bool Prefix(Registry __instance, ItemRegister reg)
        //    {
        //        Melon<Core>.Logger.Msg("Name: " + reg.Definition.Name + ", Category: " + reg.Definition.Category);
        //        return true;
        //    }
        //}

        // Delivery phone app
        [HarmonyPatch(typeof(ListingEntry), "Initialize")]
        public static class ListingEntry_Initialize_Patch
        {
            public static void Postfix(ListingEntry __instance, ShopListing match)
            {
                // Remove previous Listeners (+1/-1)
                __instance.IncrementButton.onClick.RemoveAllListeners();
                __instance.DecrementButton.onClick.RemoveAllListeners();

                // Set offset value
                int offset = 1;
                if (isStackableOffset(match.Item)) offset = match.Item.StackLimit;

                // Implement the new button behavior
                // Note: Can't use ChangeQuantity because it's private ?
                __instance.IncrementButton.onClick.AddListener(delegate
                {
                    __instance.SetQuantity(__instance.SelectedQuantity + offset, true);
                });
                __instance.DecrementButton.onClick.AddListener(delegate
                {
                    __instance.SetQuantity(__instance.SelectedQuantity - offset, true);
                });
            }
        }

        public static bool isStackableOffset(ItemDefinition item)
        {
            if (item.Category == EItemCategory.Packaging) return true;
            if (item.Category == EItemCategory.Ingredient) return true;
            if (item.Category == EItemCategory.Consumable) return true;
            if (item.Category == EItemCategory.Growing)
            {
                if (!item.Name.Contains("pot") && !item.Name.Equals("Grow Tent"))
                { return true; }
            }
            if (item.Category == EItemCategory.Tools && item.Name.Equals("Trash Bag"))
            { return true; }

            // Note: Don't realy understand why this item is not tag as Product
            if (item.Category == EItemCategory.Product && item.Name.Equals("Speed Grow"))
            { return true; }

            return false;
        }
    }
}