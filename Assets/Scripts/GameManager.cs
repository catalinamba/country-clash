using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI scoreText;

    private Dictionary<string, int> scores = new Dictionary<string, int>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Inicializamos solo Player y Bot
        scores["Player"] = 0;
        scores["Bot"] = 0;

        UpdateScoreText();
    }

    public void AddScore(string key, int points)
    {
        if (scores.ContainsKey(key))
        {
            scores[key] += points;
            Debug.Log(key + " tiene " + scores[key] + " puntos");
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Player: " + scores["Player"] + " | Bot: " + scores["Bot"];
    }


    public Dictionary<string, int> GetScores()
    {
        return scores;
    }
}