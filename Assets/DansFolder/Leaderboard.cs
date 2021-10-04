using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Leaderboard : MonoBehaviour
{

    public Highscores highscores;
    public List<PlayerEntry> entries = new List<PlayerEntry>();

    public Text tryAgainText;
    public Text goodJobText;

    string getJson;

    float lowestScore = float.NegativeInfinity;

    void Start()
    {
        SetColorOfTextToTransparent(tryAgainText, 0f);
        SetColorOfTextToTransparent(goodJobText, 0f);
        LoadHighscoresFromAPI();
    }

    void SetColorOfTextToTransparent(Text text, float alpha)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }

    IEnumerator DisplayTextOverTime(Text text, float rate)
    {
        float holdRate = 0f;
        while(holdRate < 1)
        {
            holdRate += rate * Time.deltaTime;
            SetColorOfTextToTransparent(text, holdRate);
            yield return null;
        }
        yield return new WaitForSeconds(1);

        while(holdRate > 0)
        {
            holdRate -= rate * Time.deltaTime;
            SetColorOfTextToTransparent(text, holdRate);
            yield return null;
        }
    }

    public void AddNewScore(string playerName, float score)
    {
        if(score > lowestScore)
            StartCoroutine(Upload("https://cairns-leaderboard.herokuapp.com/add_score/" + playerName + "/" + score, "{ \"name\" : \"" + playerName + "\", \"score\" : \" " + score.ToString() + "\"}"));
        else
        {
            StartCoroutine(DisplayTextOverTime(tryAgainText, 2f));
        }
    }

    public void LoadHighscoresFromAPI()
    {
        StartCoroutine(GetRequest("https://cairns-leaderboard.herokuapp.com/gettop"));
    }

    void SetEntries()
    {
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
        public string score;

        public Highscore(string h, string s)
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

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {

            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    Highscores json = JsonUtility.FromJson<Highscores>("{ \"all\": " + webRequest.downloadHandler.text + "}");
                    highscores = json;
                    if(highscores.all.Count > 0)
                        lowestScore = float.Parse(json.all[json.all.Count - 1].score);
                    SetEntries();
                    break;
            }
        }
    }

    IEnumerator Upload(string url, string data)
    {
        byte[] myData = System.Text.Encoding.UTF8.GetBytes(data);
        using (UnityWebRequest www = UnityWebRequest.Put(url, data))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Upload complete!");
                LoadHighscoresFromAPI();
            }
        }
    }


}
