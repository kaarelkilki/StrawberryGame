using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int count;
    public float timeRemaining;
    public int highScore;

    public PlayerData(PlayerController player)
    {
        count = player.count;
        timeRemaining = player.timeRemaining;
        highScore = player.highScore;
    }
}
