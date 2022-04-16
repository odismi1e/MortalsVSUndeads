using Assets.SimpleLocalization;
using UnityEngine;

public class Localization : MonoBehaviour
{
    public static Localization Instance { get => _instance; private set => _instance = value; }
    private static Localization _instance;

    void Awake()
    {
        InitializeSingleton();

        LocalizationManager.Read();

        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                LocalizationManager.Language = "German";
                break;
            case SystemLanguage.Russian:
                LocalizationManager.Language = "Russian";
                break;
            default:
                LocalizationManager.Language = "English";
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
