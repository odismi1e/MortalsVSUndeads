using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UI_Controller : MonoBehaviour
{
    public static UI_Controller Instance { get => _instance; private set => _instance = value; }
    private static UI_Controller _instance;

    private UI_Screen[] _screens;
    private UI_Window[] _windows;

    private List<GameObject> _instantiatedScreens = new List<GameObject>();
    private List<GameObject> _instantiatedWindows = new List<GameObject>();

    [SerializeField] private GameObject _currentScreen;
    private GameObject _currentWindow;
    private GameObject _previousWindow;
    private string _cloneString = "(Clone)";

    

    void Awake() => InitializeSingleton();
    void Start()
    {
        _screens = Resources.LoadAll<UI_Screen>("UI/Screens");
        _windows = Resources.LoadAll<UI_Window>("UI/Windows");
        InstantiateAllScreensAndWindows();
    }
    public void SetScreenActive(ScreenName name)
    {
        if (_currentScreen)
        {
            _currentScreen.SetActive(false);
            _currentScreen = _instantiatedScreens.Find(element => element.name == name.ToString() + _cloneString);
            _currentScreen.SetActive(true);
        }
        else
        {
            _currentScreen = _instantiatedScreens.Find(element => element.name == name.ToString() + _cloneString);
            _currentScreen.SetActive(true);
        }
    }

    public void SetWindowActive(WindowName name)
    {
        _currentWindow = _instantiatedWindows.Find(element => element.name == name.ToString() + _cloneString);
        _currentWindow.SetActive(true);
    }

    public void SetWindowInactive(WindowName name, GameObject go)
    {
        if (name == WindowName.Current)
        {
            go.GetComponentInParent<UI_Window>().gameObject.SetActive(false);
        }
        else
        {
            _currentWindow = _instantiatedWindows.Find(element => element.name == name.ToString() + _cloneString);
            _currentWindow.SetActive(false);
        }
    }

    public void CloseAll()
    {
        foreach (var element in _instantiatedScreens)
        {
            element.SetActive(false);
        }
        foreach (var element in _instantiatedWindows)
        {
            element.SetActive(false);
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
    private void InstantiateAllScreensAndWindows()
    {
        foreach (var element in _screens)
        {
            var temp = Instantiate(element, this.gameObject.transform);
            _instantiatedScreens.Add(temp.gameObject);
            temp.gameObject.SetActive(false);
        }
        foreach (var element in _windows)
        {
            var temp = Instantiate(element, this.gameObject.transform);
            _instantiatedWindows.Add(temp.gameObject);
            temp.gameObject.SetActive(false);
        }

        _screens = null;
        _windows = null;
    }
}
