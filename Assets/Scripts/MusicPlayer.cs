using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;
    public static MusicPlayer instance;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = CustomSettings.GetMasterVolume();
        Singelton();
    }
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
    void Singelton()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "GameOver")
        {
            Destroy(instance.gameObject);
        }
        if (instance != null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}