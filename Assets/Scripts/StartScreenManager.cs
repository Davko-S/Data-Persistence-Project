using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is controling Main Menu and provides static instance to be able to share data between scenes
/// </summary>

public class StartScreenManager : MonoBehaviour
{
    public static StartScreenManager Instance;

    public InputField UserName_textBox;
    public int HighScore;

    private void Awake() 
    {
        if (Instance != null) 
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData 
    {
        public string UserName;
        public int HighScore = 0;
    }
    // A method to save user data to JSON file 
    public void SaveUserData()
    {

    }


}
