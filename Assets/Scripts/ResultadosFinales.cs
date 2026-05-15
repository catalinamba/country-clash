using UnityEngine;
using TMPro;

public class ResultadosFinales : MonoBehaviour
{
    public TextMeshProUGUI texto;

    void Start()
    {
        MostrarHistorial();
    }

    void MostrarHistorial()
    {
        var historial = GameManager.instance.GetHistorial();

        string t = "HISTORIAL DE PARTIDAS:\n\n";

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
}