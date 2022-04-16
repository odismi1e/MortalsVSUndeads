using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.SimpleLocalization
{
    /// <summary>
    /// Localize text component.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ShowData : MonoBehaviour
    {
        public DataPrimaryKey PrimaryKey;
        public DataFieldKey FieldKey;

        private string Key;
        public void Start()
        {
            Key = JoinStringsWithDot(PrimaryKey, FieldKey);
            Show();
            DataManager.DataChanged += Show;
        }

        public void OnDestroy()
        {
            DataManager.DataChanged -= Show;
        }
        private string JoinStringsWithDot(DataPrimaryKey primaryKey, DataFieldKey fieldKey)
        {
            return string.Join(".", primaryKey.ToString(), fieldKey.ToString());
        }
        private void Show()
        {
            GetComponent<TextMeshProUGUI>().text = $"{Key}" + " = " + $"{DataManager.Parse(Key)}";
        }
    }
}
