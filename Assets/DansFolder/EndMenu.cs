using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{

    InputField playerName;
    [SerializeField] Leaderboard leaderboard;
    
    void Awake()
    {
        playerName = GetComponentInChildren<InputField>();
    }

    public void SubmitScore()
    {
        float score = GameManager.instance.score;
        leaderboard.AddNewScore(playerName.GetComponentsInChildren<Text>()[1].text, GameManager.instance.score);
        playerName.Select();
        playerName.text = "";
    }

    public void Restart()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
