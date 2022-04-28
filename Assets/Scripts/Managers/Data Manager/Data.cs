using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static Data Instance { get => _instance; private set => _instance = value; }
    private static Data _instance;

    [SerializeField] private Object SaveFolder;


    void Awake()
    {
        InitializeSingleton();

        DataManager.Read(SaveFolder.name);
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
