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

        List<string> maxPaises = new List<string>();
        int maxPuntos = -1;

        foreach (var pais in scores)
        {
            texto += pais.Key + ": " + pais.Value + " puntos\n";

            if (pais.Value > maxPuntos)
            {
                maxPuntos = pais.Value;
                maxPaises.Clear();
                maxPaises.Add(pais.Key);
            }
            else if (pais.Value == maxPuntos)
            {
                maxPaises.Add(pais.Key);
            }
        }

        if (maxPaises.Count == 1)
            texto += "\nGANADOR: " + maxPaises[0] + " con " + maxPuntos + " puntos";
        else
            texto += "\nEMPATE: " + string.Join(", ", maxPaises) + " con " + maxPuntos + " puntos";

        rankingText.text = texto;
    }
}