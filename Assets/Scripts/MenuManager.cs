using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TMP_InputField inputNombre;

    public void Jugar()
    {
        // guardar nombre antes de cambiar de escena
        if (inputNombre != null && inputNombre.text != "")
        {
            PlayerDataLogin.playerName = inputNombre.text;
        }
        else
        {
            PlayerDataLogin.playerName = "Player";
        }

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