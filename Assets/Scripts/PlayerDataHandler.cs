using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A static class using Singleton pattern to keep player's data between scenes
/// </summary>
public class PlayerDataHandler : MonoBehaviour
{
    // Constructor of a public static instance
    public static PlayerDataHandler Instance;

    public string UserName;
    public int Score;

    private void Awake() 
    {
        // Making sure that there's only one static instance of this class - Singleton creation
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}