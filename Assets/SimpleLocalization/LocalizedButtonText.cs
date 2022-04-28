using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.SimpleLocalization
{
	/// <summary>
	/// Localize text component.
	/// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedButtonText : MonoBehaviour
    {
        public LocalizationButtonKey localizationButtonKey;
        private string listKey = "Buttons";

        public void Start()
        {
            Localize();
            LocalizationManager.LocalizationChanged += Localize;
        }

        public void OnDestroy()
        {
            LocalizationManager.LocalizationChanged -= Localize;
        }

        private void Localize()
        {
            string localizationKey = LocalizationManager.JoinStringsWithDot(listKey, localizationButtonKey.ToString());
            GetComponent<TextMeshProUGUI>().text = LocalizationManager.Localize(localizationKey);
        }
    }
}