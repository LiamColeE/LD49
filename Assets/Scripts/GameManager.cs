using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int rocksUsed;
    public float totalHeight;

    public float score;

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
        foreach (Rock rock in rockList)
        {
            if (rock.rooted || rock.root)
            {
                temp++;
            }
        }
        rocksUsed = temp;

        rockCountText.text = "Stones - "  + rocksUsed;
    }

    public void SetHeight(float height)
    {
        totalHeight = height;
    }
}
