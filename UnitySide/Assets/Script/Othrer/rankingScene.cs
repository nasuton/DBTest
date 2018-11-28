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

    [SerializeField]
    private Text newRank = null;

    void Start()
    {
        StartCoroutine(hoge());
    }

    IEnumerator hoge()
    {
        var rank = RecordSQL.Instance.GetRanking();

        yield return new WaitForSeconds(10.0f);

        rank = RecordSQL.Instance.GetRanking();

        yield return new WaitForSeconds(5.0f);

        int uid = 0;
        uid = RecordSQL.Instance.UserId;

        newScore.text = RecordSQL.Instance.UserPoint.ToString();
        newName.text = RecordSQL.Instance.UserName;
        if(uid < 0 || 10 <= uid)
        {
            newRank.text = "ランク外";
        }
        else
        {
            uid += 1;
            newRank.text = uid.ToString();
        }
        
        for (int i = 0; i < rank.Count; i++)
        {
            nameText[i].text = rank[i].name;
            scoreText[i].text = rank[i].point.ToString();
        }
    }
}
