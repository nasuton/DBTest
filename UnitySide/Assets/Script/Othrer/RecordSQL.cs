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
    #region Singleton

    private static RecordSQL mInstance = null;

    public static RecordSQL Instance
    {
        get
        {
            if(mInstance == null)
            {
                GameObject obj = new GameObject("NetworkManger");
                DontDestroyOnLoad(obj);
                obj.AddComponent<RecordSQL>();
                mInstance = obj.GetComponent<RecordSQL>();
                mInstance.Init();
            }

            return mInstance;
        }
    }

    #endregion

    //DBランキングを取得してくる
    [SerializeField]
    private string getRanking_url = "http://localhost/getRecord.php";

    //DBのデータを更新する
    [SerializeField]
    private string setRanking_url = "http://localhost/setRecord.php";

    [SerializeField]
    private const int MAX_RANK = 10;

    //ランキング作成時に使用する
    private List<DBUsers> usersList = new List<DBUsers>();

    private bool isInit = false;

    public void Init()
    {
        if(isInit == true)
        {
            return;
        }

        //ここで初期化時に行いたいことを書く

        isInit = true;
    }

    /// <summary>
    /// DBからランキングを取得してくる
    /// </summary>
    /// <returns>
    /// ランキングのリスト
    /// </returns>
    public List<DBUsers> GetRanking()
    {
        usersList.Clear();

        for(int i = 1; i < 11; i++)
        {
            StartCoroutine(GetRankingData(i));
        }

        return usersList;
    }

    public void CheckRank(string uname, int upoint)
    {
        //ポイントを判断して超えているものがあれば更新するようにする
        int tmpPoint = 0;
        string tmpName = "";

        for(int i = 0; i < usersList.Count; i++)
        {
            if(usersList[i].point < upoint)
            {
                tmpPoint = usersList[i].point;
                tmpName = usersList[i].name;
                usersList[i].point = upoint;
                usersList[i].name = uname;
                upoint = tmpPoint;
                uname = tmpName;
            }

            //TODO::もしポイントが同じだった場合かつ10位まで続いていた場合は、
            //      保存した日にちで判断して古いものから削除して新しいものを保存するようにしたい
            SetRanking(usersList[i].id, usersList[i].name, usersList[i].point);
            Debug.Log("set id :" + usersList[i].id + ", set name :" + usersList[i].name + ", set point :" + usersList[i].point);
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
            usersList.Add(user);
        }
    }

    private void SetRanking(int uid, string uname, int upoint)
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
        }
    }
}
