using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] private Scenes scene;

    private Button _button;

    void Awake()
    {
        _button = this.GetComponent<Button>();
    }
    void OnEnable()
    {
        if (_button) { _button.onClick.AddListener(LoadSceneByName); }
    }
    void OnDisable()
    {
        if (_button) { _button.onClick.RemoveListener(LoadSceneByName); }
    }
    private void LoadSceneByName()
    {
        SceneManager.LoadScene(scene.ToString());
        UI_Controller.Instance.CloseAll();
        if (scene.ToString() == Scenes.Main_Menu.ToString())
        {
            UI_Controller.Instance.SetScreenActive(ScreenName.Start);
        }
    }
}
