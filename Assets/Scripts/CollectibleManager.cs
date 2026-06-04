using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static List<Collectible> all = new List<Collectible>();

    public static void Register(Collectible c)
    {
        all.Add(c);
    }

    public static void Unregister(Collectible c)
    {
        all.Remove(c);
    }
}