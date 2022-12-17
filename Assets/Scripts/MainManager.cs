using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;

    // Added lines : updated score and playername display
    public Text UserName;
    public Text BestPlayerNameAndScore;

    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    // Added lines: static fields to keep player's data
    private static int bestScore;
    private static string bestPlayer;

    // Data persistence: loading data from previous gameplays, if given
    private void Awake() 
    {
        LoadRank();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        // Assigning current username from player's Input in the menu scene
        UserName.text = PlayerDataHandler.Instance.UserName;
        SetBestPlayer();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        PlayerDataHandler.Instance.Score = m_Points; // Data persistence: getting the current score and sendint to PDH
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        CheckBestPlayer();
        GameOverText.SetActive(true);
    }

    private void CheckBestPlayer()
    {
        int currentScore = PlayerDataHandler.Instance.Score; 
        
        if (currentScore > bestScore)
        {
            bestPlayer = PlayerDataHandler.Instance.UserName;
            bestScore = currentScore;
            BestPlayerNameAndScore.text = $"High Score - {bestPlayer}: {bestScore}";
            
            // Data persistence: saving best player's name and score
            SaveGameRank(bestPlayer, bestScore);
        }
    }

    private void SetBestPlayer()
    {
        // EDGE_CASE: no best player yet
        if (bestPlayer == null && bestScore == 0) 
        {
            BestPlayerNameAndScore.text = "";
        }
        else
        {
            BestPlayerNameAndScore.text = $"High Score - {bestPlayer}: {bestScore}";
        }
    }

    public void SaveGameRank(string bestPlayerName, int bestPlayerScore)
    {
        SaveData data = new SaveData();
        data.TheBestPlayer = bestPlayerName;
        data.HighiestScore = bestScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadRank()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            // Data persistence: loading the saved data to static members
            bestPlayer = data.TheBestPlayer;
            bestScore = data.HighiestScore;
        }
    }

    [System.Serializable]
    class SaveData
    {
        public int HighiestScore;
        public string TheBestPlayer;
    }
}
