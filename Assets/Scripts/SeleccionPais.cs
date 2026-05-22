using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SeleccionPais : MonoBehaviour
{
    [Header("UI")]
    public TMP_Dropdown dropdownPais;

    public void ConfirmarSeleccion()
    {
        string paisSeleccionado = dropdownPais.options[dropdownPais.value].text;

        if (string.IsNullOrEmpty(paisSeleccionado))
        {
            paisSeleccionado = "Guinea Ecuatorial";
        }

        PlayerPrefs.SetString("pais", paisSeleccionado);
        PlayerPrefs.Save();

        Debug.Log("País seleccionado: " + paisSeleccionado);

        SceneManager.LoadScene("Juego");
    }
}