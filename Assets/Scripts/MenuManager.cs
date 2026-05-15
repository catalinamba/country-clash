using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("SeleccionPais");
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Saliendo...");
    }

    public void IrRanking()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Ranking");
    }

}