using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Personajes")]
    public GameObject playerPrefab;
    public GameObject botPrefab;
    public Vector2 playerSpawnPos;
    public Vector2[] botSpawnPositions;

    [Header("Países")]
    public string[] paises = { "Guinea Ecuatorial", "Francia", "Japón" };

    [Header("AOC Player")]
    public AnimatorOverrideController playerFrancia;
    public AnimatorOverrideController playerJapon;
    public AnimatorOverrideController playerGuinea;

    [Header("AOC Bots")]
    public AnimatorOverrideController botFrancia;
    public AnimatorOverrideController botJapon;
    public AnimatorOverrideController botGuinea;

  
    [Header("Objetos por país")]
    public int objetosPorPais = 5;

    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    public GameObject coleccion_Francia;
    public GameObject coleccion_GuineaEcuatorial;
    public GameObject coleccion_Japon;

    void Start()
    {
        SpawnPlayer();
        SpawnBots();
        SpawnObjetos(); 
    }

    void SpawnPlayer()
    {
        GameObject player = Instantiate(playerPrefab, playerSpawnPos, Quaternion.identity);

        PlayerController pc = player.GetComponent<PlayerController>();
        Animator anim = player.GetComponent<Animator>();

        string pais = PlayerPrefs.GetString("pais");

        if (pc != null)
            pc.playerCountry = pais;

        if (anim == null)
        {
            Debug.LogError("Player sin Animator");
            return;
        }

        if (pais == "Francia")
            anim.runtimeAnimatorController = playerFrancia;
        else if (pais == "Japón")
            anim.runtimeAnimatorController = playerJapon;
        else
            anim.runtimeAnimatorController = playerGuinea;
    }

    void SpawnBots()
    {
        for (int i = 0; i < botSpawnPositions.Length; i++)
        {
            GameObject bot = Instantiate(botPrefab, botSpawnPositions[i], Quaternion.identity);

            BotAI ai = bot.GetComponent<BotAI>();
            Animator anim = bot.GetComponent<Animator>();

            string pais = paises[i % paises.Length];

            if (ai != null)
                ai.targetCountry = pais;

            if (anim == null)
            {
                Debug.LogError("Bot sin Animator");
                continue;
            }

            if (pais == "Francia")
                anim.runtimeAnimatorController = botFrancia;
            else if (pais == "Japón")
                anim.runtimeAnimatorController = botJapon;
            else
                anim.runtimeAnimatorController = botGuinea;
        }
    }

    //Spawn objetos
    void SpawnObjetos()
    {
        foreach (string pais in paises)
        {
            for (int i = 0; i < objetosPorPais; i++)
            {
                Vector2 pos = new Vector2(
                    Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                    Random.Range(spawnAreaMin.y, spawnAreaMax.y)
                );

                GameObject prefab = GetPrefab(pais);

                if (prefab == null)
                {
                    Debug.LogError("No hay prefab para: " + pais);
                    continue;
                }

                GameObject obj = Instantiate(prefab, pos, Quaternion.identity);

                Collectible c = obj.GetComponent<Collectible>();

                if (c != null)
                    c.countryName = pais;
            }
        }
    }

    GameObject GetPrefab(string pais)
    {
        if (pais == "Francia") return coleccion_Francia;
        if (pais == "Japón") return coleccion_Japon;
        if (pais == "Guinea Ecuatorial") return coleccion_GuineaEcuatorial;
        return null;
    }
}
