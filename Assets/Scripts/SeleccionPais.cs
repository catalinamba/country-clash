using UnityEngine;
using UnityEngine.SceneManagement;

public class SeleccionPais : MonoBehaviour
{
    public void ComenzarJuego()
    {
        SceneManager.LoadScene("Juego");
    }
}