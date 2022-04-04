using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CloseWindowButton : MonoBehaviour
{
    [SerializeField] private WindowName windowName;
    private Button _closingButton;

    void Awake()
    {
        _closingButton = this.GetComponent<Button>();
        
    }
    void OnEnable()
    {
        if (_closingButton) { _closingButton.onClick.AddListener(OnClickEvent); }
    }
    void OnDisable()
    {
        if (_closingButton) { _closingButton.onClick.RemoveListener(OnClickEvent); }
    }
    private void OnClickEvent()
    {
        UI_Controller.Instance.SetWindowInactive(windowName);
    }
}
