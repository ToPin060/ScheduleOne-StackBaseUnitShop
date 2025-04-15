using TMPro;
using UnityEngine.Events;
using UnityEngine;
using Object = UnityEngine.Object;
using UnityEngine.UI;

namespace StackBaseUnitShop.UI.UI
{
    public static class UIUtils
    {
        public static GameObject CreateButtonFromPrefab(string name, string text, GameObject prefab,
            Color? fontColor = null, UnityAction onClick = null)
        {
            var button = Object.Instantiate(prefab);
            button.name = name + "Button";

            var textComponent = button.transform.GetComponentInChildren<Text>();
            textComponent.text = text;

            if (fontColor.HasValue)
                textComponent.color = fontColor.Value;

            if (onClick != null)
            {
                var buttonComponent = button.GetComponent<Button>();
                buttonComponent.onClick.RemoveAllListeners();
                buttonComponent.onClick.AddListener(onClick);
            }

            return button;
        }

        public static GameObject CreateButtonFromPrefabTM(string name, string text, GameObject prefab,
            Color? fontColor = null, UnityAction onClick = null)
        {
            var button = Object.Instantiate(prefab);
            button.name = name + "Button";

            var textComponent = button.transform.GetComponentInChildren<TextMeshProUGUI>();
            textComponent.text = text;

            if (fontColor.HasValue)
                textComponent.color = fontColor.Value;

            if (onClick != null)
            {
                var buttonComponent = button.GetComponent<Button>();
                buttonComponent.onClick.RemoveAllListeners();
                buttonComponent.onClick.AddListener(onClick);
            }

            return button;
        }
    }
}
