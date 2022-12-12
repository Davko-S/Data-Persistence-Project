using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int score;
    public string username;

    public GameData()
    {
        this.score = 0;
        this.username = "";
    
    }
}
