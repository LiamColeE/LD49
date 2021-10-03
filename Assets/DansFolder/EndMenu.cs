using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        Debug.Log("Submitted score for " + playerName.GetComponentsInChildren<Text>()[1].text);

        //GameManager.instance.score to get the score
        float placeholderScore = 2000000000f;

        leaderboard.NewHighScore(playerName.GetComponentsInChildren<Text>()[1].text, placeholderScore);
    }

}
