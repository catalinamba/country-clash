using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Temporizador : MonoBehaviour
{
    public float tiempoInicial = 60f;
    private float tiempoActual;

    public TextMeshProUGUI textoTiempo;

    void Start()
    {
        tiempoActual = tiempoInicial;
    }

    void Update()
    {
        tiempoActual -= Time.deltaTime;

        if (tiempoActual <= 0)
        {
            tiempoActual = 0;
            FinDelJuego();
        }

        MostrarTiempo();
    }

    void MostrarTiempo()
    {
        int segundos = Mathf.FloorToInt(tiempoActual);
        textoTiempo.text = "Tiempo: " + segundos;
    }

    void FinDelJuego()
    {
        SceneManager.LoadScene("Ranking");
    }
}