using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Temporizador : MonoBehaviour
{
    public float tiempoInicial = 60f;
    private float tiempoActual;

    public TextMeshProUGUI textoTiempo;
    private bool gameEnded = false;

    void Start()
    {
        tiempoActual = tiempoInicial;
    }

    void Update()
    {
        if (gameEnded) return;

        tiempoActual -= Time.deltaTime;

        if (tiempoActual <= 0)
        {
            tiempoActual = 0;
            gameEnded = true;
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
        Debug.Log("FIN DEL JUEGO");

        if (GameManager.instance != null)
        {
            GameManager.instance.EndGame();
        }
        else
        {
            Debug.Log("GameManager NO EXISTE");
        }
    }
}