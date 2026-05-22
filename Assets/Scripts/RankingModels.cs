using System;

[System.Serializable]
public class RankingPlayerData
{
    public string nombreJugador;
    public string pais;
    public int puntuacion;
}

[System.Serializable]
public class RankingResponse
{
    public RankingPlayerData[] players;
}