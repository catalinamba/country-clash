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

        string texto ="RESULTADOS:\n";

        // convertir a lista para poder ordenar
        List<KeyValuePair<string, int>> lista = new List<KeyValuePair<string, int>>(scores);

        // ordenar de mayor a menor
        lista.Sort((a, b) => b.Value.CompareTo(a.Value));

        foreach (var item in lista)
        {
            texto += item.Key + ": " + item.Value + " puntos\n";
        }

        // ganador
        if (lista.Count > 0)
        {
            int max = lista[0].Value;

            List<string> ganadores = new List<string>();

            foreach (var item in lista)
            {
                if (item.Value == max)
                    ganadores.Add(item.Key);}

            if (ganadores.Count == 1)
                texto += "\nGANADOR: " + ganadores[0] + " con " + max + " puntos";
            else
                texto += "\nEMPATE: " + string.Join(", ", ganadores) + " con " + max + " puntos";
        }

        rankingText.text = texto;
    }
}

//public class RankingManager : MonoBehaviour
//{
//    public TextMeshProUGUI rankingText;

//    void Start()
//    {
//        MostrarRanking();
//    }

//    void MostrarRanking()
//    {
//        Dictionary<string, int> scores = GameManager.instance.GetScores();

//        string texto = "RESULTADOS:\n";

//        List<string> maxPaises = new List<string>();
//        int maxPuntos = -1;

//        foreach (var pais in scores)
//        {
//            texto += pais.Key + ": " + pais.Value + " puntos\n";

//            if (pais.Value > maxPuntos)
//            {
//                maxPuntos = pais.Value;
//                maxPaises.Clear();
//                maxPaises.Add(pais.Key);
//            }
//            else if (pais.Value == maxPuntos)
//            {
//                maxPaises.Add(pais.Key);
//            }
//        }

//        if (maxPaises.Count == 1)
//            texto += "\nGANADOR: " + maxPaises[0] + " con " + maxPuntos + " puntos";
//        else
//            texto += "\nEMPATE: " + string.Join(", ", maxPaises) + " con " + maxPuntos + " puntos";

//        rankingText.text = texto;
//    }
//}
