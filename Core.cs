using MelonLoader;
using HarmonyLib;
using ScheduleOne.UI.Phone.Delivery;
using ScheduleOne.UI.Shop;
using CartEntry = ScheduleOne.UI.Shop.CartEntry;
using StackBaseUnitShop.UI;

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
        public const string Version = "2.0";
        public const string DownloadLink = null;
    }

    public class Core : MelonMod
    {
        public override void OnInitializeMelon()
        {
            Melon<Core>.Logger.Msg("Initialized.");
        }

        // Patching Shop UI
        [HarmonyPatch(typeof(ShopInterface), "Awake")]
        public static class ShopInterface_Awake_Patch
        {
            public static void Postfix(ShopInterface __instance)
            {
                UILogic.AddToogleSLButtonShops(__instance);
            }
        }

        // Patching Delivery UI
        [HarmonyPatch(typeof(DeliveryApp), "Start")]
        public static class DeliveryApp_Start_Patch
        {
            public static void Postfix(DeliveryApp __instance)
            {
                UILogic.AddToogleSLButtonDelivery(__instance);
            }
        }

        // Patching Shops Logic
        [HarmonyPatch(typeof(ShopInterface), "ListingClicked")]
        public static class ShopInterface_ListingClicked_Patch
        {
            public static bool Prefix(ShopInterface __instance, ListingUI listingUI)
            {
                if (listingUI.Listing.Item.IsPurchasable && listingUI.CanAddToCart())
                {
                    int quantity = 1;
                    if (__instance.AmountSelector.IsOpen)
                    {
                        quantity = __instance.AmountSelector.SelectedAmount;
                    }
                    else if (StackLimitLogic.UseSL && StackLimitLogic.isConsumableItem(listingUI.Listing.Item))
                    {
                        quantity = listingUI.Listing.Item.StackLimit;
                    }

                    __instance.Cart.AddItem(listingUI.Listing, quantity);
                    __instance.AddItemSound.Play();
                }
                return false;
            }
        }

        [HarmonyPatch(typeof(CartEntry), "Initialize")]
        public static class CartEntry_Initialize_Patch
        {
            public static void Postfix(CartEntry __instance, Cart cart, ShopListing listing, int quantity)
            {
                // Remove previous Listeners (+1/-1)
                __instance.IncrementButton.onClick.RemoveAllListeners();
                __instance.DecrementButton.onClick.RemoveAllListeners();

                // Implement the new button behavior
                __instance.IncrementButton.onClick.AddListener(delegate
                {
                    StackLimitLogic.onAddRemCartItem(true, __instance, listing);
                });
                __instance.DecrementButton.onClick.AddListener(delegate
                {
                    StackLimitLogic.onAddRemCartItem(false, __instance, listing);
                });
            }
        }

        // Patching Delivery Logic
        [HarmonyPatch(typeof(ListingEntry), "Initialize")]
        public static class ListingEntry_Initialize_Patch
        {
            public static void Postfix(ListingEntry __instance, ShopListing match)
            {
                // Remove previous listeners
                __instance.IncrementButton.onClick.RemoveAllListeners();
                __instance.DecrementButton.onClick.RemoveAllListeners();

                __instance.IncrementButton.onClick.AddListener(() =>
                {
                    StackLimitLogic.onAddRemDeliveryItem(true, __instance, match.Item);
                });
                __instance.DecrementButton.onClick.AddListener(() =>
                {
                    StackLimitLogic.onAddRemDeliveryItem(false, __instance, match.Item);
                });
            }
        }
    }
}