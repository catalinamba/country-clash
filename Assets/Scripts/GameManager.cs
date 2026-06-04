using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public GameObject endGamePanel;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI rankingText;
    public TextMeshProUGUI winnerText;

    [Header("Audio")]
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip endGameSound;
    private AudioSource audioSource;

    [Header("API")]
    public APIManager apiManager;

    [Header("Player Data")]
    public string selectedCountry;

    private Dictionary<string, int> scores = new Dictionary<string, int>();
    private List<Dictionary<string, int>> historialPartidas = new List<Dictionary<string, int>>();

    void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();

        string player = PlayerDataLogin.playerName;

        if (!scores.ContainsKey(player))
            scores.Add(player, 0);

        scores["Bot_Francia"] = 0;
        scores["Bot_Japón"] = 0;
        scores["Bot_Guinea Ecuatorial"] = 0;


    }

    void Start()
    {
        if (endGamePanel != null)
            endGamePanel.SetActive(false);

        selectedCountry = PlayerPrefs.GetString("pais", "Guinea Ecuatorial");

        if (scoreText != null)
            scoreText.text =
                "País: " + selectedCountry + "\n\n" +
                "Player: 0\nFrancia: 0\nJapón: 0\nGuinea Ecuatorial: 0";
    }

    public void AddScore(string key, int points)
    {
        if (scores.ContainsKey(key))
        {
            scores[key] += points;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        if (scoreText == null) return;

        string player = PlayerDataLogin.playerName;

        int playerScore = scores.ContainsKey(player) ? scores[player] : 0;

        scoreText.text =
            "Player: " + playerScore + "\n" +
            "Francia: " + scores["Bot_Francia"] + "\n" +
            "Japón: " + scores["Bot_Japón"] + "\n" +
            "Guinea Ecuatorial: " + scores["Bot_Guinea Ecuatorial"];
    }

    public void EndGame()
    {
        if (endGamePanel != null)
            endGamePanel.SetActive(true);

        Time.timeScale = 0f;

        int playerScore = scores[PlayerDataLogin.playerName];
        int francia = scores["Bot_Francia"];
        int japon = scores["Bot_Japón"];
        int ge = scores["Bot_Guinea Ecuatorial"];

        if (audioSource != null && endGameSound != null)
            audioSource.PlayOneShot(endGameSound);

        List<(string nombre, int puntos)> ranking = new List<(string, int)>
        {
            (PlayerDataLogin.playerName, playerScore),
            ("Francia", francia),
            ("Japón", japon),
            ("Guinea Ecuatorial", ge)
        };

        ranking.Sort((a, b) => b.puntos.CompareTo(a.puntos));

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Puestos\n");

        for (int i = 0; i < ranking.Count; i++)
        {
            sb.AppendLine((i + 1) + ". " + ranking[i].nombre + " - " + ranking[i].puntos + " pts");
        }

        if (rankingText != null)
            rankingText.text = sb.ToString();

        string winner = ranking[0].nombre;

        if (winnerText != null)
            winnerText.text = "Ganador: " + winner;

        if (playerScore == ranking[0].puntos)
        {
            if (audioSource != null && winSound != null)
                audioSource.PlayOneShot(winSound);

            if (resultText != null)
                resultText.text = "HAS GANADO!";
        }
        else
        {
            if (audioSource != null && loseSound != null)
                audioSource.PlayOneShot(loseSound);

            if (resultText != null)
                resultText.text = "Has perdido";
        }

        EnviarPuntuacion();
    }

    void EnviarPuntuacion()
    {
        Debug.Log("PAÍS SELECCIONADO REAL: " + selectedCountry);

            if (apiManager != null)
            {
                apiManager.SendScore(
                    PlayerDataLogin.playerName,
                    GameManager.instance.GetScores()[PlayerDataLogin.playerName],
                    PlayerPrefs.GetString("pais", "Guinea Ecuatorial")//VALOR POR DEFECTO  GUINEA ECUATORIAL , EN CASO DE QUE NO HAYA ESCOGIDO PAIS
                );

                Debug.Log("Score enviado a la API: " + scores[PlayerDataLogin.playerName]);
        }
        else
        {
            Debug.LogError("APIManager no asignado en GameManager");
        }
    }

    void GuardarPartida()
    {
        historialPartidas.Add(new Dictionary<string, int>(scores));
    }

    public List<Dictionary<string, int>> GetHistorial()
    {
        return historialPartidas;
    }

    public Dictionary<string, int> GetScores()
    {
        return scores;
    }

    public void IrRanking()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Ranking");
    }
}
