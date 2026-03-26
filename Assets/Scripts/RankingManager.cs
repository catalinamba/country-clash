using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class RankingManager : MonoBehaviour
{
    public TextMeshProUGUI rankingText;

    void Start()
    {
        MostrarRanking();
    }

    void MostrarRanking()
    {
        Dictionary<string, int> scores = GameManager.instance.GetScores();

        string texto = "RESULTADOS:\n";

        foreach (var pais in scores)
        {
            texto += pais.Key + ": " + pais.Value + " puntos\n";
        }

        rankingText.text = texto;
    }
}