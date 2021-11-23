using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceCounter : MonoBehaviour
{
    public static ResourceCounter Instance;

    public int Resources;
    public TMP_Text RecourcesText;

    private void Awake()
    {
        if(Instance!=null && Instance!=this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
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
