using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataHandler : MonoBehaviour
{
    public static PlayerDataHandler Instance;

    public string _playerName;
    public InputField usernameInput;
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

    private void Start() 
    {
        SaveUsername();
    }

    //Added SaveData class to be able to keep the color data
    [System.Serializable]
    class SaveData
    {
        public string username;
    }
// A method to save player name into JSON file (serialization)
    public void SaveUsername()
    {
        _playerName = usernameInput.text;
        Debug.Log(_playerName);
        SaveData data = new SaveData();
        data.username = _playerName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
// A method to load player name from previously saved JSON file (deserialization)
    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            _playerName = data.username;
        }
    }
}
