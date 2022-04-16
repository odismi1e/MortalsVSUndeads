using UnityEngine;
using UnityEngine.UI;

public class SetLocalizationLanguage : MonoBehaviour
{
    [SerializeField] private Language language;
    private Button _button;

    void Awake()
    {
        _button = this.GetComponent<Button>();
    }
    void OnEnable()
    {
        if (_button) { _button.onClick.AddListener(OnClickEvent); }
    }
    void OnDisable()
    {
        if (_button) { _button.onClick.RemoveListener(OnClickEvent); }
    }
    private void OnClickEvent()
    {
        Localization.Instance.SetLocalizationLanguage(language);
    }
}
