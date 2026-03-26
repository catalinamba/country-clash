using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorEscenas : MonoBehaviour
{
    public void IrSeleccionPais()
    {
        SceneManager.LoadScene("SeleccionPais");
    }

    public void IrPantallaInicio()
    {
        SceneManager.LoadScene("Inicio");
    }

    public void IrPantallaJuego()
    {
        SceneManager.LoadScene("Juego");
    }
} 