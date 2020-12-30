using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int count;
    public float timeRemaining;
    public int highScore;
    public int scoreII;
    public int scoreIII;
    public int scoreIV;
    public int scoreV;
    public float extraTime;
    public float[] position;

    public PlayerData(PlayerController player)
    {
        count = player.count;
        timeRemaining = player.timeRemaining;
        highScore = player.highScore;
        scoreII = player.scoreII;
        scoreIII = player.scoreIII;
        scoreIV = player.scoreIV;
        scoreV = player.scoreV;
        extraTime = player.extraTime;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
