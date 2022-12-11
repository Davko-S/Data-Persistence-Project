using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataHandler : MonoBehaviour
{
    public static PlayerDataHandler Instance;

    public string _playerName;
    public int _score;
    
    private void Awake() 
    {
        if (Instance != null) 
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
