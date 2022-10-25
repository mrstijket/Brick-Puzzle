using UnityEngine;
using UnityEngine.UI;

public class CustomSettings : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] float defaultVolume = 0.8f;

    [SerializeField] Slider speedSlider;
    [SerializeField] float defaultSpeed = 1f;

    const string MASTER_VOLUME_KEY = "volume";
    const string DIFFICULTY_KEY = "difficulty";

    const float MIN_VOLUME = 0f;
    const float MAX_VOLUME = 1f;

    const float MIN_DIFFICULTY = 0f;
    const float MAX_DIFFICULTY = 2f;

    void Start()
    {
        speedSlider.value = GetDifficulty();
        volumeSlider.value = GetMasterVolume();
    }
    void Update()
    {
        MusicPlayer musicPlayer = FindObjectOfType<MusicPlayer>();
        if (musicPlayer)
        {
            musicPlayer.SetVolume(volumeSlider.value);
        }
        else
        {
            Debug.LogWarning("There is no music player found!");
        }
    }
    public static void SetMasterVolume (float volume)
    {
        if(volume >= MIN_VOLUME && volume <= MAX_VOLUME)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Volume settings is not in range");
        }
    }
    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }
    public static void SetDifficulty(float difficulty)
    {
        if(difficulty >= MIN_DIFFICULTY && difficulty <= MAX_DIFFICULTY)
        {
            PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
        }
        else
        {
            Debug.LogError("Difficulty settings is not in range");
        }
    }
    public static float GetDifficulty()
    {
        return PlayerPrefs.GetFloat(DIFFICULTY_KEY);
    }
    public void SaveAndExit()
    {
        SetMasterVolume(volumeSlider.value);
        SetDifficulty(speedSlider.value);
        FindObjectOfType<LoadLevel>().ShowMenu();
    }
    public void SetDefaults()
    {
        volumeSlider.value = defaultVolume;
        speedSlider.value = defaultSpeed;
    }
}
