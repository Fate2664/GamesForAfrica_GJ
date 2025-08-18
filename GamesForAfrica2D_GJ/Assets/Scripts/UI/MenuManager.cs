using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    private void Start()
    {
        if (!AudioManager.Instance.IsPlaying("MenuMusic"))
        {
            AudioManager.Instance.PlaySFX("MenuMusic");
        }   
    }

    //This script manages the main menu
    public void StartGame()
    {
        AudioManager.Instance?.StopSFX("MenuMusic");

        SceneManager.LoadScene("MainLevel");
    }
    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LoadSettingsMenu()
    {
        SceneManager.LoadScene("Settings");
    }


}
