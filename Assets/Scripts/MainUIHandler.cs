using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// This is the manager of the main menu scene
/// </summary>

public class MainUIHandler : MonoBehaviour
{
    [SerializeField] Text UserNameInput;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    // Asigning username from player's input into PlayerDataHandler
    public void SetUserName()
    {
        PlayerDataHandler.Instance.UserName = UserNameInput.text; 
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}