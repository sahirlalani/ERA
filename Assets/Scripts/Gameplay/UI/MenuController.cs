using System.Collections;
using System.Collections.Generic;
#if Unity_Editor
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MenuController : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject OptionsMenu;
    public GameObject PauseMenuUI;
    public GameObject InGameUI;
    public GameObject VictoryGameOverUI, DefeatGameOverUI;
    public GameObject ControlsUI;
    public AudioManager audioManager;
    public AudioSource audioSource;
    public Slider VolumeSlider;

    public PlayerController player;

    public static bool WinTriggered = false;

    public static string PreviousUI;

    public void Start()
    {
        DontDestroyOnLoad(this);
        Time.timeScale = 1f;
        audioManager.PlayMusic("BG_Music");

        //AudioManager audioManager = GetActiveScene.GetComponent<AudioManager>();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("World");
        //audioManager.PlayMusic("BG_Music");
        //PreviousUI = "GrassLands";
        Time.timeScale = 1f;
    }

    public void Options()
    {
        OptionsMenu.SetActive(true);
        MainMenuUI.SetActive(false);
    }

    public void ControlsPage()
    {
        ControlsUI.SetActive(true);
        MainMenuUI.SetActive(false);
    }

    public void ControlVolume()
    {
        audioSource.volume = VolumeSlider.value;
    }
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        InGameUI.SetActive(true);
        Time.timeScale = 1f;
        //GameIsPaused = false;
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        InGameUI.SetActive(false);
        Time.timeScale = 0f;
        //GameIsPaused = true;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        //PreviousUI = "MainMenu";
        Time.timeScale = 0f;
    }
    public void ReturnToPreviousUI()
    {
        if (PreviousUI == null)
        {
            PreviousUI = "MainMenu";
        }
        if (PreviousUI == "World")
        {
            PauseMenuUI.SetActive(true);
            OptionsMenu.SetActive(false);
        }
        if (PreviousUI == "MainMenu")
        {
            MainMenuUI.SetActive(true);
            OptionsMenu.SetActive(false);
        }
    }

    public void WinGameOver()
    {
        Time.timeScale = 0f;
        VictoryGameOverUI.SetActive(true);
    }

    public void LoseGameOver()
    {
        Time.timeScale = 0f;
        DefeatGameOverUI.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");

#if Unity_Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0f)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.C) && ControlsUI.activeSelf == true)
        {
            //ReturnToPreviousUI();
            ControlsUI.SetActive(false);
            ReturnToPreviousUI();
        }

        if (player.CurrentHp > 0 && WinTriggered == true)
        {
            WinGameOver();
        }

        if (player.CurrentHp <= 0)
        {
            LoseGameOver();
        }

        PreviousUI = SceneManager.GetActiveScene().name;
        //Debug.Log(PreviousUI);

    }
}
