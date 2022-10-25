using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    public void LoadHighScore()
    {
        SceneManager.LoadScene("HighScore");
    }
    public void LoadLearnScene()
    {
        SceneManager.LoadScene("HowToPlay");
    }
    public void LoadCustomScene()
    {
        SceneManager.LoadScene("Customize");
    }
    public void ShowMenu()
    {
        SceneManager.LoadScene("Welcome");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
        GameLogic.score = 0;
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
