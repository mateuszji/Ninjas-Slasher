using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    private TMP_Text playerFailedInfo, scoreTMP;

    [HideInInspector]
    public int score;

    [HideInInspector]
    public bool gameStarted = false;

    private void Start()
    {
        score = 0;
        Time.timeScale = 1;
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

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void AddScore(int points)
    {
        score += points;
        scoreTMP.text = score.ToString();
    }

    private void Update()
    {
        if(!gameStarted)
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.RightShift))
            {
                PlayAgain();
            }
        }
    }
}
