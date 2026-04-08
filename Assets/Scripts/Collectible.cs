using UnityEngine;

public class Collectible : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AddScore("Player", 1);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Bot"))
        {
            GameManager.instance.AddScore("Bot", 1);
            Destroy(gameObject);
        }
    }

}