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

        PlayerList players = JsonUtility.FromJson<PlayerList>(json);

        string texto = "🏆 RANKING ONLINE\n\n";

        for (int i = 0; i < players.players.Length; i++)
        {
            texto += (i + 1) + ". " +
                     players.players[i].nombre + " - " +
                     players.players[i].max_puntuacion + "\n";
        }

        Debug.Log("TEXTO FINAL:\n" + texto);

        textoRanking.text = texto;
    }
}