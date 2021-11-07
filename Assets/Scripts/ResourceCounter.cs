using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceCounter : MonoBehaviour
{
    public int Resources;
    public TMP_Text RecourcesText;

    private static ResourceCounter _instance;
    public static ResourceCounter Instance { get { return _instance; } }

    private void Awake()
    {
        if(_instance!=null && _instance!=this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void ReceiveResources(int resourceCount)
    {
        Resources += resourceCount;
        RecourcesText.text = Resources.ToString();
    }
    public void WasteOfResources(int resourceCount)
    {
        Resources -= resourceCount;
        RecourcesText.text = Resources.ToString();
    }
}
