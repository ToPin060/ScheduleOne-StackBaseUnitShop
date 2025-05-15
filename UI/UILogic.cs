using UnityEngine;
using UnityEngine.UI;
#if MONO
using GameKit.Utilities;
using ScheduleOne.UI.Phone.Delivery;
using ScheduleOne.UI.Shop;
using StackBaseUnitShop.UI.UI;

#else
using Il2CppGameKit.Utilities;
using Il2CppScheduleOne.UI.Phone.Delivery;
using Il2CppScheduleOne.UI.Shop;
using StackBaseUnitShop.UI.UI;
#endif

namespace StackBaseUnitShop.UI
{
    public class UILogic
    {
        public static void AddToogleSLButtonShops(ShopInterface __instance)
        {
            var topBar = __instance.gameObject.transform.GetChild(0).GetChild(1).gameObject;
            var allButon = topBar.transform.GetChild(3).GetChild(0).gameObject;
            var toogleSL = UIUtils.CreateButtonFromPrefabTM("toogleSL", "SL: On", allButon);

            var button = toogleSL.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            GameObject.Destroy(toogleSL.GetComponent<CategoryButton>());

            toogleSL.transform.SetParent(topBar.transform);
            toogleSL.transform.localPosition = new Vector3(489, 17.5f, 0);
            toogleSL.transform.SetScale(new Vector3(1, 1, 1));
#if MONO
            toogleSL.GetComponent<Button>().onClick.AddListener(() =>
            {
                var button = toogleSL.GetComponent<Button>();
                StackLimitLogic.ToogleSLMod(ref button);
            });
#else
            button.onClick.AddListener(new Action(() =>
            {
                StackLimitLogic.ToogleSLMod(ref button);
            }));
#endif
        }

        public static void AddToogleSLButtonDelivery(DeliveryApp __instance)
        {
            GameObject deliveryApp = __instance.gameObject;
            GameObject topBar = deliveryApp.transform.GetChild(0).GetChild(1).gameObject;
            GameObject incrementButton = deliveryApp.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0)
                                                  .GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetChild(4).gameObject;

            var toogleSL = UIUtils.CreateButtonFromPrefab("toogleSL", "SL: On", incrementButton);
            toogleSL.GetComponentInChildren<Text>().fontSize = 42;
#if MONO
            ((RectTransform)toogleSL.GetComponent<Button>().transform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 60);
#else
            (toogleSL.GetComponent<Button>().transform.Cast<RectTransform>()).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 60);
#endif
            
            var button = toogleSL.GetComponent<Button>();
            button.onClick.RemoveAllListeners();

            toogleSL.transform.SetParent(topBar.transform);
            toogleSL.transform.SetLocalPositionAndRotation(new Vector3(543.9365f, -1.2f, 0), new Quaternion(0, 0, 0, 0));
            toogleSL.transform.SetScale(new Vector3(1, 1, 1));

#if MONO
            toogleSL.GetComponent<Button>().onClick.AddListener(() =>
            {
                var button = toogleSL.GetComponent<Button>();
                StackLimitLogic.ToogleSLMod(ref button);
            });
#else
            button.onClick.AddListener(new Action(() =>
            {
                StackLimitLogic.ToogleSLMod(ref button);
            }));
#endif
        }
    }
}
