using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{

    string urlSave = "http://localhost/countryclash_api/saveScore.php";
    string urlRanking = "http://localhost/countryclash_api/getRanking.php";

    public void SendScore(string nombre, int puntuacion, string pais)
    {
        StartCoroutine(SendScoreCoroutine(nombre, puntuacion, pais));
    }

    IEnumerator SendScoreCoroutine(string nombre, int puntuacion, string pais)
    {
        WWWForm form = new WWWForm();
        form.AddField("nombre", nombre);
        form.AddField("puntuacion", puntuacion);
        form.AddField("pais", pais);

        UnityWebRequest www = UnityWebRequest.Post(urlSave, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
            Debug.Log("Score enviado");
        else
            Debug.LogError(www.error);
    }

    public IEnumerator GetRanking(System.Action<string> callback)
    {
        Debug.Log("Pidiendo ranking...");

        UnityWebRequest www = UnityWebRequest.Get(urlRanking);
        yield return www.SendWebRequest();

        Debug.Log("Respuesta recibida");

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(www.downloadHandler.text);
            callback(www.downloadHandler.text);
        }
        else
        {
            Debug.LogError(www.error);
        }
    }
}