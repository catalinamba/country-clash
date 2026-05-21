using System;

[Serializable]
public class Player
{
    public string nombre;
    public string max_puntuacion;
}

[Serializable]
public class PlayerList
{
    public Player[] players;
}