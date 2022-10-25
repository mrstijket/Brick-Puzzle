using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject pauseMenuUI;

    public void StopAndGo()
    {
        if (isGamePaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
        FindObjectOfType<BlockLogic>().movable = false;
        FindObjectOfType<AudioSource>().Pause();
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
        FindObjectOfType<BlockLogic>().movable = true;
        FindObjectOfType<AudioSource>().Play();
    }
    public void Menu()
    {
        pauseMenuUI.SetActive(false);
        SceneManager.LoadScene("Welcome");
        Time.timeScale = 1f;
        isGamePaused = false;
        FindObjectOfType<BlockLogic>().movable = true;
        FindObjectOfType<AudioSource>().Play(); 
    }
    public void QuitFromPause()
    {
        Application.Quit();
    }
}