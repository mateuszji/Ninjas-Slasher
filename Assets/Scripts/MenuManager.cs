using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuCanvas, selectCharCanvas;

    [SerializeField]
    private Sprite[] ninjaSkins;

    [SerializeField]
    private Image ninjaWASD, ninjaArrows;

    private int ninjaWASDId, ninjaArrowsId;

    private int state;
    private void Start()
    {
        ninjaWASDId = Random.Range(0, ninjaSkins.Length);
        ninjaArrowsId = Random.Range(0, ninjaSkins.Length);
        ninjaWASD.sprite = ninjaSkins[ninjaWASDId];
        ninjaArrows.sprite = ninjaSkins[ninjaArrowsId];

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
        ConfigManager.Instance.skinsSelected = true;
        ConfigManager.Instance.skinNinjaWASD = ninjaWASDId;
        ConfigManager.Instance.skinNinjaArrows = ninjaArrowsId;
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
            if (Input.GetKeyDown(KeyCode.D))
            {
                ninjaWASDId++;
                if (ninjaWASDId >= ninjaSkins.Length)
                    ninjaWASDId = 0;
                ninjaWASD.sprite = ninjaSkins[ninjaWASDId];
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                ninjaWASDId--;
                if (ninjaWASDId < 0)
                    ninjaWASDId = ninjaSkins.Length - 1;
                ninjaWASD.sprite = ninjaSkins[ninjaWASDId];
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ninjaArrowsId++;
                if (ninjaArrowsId >= ninjaSkins.Length)
                    ninjaArrowsId = 0;
                ninjaArrows.sprite = ninjaSkins[ninjaArrowsId];
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ninjaArrowsId--;
                if (ninjaArrowsId < 0)
                    ninjaArrowsId = ninjaSkins.Length - 1;
                ninjaArrows.sprite = ninjaSkins[ninjaArrowsId];
            }
        }
    }
}
