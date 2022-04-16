using Assets.SimpleLocalization;
using UnityEngine;

public class Localization : MonoBehaviour
{
    public static Localization Instance { get => _instance; private set => _instance = value; }
    private static Localization _instance;

    [SerializeField] private Object SaveFolder;


    void Awake()
    {
        InitializeSingleton();

        LocalizationManager.Read(SaveFolder.name);

        SetLocalizationLanguage(Application.systemLanguage.ToString());
    }
    public void SetLocalizationLanguage(Language value = Language.English)
    {
        switch (value)
        {
            case Language.German:
                LocalizationManager.Language = SystemLanguage.German.ToString();
                break;
            case Language.Russian:
                LocalizationManager.Language = SystemLanguage.Russian.ToString();
                break;
            case Language.English:
                LocalizationManager.Language = SystemLanguage.English.ToString();
                break;
            default:
                LocalizationManager.Language = SystemLanguage.English.ToString();
                break;
        }
    }
    public void SetLocalizationLanguage(string value)
    {
        switch (value)
        {
            case "German":
                LocalizationManager.Language = SystemLanguage.German.ToString();
                break;
            case "Russian":
                LocalizationManager.Language = SystemLanguage.Russian.ToString();
                break;
            case "English":
                LocalizationManager.Language = SystemLanguage.English.ToString();
                break;
            default:
                LocalizationManager.Language = SystemLanguage.English.ToString();
                break;
        }
    }
    private void InitializeSingleton()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
