using UnityEngine;
using TMPro;

public class ResultadosFinales : MonoBehaviour
{
    public TextMeshProUGUI texto;

    public APIManager apiManager;

    int scoreFinal;
    string playerName;
    string country;

    void Start()
    {
        MostrarHistorial();
        EnviarPuntuacion();
    }

    void MostrarHistorial()
    {
        var historial = GameManager.instance.GetHistorial();

        string t = "Historial de partidas:\n\n";

        for (int i = 0; i < historial.Count; i++)
        {
            var partida = historial[i];

            t += "Partida " + (i + 1) + ":\n";
            t += "Player: " + partida["Player"] + "\n";
            t += "Francia: " + partida["Bot_Francia"] + "\n";
            t += "Japón: " + partida["Bot_Japón"] + "\n";
            t += "GE: " + partida["Bot_Guinea Ecuatorial"] + "\n\n";
        }

        texto.text = t;
    }

    void EnviarPuntuacion()
    {
        if (apiManager != null)
        {
            apiManager.SendScore(playerName, scoreFinal, country);
            Debug.Log("Enviando puntuación...");
        }
        else
        {
            Debug.LogError("APIManager no asignado");
        }
    }
}