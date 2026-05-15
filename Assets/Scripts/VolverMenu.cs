using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverMenu : MonoBehaviour
{
    public void Volver()
    {
        SceneManager.LoadScene("MainMenu");
    }
}