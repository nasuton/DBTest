using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    private static ScoreCount mInstance = null;

    private int nowScore = 0;

    [SerializeField]
    private Text scoreText = null;

    public static ScoreCount Instance
    {
        get
        {
            return mInstance;
        }
        set
        {

        }
    }

    private void Awake()
    {
        mInstance = gameObject.GetComponent<ScoreCount>();
    }

    public void SetScore(int num)
    {
        nowScore += num;
        scoreText.text = nowScore.ToString();
    }

    public int GetScore()
    {
        return nowScore;
    }
}
