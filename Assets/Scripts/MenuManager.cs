using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuCanvas, selectCharCanvas;

    private int state;
    private void Start()
    {
        ShowMainMenu();
    }
    public void GoToCharacterSelection()
    {
        state = 2;
        mainMenuCanvas.SetActive(false);
        selectCharCanvas.SetActive(true);
    }

    public void ShowMainMenu()
    {
        state = 1;
        mainMenuCanvas.SetActive(true);
        selectCharCanvas.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    private void Update()
    {
        if(state == 2) // Character select
        {

        }
    }
}
