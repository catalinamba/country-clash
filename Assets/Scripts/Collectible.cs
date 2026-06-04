using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string countryName;
    public int points = 1;

    void Start()
    {
        CollectibleManager.Register(this);
    }

    void OnDestroy()
    {
        CollectibleManager.Unregister(this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null && player.playerCountry == countryName)
            {
                GameManager.instance.AddScore(PlayerDataLogin.playerName, points);
                Destroy(gameObject);
            }
        }
        else if (other.CompareTag("Bot"))
        {
            BotAI bot = other.GetComponent<BotAI>();

            if (bot != null && bot.targetCountry == countryName)
            {
                GameManager.instance.AddScore("Bot_" + countryName, points);
                Destroy(gameObject);
            }
        }
    }
}