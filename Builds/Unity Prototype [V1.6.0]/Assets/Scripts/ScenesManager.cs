using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager SMInstance;

    public Button playButton;
    public Button settingsButton;
    public Button instructionsButton;
    public Button quitButton;

    public Canvas quitCanvas;
    public Image quitMenu;
    public Button quitYes;
    public Button quitNo;

    public Canvas settingsCanvas;
    public Image settingsMenu;
    public Slider volumeControl;
    public AudioSource Sound;
    public Button settingsDone;

    public Button instructionsForward;
    public Button instructionsBackward;
    public Button instructionsMainMenu;

    public Button restartGame;
    public Button goHome;

    //public Canvas testcanvas;

    public bool isQuitMenu = false;
    public bool isSettingsMenu = false;


    #region sceneManager singleton
    void Awake()
    {
        //DontDestroyOnLoad(gameObject);

        if (SMInstance == null)
            SMInstance = this;
        else if (SMInstance != null)
            Destroy(gameObject);
    }
    #endregion


    void Start()
    {
        playButton = playButton.GetComponent<Button>();
        settingsButton = settingsButton.GetComponent<Button>();
        instructionsButton = instructionsButton.GetComponent<Button>();
        quitButton = quitButton.GetComponent<Button>();

        quitCanvas = quitCanvas.GetComponent<Canvas>();
        quitMenu = quitMenu.GetComponent<Image>();
        quitYes = quitYes.GetComponent<Button>();
        quitNo = quitNo.GetComponent<Button>();


        settingsCanvas = settingsCanvas.GetComponent<Canvas>();
        settingsMenu = settingsMenu.GetComponent<Image>();
        volumeControl = volumeControl.GetComponent<Slider>();
        settingsDone = settingsDone.GetComponent<Button>();
        volumeControl.value = 0.5f;


        instructionsForward = instructionsForward.GetComponent<Button>();
        instructionsBackward = instructionsBackward.GetComponent<Button>();
        instructionsMainMenu = instructionsMainMenu.GetComponent<Button>();

        restartGame = restartGame.GetComponent<Button>();
        goHome = goHome.GetComponent<Button>();

        quitCanvas.enabled = false;
        isQuitMenu = true;
        isSettingsMenu = true;
        settingsCanvas.enabled = false;
    }

    void Update()
    {
        Sound.volume = volumeControl.value;
    }

    public void QuitMenu()
    {
        quitCanvas.enabled = true;
        quitMenu.enabled = true;
        quitYes.enabled = true;
        quitNo.enabled = true;

        playButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
        instructionsButton.gameObject.SetActive(false);

        isQuitMenu = true;
    }

    public void HideQuitMenu()
    {
        quitCanvas.enabled = false;
        quitMenu.enabled = false;
        quitYes.enabled = false;
        quitNo.enabled = false;

        playButton.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(true);
        instructionsButton.gameObject.SetActive(true);

        isQuitMenu = false;
    }

    public void QuitMenuToggle()
    {
        if (isQuitMenu)
        {
            HideQuitMenu();
        }

        else if (!isQuitMenu)
        {
            QuitMenu();
        }
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    public void SettingsMenu()
    {
        settingsCanvas.enabled = true;
        settingsMenu.enabled = true;
        volumeControl.enabled = true;
        settingsDone.enabled = true;

        playButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        instructionsButton.gameObject.SetActive(false);

        isSettingsMenu = true;
    }

    public void HideSettingsMenu()
    {
        settingsCanvas.enabled = false;
        settingsMenu.enabled = false;
        volumeControl.enabled = false;
        settingsDone.enabled = false;

        playButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        instructionsButton.gameObject.SetActive(true);

        isSettingsMenu = false;
    }

    public void ToggleSettingsMenu()
    {
        if (isSettingsMenu)
        {
            HideSettingsMenu();
        }

        else if (!isSettingsMenu)
        {
            SettingsMenu();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadInstructions()
    {
        SceneManager.LoadScene(2);
    }

    public void NextInstructions()
    {
        SceneManager.LoadScene(3);
    }

    public void PreviousInstructions()
    {
        SceneManager.LoadScene(2);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}