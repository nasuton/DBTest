using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DBUsers
{
    public int id;
    public string name;
    public int point;
}


public class RecordSQL : MonoBehaviour
{

    [SerializeField]
    private string getRanking_url = "http://localhost/getRecord.php";

    [SerializeField]
    private string setRanking_url = "http://localhost/setRecord.php";

    [SerializeField]
    private const int MAX_RANK = 10;

    //ランキング作成時に使用する予定
    private List<DBUsers> usersList;

    private static RecordSQL mInstance = null;

    public static RecordSQL Instance
    {
        get
        {
            return mInstance;
        }
    }

    private void Awake()
    {
        mInstance = gameObject.GetComponent<RecordSQL>();
    }

    public void GetRanking()
    {
        for(int i = 1; i < 11; i++)
        {
            StartCoroutine(GetRankingData(i));
        }
    }

    IEnumerator GetRankingData(int uid)
    {
        WWWForm form = new WWWForm();

        form.AddField("user", uid);

        using (WWW www = new WWW(getRanking_url, form))
        {
            yield return www;
            if(!string.IsNullOrEmpty(www.error))
            {
                Debug.LogError("error : " + www.error);
                yield break;
            }

            DBUsers user = JsonUtility.FromJson<DBUsers>(www.text);
            Debug.Log("id :" + user.id + ", name :" + user.name + ", score :" + user.point);
        }
    }

    public void SetRanking(int uid, string uname, int upoint)
    {
        if(uid > MAX_RANK)
        {
            Debug.LogError("set rank error. " + MAX_RANK + " < " + uid);
        }

        StartCoroutine(SetRankingData(uid, uname, upoint));
    }

    IEnumerator SetRankingData(int uid, string uname, int upoint)
    {
        DBUsers updateData = new DBUsers();
        updateData.id = uid;
        updateData.name = uname;
        updateData.point = upoint;

        string updateDataJson = JsonUtility.ToJson(updateData);

        WWWForm form = new WWWForm();

        form.AddField("user", updateDataJson);

        using (WWW www = new WWW(setRanking_url, form))
        {
            yield return www;
            if(!string.IsNullOrEmpty(www.error))
            {
                Debug.LogError("error : " + www.error);
                yield break;
            }
            Debug.Log("id :" + updateData.id + "name :" + updateData.name + "point :" + updateData.point);
        }
    }
}
