using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class GameLogic : MonoBehaviour
{
    [SerializeField] float slow = 0.9f;
    [SerializeField] float medium = 0.6f;
    [SerializeField] float fast = 0.25f;
    [SerializeField] AudioClip destroySound;
    [SerializeField] GameObject shineObject;
    public static int score = 0;
    public Text scoreText;

    public float dropTime; // 0.25 for fast 0.6 for medium 0.9 for slow
    public float quickDropTime = 0.05f;
    public static int width = 11, height = 22;
    public GameObject[] blocks;
    public Transform[,] grid = new Transform[width, height];// comma in Brackets - that called two dimensional array
    void Start()
    {
        SpawnBlock();
    }
    void Update()
    {
        SetUpGameSpeed();
        scoreText.text = GetScore().ToString();
        PlayerPrefs.SetInt("lastScore", score);
    }
    void SetUpGameSpeed()
    {
        if (CustomSettings.GetDifficulty().ToString() == 0.ToString())
        {
            if (score == 0)
                dropTime = slow;
            else
                dropTime = slow * (1 / Mathf.Sqrt(Mathf.Sqrt(Mathf.Sqrt(Mathf.Sqrt(score))))); //yes there is 4 sqrt there im noob, and its not working properly
        }
        else if (CustomSettings.GetDifficulty().ToString() == 1.ToString())
        {
            if (score == 0)
                dropTime = medium;
            else
                dropTime = medium * (1 / Mathf.Sqrt(Mathf.Sqrt(Mathf.Sqrt(Mathf.Sqrt(score)))));
        }
        else if (CustomSettings.GetDifficulty().ToString() == 2.ToString())
        {
            if (score == 0)
                dropTime = fast;
            else
                dropTime = fast * (1 / Mathf.Sqrt(Mathf.Sqrt(Mathf.Sqrt(Mathf.Sqrt(score)))));
        }
        else
        {
            Debug.LogError("Difficulty Level out of range!");
        }
    }
    public void CheckForLines()
    {
        for (int i = height - 1; i >= 2; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
                GainScore(10);
            }
        }
    }
    bool HasLine(int i)
    {
        for (int j = 1; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;
        }
        return true;
    }
    void DeleteLine(int i)
    {
        for (int j = 1; j < width; j++)
        {
            shineObject.transform.position = new Vector2(shineObject.transform.position.x, i);
            shineObject.SetActive(true);
            AudioSource.PlayClipAtPoint(destroySound, Vector3.zero);
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
            Invoke("DisableShine", 0.05f);
        }
    }
    void DisableShine()
    {
        shineObject.SetActive(false);
    }
    void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 1; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1f, 0);
                }
            }

        }
    }
    public void SpawnBlock()
    {
        Instantiate(blocks[UnityEngine.Random.Range(0, blocks.Length)]);
    }
    int GetScore()
    {
        return score;
    }
    public void GainScore(int scoreValue)
    {
        score += scoreValue;
        if (score > PlayerPrefs.GetInt("highScore", 0))
        {
            PlayerPrefs.SetInt("highScore", score);
        }
    }
}