using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_OpenWindowButton : MonoBehaviour
{
    [SerializeField] private WindowName windowName;
    private Button _openingButton;

    void Start()
    {
        _openingButton = this.GetComponent<Button>();
    }
    void OnEnable()
    {
        _openingButton.onClick.AddListener(OnClickEvent);
    }
    void OnDisable()
    {
        _openingButton.onClick.RemoveListener(OnClickEvent);
    }
    private void OnClickEvent()
    {
        UI_Controller.Instance.SetWindowActive(windowName);
    }
}
