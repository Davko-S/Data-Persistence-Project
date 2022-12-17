using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameRank : MonoBehaviour
{
    // Fields to display current player info

    public Text BestPlayerName;

    // Static variables for holding best player data

    private static string BestPlayer;
    private static int BestScore;

    private void Awake() 
    {
        LoadRank();
    }

    public void LoadRank()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestPlayer = data.TheBestPlayer;
            BestScore = data.HighiestScore;
            SetBestPlayer();
        }
    }

    private void SetBestPlayer()
    {
        if (BestPlayer == null && BestScore == 0) 
        {
            BestPlayerName.text = "";
        } 
        else
        {
            BestPlayerName.text = $"High Score - {BestPlayer}: {BestScore}";
        }
    }

    [System.Serializable]
    class SaveData
    {
        public int HighiestScore;
        public string TheBestPlayer;
    } 
}