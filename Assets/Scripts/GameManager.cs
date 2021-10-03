using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int rocksUsed = 0;
    public float totalHeight = 0;
    public float score = 0;

    public Text rockCountText;
    public Text Height;
    public Text Score;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        Rock[] rockList = GameObject.FindObjectsOfType<Rock>();

        int temp = 0;
        float highestPoint = 0;
        foreach (Rock rock in rockList)
        {
            if (rock.rooted || rock.root)
            {
                temp++;
                float point = rock.gameObject.GetComponent<TestVertices>().GetTrueHeight().y;
                if (point > highestPoint)
                {
                    highestPoint = point;
                }
            }
        }
        rocksUsed = temp;
        totalHeight = highestPoint;

        if (rocksUsed == 0)
        {
            score = 0;
        }
        else
        {
            score = (totalHeight * 100) / rocksUsed;
        }

        rockCountText.text = "Stones - "  + (int)rocksUsed;
        Score.text = "Score - " + (int)score;
        Height.text = "Height - " + totalHeight;
    }

    public void SetHeight(float height)
    {
        totalHeight = height;
    }
}
