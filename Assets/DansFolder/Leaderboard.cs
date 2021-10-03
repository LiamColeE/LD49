using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{

    public Highscores highscores;
    public List<PlayerEntry> entries = new List<PlayerEntry>();

    public bool resetPlayerPrefs;

    void Start()
    {
        if(resetPlayerPrefs)
            PlayerPrefs.DeleteAll();

        highscores = new Highscores();
        InitializeHighScores();
        LoadHighScores();
        SetEntries();
    }

    public void NewHighScore(string playerName, float highscore)
    {
        highscores.all.Add(new Highscore(playerName, highscore));

        string json = JsonUtility.ToJson(highscores);

        PlayerPrefs.SetString("Highscores", json);
        PlayerPrefs.Save();
        LoadHighScores();
        SetEntries();
    }

    public void LoadHighScores()
    {
        string json = PlayerPrefs.GetString("Highscores");

        highscores = JsonUtility.FromJson<Highscores>(json);

        foreach(var v in highscores.all)
        {
            Debug.Log(v.name + ": " + v.score);
        }
    }

    public void InitializeHighScores()
    {
        if(PlayerPrefs.GetString("Highscores") == null)
        {
            PlayerPrefs.SetString("Highscores", "");
            PlayerPrefs.Save();
        }
    }

    void SetEntries()
    {
        highscores.all.Sort((p1,p2)=>p2.score.CompareTo(p1.score));
        int place = 0;
        foreach(Highscore hs in highscores.all)
        {
            entries[place].GetComponentInChildren<Text>().text = hs.name + " - " + hs.score;
            place++;
        }
    }

    [Serializable]
    public class Highscore
    {
        public string name;
        public float score;

        public Highscore(string h, float s)
        {
            name = h;
            score = s;
        }
    }

    [Serializable]
    public class Highscores
    {
        public List<Highscore> all = new List<Highscore>();
    }


}
