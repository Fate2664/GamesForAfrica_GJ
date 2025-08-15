using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using DG.Tweening;

public class PauseScreen : MonoBehaviour
{
    //This script manages the pause screen functionality

    public Button restartButton;
    public Button continueButton;
    public Button mainMenuButton;
    public GameObject mainUI;

    [SerializeField] private AudioSource engineSource;
    [SerializeField] private Volume _volume;
    [Range(-100, 100)]
    [SerializeField] private float _saturation = 0f;
    [Range(-10, 10)]
    [SerializeField] private float _postExposure = 0f;

    private GameObject pauseScreen;
    private bool inPause = false;
    private PauseVolume pauseVolume;

    void Start()
    {
        pauseScreen = this.gameObject;
        pauseScreen.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
        continueButton.onClick.AddListener(ContinueGame);
        mainMenuButton.onClick.AddListener(LoadMainMenu);
        pauseVolume = new PauseVolume(_volume, _saturation, _postExposure);
    }

    private void ShowPauseScreen()
    {
        inPause = true;
        engineSource.Pause();
        pauseScreen.SetActive(true);
        mainUI.SetActive(false);
        pauseVolume.ApplyPauseEffect();
        Time.timeScale = 0f;

    }

    public void ActivatePauseScreen()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!inPause)
            {
                ShowPauseScreen();
            }else ContinueGame();
        }

    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        pauseVolume.RemovePauseEffect();
        SceneManager.LoadScene("Level 1");
    }

    private void ContinueGame()
    {
        Time.timeScale = 1f;
        pauseVolume.RemovePauseEffect();
        mainUI.SetActive(true);
        pauseScreen.SetActive(false);
        engineSource.UnPause();
        inPause = false;
    }

    private void LoadMainMenu()
    {
        Time.timeScale = 1f;
        pauseVolume.RemovePauseEffect();
        AudioManager.Instance?.StopSFX("GameplayMusic"); //Stop the gameplay music
        AudioManager.Instance?.PlaySFX("MenuMusic"); //Play the main menu music
        SceneManager.LoadScene("MainMenu");
    }


}
