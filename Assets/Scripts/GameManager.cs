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
            DontDestroyOnLoad(gameObject); //para que no se pierda la puntuacion al cambiar escena
        }
        else
        {
            Destroy(gameObject);
        }

        scores["Guinea Ecuatorial"] = 0;
        scores["Francia"] = 0;
        scores["Japon"] = 0;

        UpdateScoreText();
    }

    public void AddScore(string country, int points)
    {
        if (scores.ContainsKey(country))
        {
            scores[country] += points;
            Debug.Log(country + " tiene " + scores[country] + " puntos");
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Guinea Ecuatorial:" + scores["Guinea Ecuatorial"] + " Francia: " + scores["Francia"] +"  Japon: " + scores["Japon"];
    }

    //metodo para obtener puntuaciones
    public Dictionary<string, int> GetScores()
    {
        return scores;
    }
}