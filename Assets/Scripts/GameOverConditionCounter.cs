using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverConditionCounter : MonoBehaviour
{
    private TextMeshProUGUI _TMP;
    private int _numberOfPassedUnits = 0;
    void Awake() => _TMP = this.GetComponentInChildren<TextMeshProUGUI>();
    void Start()
    {
        Refresh();
        EventsManager.OnEnemyBorderPassed.AddListener(UpdateCounter);
        EventsManager.OnEnemyBorderPassed.AddListener(Refresh);
        EventsManager.OnEnemyBorderPassed.AddListener(CheckCondition);
    }
    void UpdateCounter() => _numberOfPassedUnits++;
    void Refresh() => _TMP.text = $"{_numberOfPassedUnits}/{LevelController.Instance.NumberOfPassedUnitsBeforeDefeat}";
    void CheckCondition()
    {
        if (_numberOfPassedUnits == LevelController.Instance.NumberOfPassedUnitsBeforeDefeat)
        {
            UI_Controller.Instance.SetWindowActive(WindowName.Lose);
        }
    }
}
