using UnityEngine;
using TMPro;
using System.Collections;

public class RankingUI : MonoBehaviour
{
    public APIManager apiManager;
    public TextMeshProUGUI textoRanking;

    void Start()
    {
        textoRanking.text = "Cargando Ranking...";
        StartCoroutine(CargarRanking());
    }

    IEnumerator CargarRanking()
    {
        yield return apiManager.GetRanking(MostrarRanking);
    }

    void MostrarRanking(string json)
    {
        Debug.Log("RAW JSON: " + json);

        RankingResponse data = JsonUtility.FromJson<RankingResponse>(json);

        string texto = "RANKING ONLINE\n\n";

        for (int i = 0; i < data.players.Length; i++)
        {
            texto += (i + 1) + ". " +
                     data.players[i].nombreJugador + " - " +
                     data.players[i].puntuacion + " pts\n";
        }

        textoRanking.text = texto;
    }
}