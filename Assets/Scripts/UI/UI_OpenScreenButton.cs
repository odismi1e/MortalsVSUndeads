using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_OpenScreenButton : MonoBehaviour
{
    [SerializeField] private ScreenName screenName;
    private Button _openingButton;

    void Awake()
    {
        _openingButton = this.GetComponent<Button>();
    }
    void OnEnable()
    {
        if (_openingButton) { _openingButton.onClick.AddListener(OnClickEvent); }
    }
    void OnDisable()
    {
        if (_openingButton) { _openingButton.onClick.RemoveListener(OnClickEvent); }
    }
    private void OnClickEvent()
    {
        UI_Controller.Instance.SetScreenActive(screenName);
    }
}
