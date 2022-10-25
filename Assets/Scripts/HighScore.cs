using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Text highScore;
    public Text lastScore;
    void Start()
    {
        if (highScore)
            highScore.text = PlayerPrefs.GetInt("highScore", 0).ToString();
        if (lastScore)
            lastScore.text = PlayerPrefs.GetInt("lastScore").ToString();
         
    }
}
