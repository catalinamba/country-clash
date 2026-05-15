using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text;

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

    private Dictionary<string, int> scores = new Dictionary<string, int>();
    private List<Dictionary<string, int>> historialPartidas = new List<Dictionary<string, int>>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        scores["Player"] = 0;
        scores["Bot_Francia"] = 0;
        scores["Bot_Japón"] = 0;
        scores["Bot_Guinea Ecuatorial"] = 0;

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (endGamePanel != null)
            endGamePanel.SetActive(false);
    }

    // =========================
    // SCORE SYSTEM
    // =========================
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

        scoreText.text =
            "Player: " + scores["Player"] + "\n" +
            "Francia: " + scores["Bot_Francia"] + "\n" +
            "Japón: " + scores["Bot_Japón"] + "\n" +
            "Guinea Ecuatorial: " + scores["Bot_Guinea Ecuatorial"];
    }

    // =========================
    // END GAME
    // =========================
    public void EndGame()
    {
        if (endGamePanel != null)
            endGamePanel.SetActive(true);

        Time.timeScale = 0f;

        int playerScore = scores["Player"];
        int francia = scores["Bot_Francia"];
        int japon = scores["Bot_Japón"];
        int ge = scores["Bot_Guinea Ecuatorial"];

        // sonido final general (opcional)
        if (audioSource != null && endGameSound != null)
            audioSource.PlayOneShot(endGameSound);

        // ranking
        List<(string nombre, int puntos)> ranking = new List<(string, int)>
        {
            ("Player", playerScore),
            ("Francia", francia),
            ("Japón", japon),
            ("Guinea Ecuatorial", ge)
        };

        ranking.Sort((a, b) => b.puntos.CompareTo(a.puntos));

        // TEXTO RANKING
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("PUESTOS\n");

        for (int i = 0; i < ranking.Count; i++)
        {
            sb.AppendLine((i + 1) + ". " + ranking[i].nombre);
        }

        if (rankingText != null)
            rankingText.text = sb.ToString();

        // GANADOR / PERDEDOR
        string winner = ranking[0].nombre;

        if (winnerText != null)
            winnerText.text = "GANADOR: " + winner;

        if (playerScore == ranking[0].puntos)
        {
            if (audioSource != null && winSound != null)
                audioSource.PlayOneShot(winSound);

            if (resultText != null)
                resultText.text = "HAS GANADO";
        }
        else
        {
            if (audioSource != null && loseSound != null)
                audioSource.PlayOneShot(loseSound);

            if (resultText != null)
                resultText.text = "HAS PERDIDO";
        }

        GuardarPartida();
    }

    // =========================
    // HISTORIAL
    // =========================
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

    // =========================
    // RANKING SCENE
    // =========================
    public void IrRanking()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Ranking");
    }
}