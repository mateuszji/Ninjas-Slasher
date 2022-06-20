using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;
    public static GameManager Instance => _instance;
    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }

    #endregion
    [SerializeField]
    private GameObject gameOverCanvas;
    [SerializeField]
    private TMP_Text playerFailedInfo;

    [HideInInspector]
    public bool gameStarted = false;

    private void Start()
    {
        gameStarted = true;
    }
    public void GameOver(Player.Players playerType)
    {
        Time.timeScale = 0;
        gameStarted = false;
        gameOverCanvas.SetActive(true);

        if (playerType == Player.Players.Arrows)
            playerFailedInfo.text = "Player (Arrows) died!";
        if (playerType == Player.Players.WSAD)
            playerFailedInfo.text = "Player (WSAD) died!";
    }
}
