using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    #region Singleton

    private static ConfigManager _instance;
    public static ConfigManager Instance => _instance;
    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
    }

    #endregion

    [HideInInspector]
    public int skinNinjaWASD, skinNinjaArrows;
    [HideInInspector]
    public bool skinsSelected;
}
