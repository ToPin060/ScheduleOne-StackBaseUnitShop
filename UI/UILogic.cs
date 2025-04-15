using FishNet.Utility.Extension;
using FluffyUnderware.DevTools.Extensions;
using GameKit.Utilities;
using MelonLoader;
using ScheduleOne.UI.Phone.Delivery;
using ScheduleOne.UI.Shop;
using StackBaseUnitShop.UI.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static MelonLoader.MelonLogger;
using static UnityEngine.UIElements.UxmlAttributeDescription;

namespace StackBaseUnitShop.UI
{
    public static class UILogic
    {
        public static void AddToogleSLButtonShops(ShopInterface __instance)
        {
            // Copy all button (from category)
            var topBar = __instance.gameObject.transform.GetChild(0).GetChild(1).gameObject;
            var allButon = topBar.transform.GetChild(3).GetChild(0).gameObject;
            var toogleSL = UIUtils.CreateButtonFromPrefabTM("toogleSL", "SL: On", allButon);

            // Cleaning
            toogleSL.GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Destroy(toogleSL.GetComponent<CategoryButton>());

            // Position
            toogleSL.transform.SetParent(topBar.transform);
            toogleSL.transform.localPosition = new Vector3(489, 17.5f, 0);
            toogleSL.transform.SetScale(new Vector3(1, 1, 1));

            // Specify
            toogleSL.GetComponent<Button>().onClick.AddListener(() =>
            {
                var button = toogleSL.GetComponent<Button>();
                StackLimitLogic.ToogleSLMod(ref button);              
            });
        }

        public static void AddToogleSLButtonDelivery(DeliveryApp __instance)
        {
            // Copy + button (from ListEntry)
            GameObject deliveryApp = __instance.gameObject;
            GameObject topBar = deliveryApp.transform.GetChild(0).GetChild(1).gameObject;
            GameObject incrementButton = deliveryApp.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetChild(4).gameObject;
            var toogleSL = UIUtils.CreateButtonFromPrefab("toogleSL", "SL: On", incrementButton);
            toogleSL.GetComponentInChildren<Text>().fontSize = 42;
            ((RectTransform)toogleSL.GetComponent<Button>().transform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 60);

            // Cleaning
            toogleSL.GetComponent<Button>().onClick.RemoveAllListeners();

            // Position
            toogleSL.transform.SetParent(topBar.transform);
            toogleSL.transform.SetLocalPositionAndRotation(new Vector3(543.9365f, -1.2f, 0), new Quaternion(0, 0, 0, 0));
            toogleSL.transform.SetScale(new Vector3(1, 1, 1));

            // Specify
            toogleSL.GetComponent<Button>().onClick.AddListener(() =>
            {
                var button = toogleSL.GetComponent<Button>();
                StackLimitLogic.ToogleSLMod(ref button);
            });
        }
    }
}
