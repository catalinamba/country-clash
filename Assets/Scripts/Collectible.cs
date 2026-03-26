using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string countryName;  // Nombre del país para identificar el objeto
    public int points = 1;      // Puntos que da al recolectar

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AddScore(countryName, points);
            Destroy(gameObject); // Se destruye al recolectar
        }
    }
}

