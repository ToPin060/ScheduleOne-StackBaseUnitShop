using UnityEngine.UI;
#if MONO
using ScheduleOne.ItemFramework;
using ScheduleOne.UI.Phone.Delivery;
using ScheduleOne.UI.Shop;
using TMPro;

#else
using Il2CppScheduleOne.ItemFramework;
using Il2CppScheduleOne.UI.Phone.Delivery;
using Il2CppScheduleOne.UI.Shop;
using Il2CppTMPro;
#endif

namespace StackBaseUnitShop
{

    public class StackLimitLogic
    {
        public static bool UseSL = true;
        public static void ToogleSLMod(ref Button button)
        {
            UseSL = !UseSL;
            if (UseSL) button.GetComponentInChildren<TextMeshProUGUI>().SetText("SL: On");
            else button.GetComponentInChildren<TextMeshProUGUI>().SetText("SL: Off");
        }
        public static void onAddRemCartItem(bool isAdd, CartEntry __instance, ShopListing listing)
        {
            var offset = 1;
            if (UseSL && isConsumableItem(__instance.Listing.Item)) offset = __instance.Listing.Item.StackLimit;
            if (isAdd) __instance.Cart.AddItem(__instance.Listing, offset);
            else __instance.Cart.RemoveItem(__instance.Listing, offset);
        }
        public static void onAddRemDeliveryItem(bool isAdd, ListingEntry __instance, ItemDefinition item)
        {
            var offset = 1;
            if (UseSL && isConsumableItem(item)) offset = item.StackLimit;
            if (!isAdd) offset = offset * -1;
            __instance.SetQuantity(__instance.SelectedQuantity + offset, true);
        }
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
            if (item.Category == EItemCategory.Product && item.Name.Equals("Speed Grow"))
            { return true; }
            return false;
        }
    }
}
