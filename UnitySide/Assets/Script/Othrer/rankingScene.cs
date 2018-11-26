using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class rankingScene : MonoBehaviour
{
    [SerializeField]
    Text[] nameText = null;

    [SerializeField]
    Text[] scoreText = null;

    [SerializeField]
    private Text newName = null;

    [SerializeField]
    private Text newScore = null;

    void Start()
    {
        StartCoroutine(hoge());
    }

    IEnumerator hoge()
    {
        var rank = RecordSQL.Instance.GetRanking();

        yield return new WaitForSeconds(10.0f);

        newScore.text = RecordSQL.Instance.UserScore.ToString();
        newName.text = RecordSQL.Instance.UserName;
        
        for (int i = 0; i < rank.Count; i++)
        {
            nameText[i].text = rank[i].name;
            scoreText[i].text = rank[i].point.ToString();
        }
    }
}
