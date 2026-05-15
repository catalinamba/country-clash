using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SeleccionPais : MonoBehaviour
{
    [Header("UI")]
    public TMP_Dropdown dropdownPais;

    public void ConfirmarSeleccion()
    {
        string paisSeleccionado = dropdownPais.options[dropdownPais.value].text;

        PlayerPrefs.SetString("pais", paisSeleccionado);

        Debug.Log("País seleccionado: " + paisSeleccionado);

        SceneManager.LoadScene("Juego");

        if (string.IsNullOrEmpty(paisSeleccionado))
        {
            paisSeleccionado = "Guinea Ecuatorial"; // valor por defecto
        }
    }
}