using ScheduleOne.ItemFramework;
using ScheduleOne.UI.Phone.Delivery;
using ScheduleOne.UI.Shop;
using TMPro;
using UnityEngine.UI;
using static MelonLoader.MelonLogger;

namespace StackBaseUnitShop
{
    internal class StackLimitLogic
    {
        public static bool UseSL = true;

        public static void ToogleSLMod(ref Button button)
        {
            UseSL = !UseSL;

            if (StackLimitLogic.UseSL) button.GetComponentInChildren<TextMeshProUGUI>().SetText("SL: On");
            else button.GetComponentInChildren<TextMeshProUGUI>().SetText("SL: Off");
        }

        public static void onAddRemCartItem(bool isAdd, CartEntry __instance, ShopListing listing)
        {
            var offset = 1;
            if (UseSL && isConsumableItem(__instance.Listing.Item)) offset = __instance.Listing.Item.StackLimit;

            if (isAdd) __instance.Cart.AddItem(__instance.Listing, offset);
            else __instance.Cart.RemoveItem(__instance.Listing, offset);
        }

        public static void onAddRemDeliveryItem(bool isAdd, ListingEntry __instance,  ItemDefinition item)
        {
            var offset = 1;
            if (UseSL && isConsumableItem(item)) offset = item.StackLimit;

            if (!isAdd) offset = offset * -1;

            __instance.SetQuantity(__instance.SelectedQuantity + offset, true);
        }

        /// <summary>
        /// Returns true if the element is a “consumable” item
        /// Note: The term “consumable” is a personal definition.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool isConsumableItem(ItemDefinition item)
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
