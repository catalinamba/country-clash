using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string countryName;
    public int points = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        //player
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null && player.playerCountry == countryName)
            {
                GameManager.instance.AddScore("Player", 1);
                Destroy(gameObject);
            }
        }

        //bot
        else if (other.CompareTag("Bot"))
        {
            BotAI bot = other.GetComponent<BotAI>();

            if (bot != null && bot.targetCountry == countryName)
            {
                GameManager.instance.AddScore("Bot_" + countryName, 1);
                Destroy(gameObject);
            }
        }
    }

}
