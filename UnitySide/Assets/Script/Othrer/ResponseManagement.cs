using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseManagement : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemys = new GameObject[3];

    [SerializeField]
    private float[] enemy_percent = new float[3];

    [SerializeField]
    private Transform[] responsePos = new Transform[5];

    [SerializeField]
    private float responseWaitTime = 10.0f;

    private Dictionary<GameObject, float> enemyDict = new Dictionary<GameObject, float>();

    private List<int> useSponse = new List<int>();

    private int responseNumber = 0;

    void Start()
    {
        for (int i = 0; i < responsePos.Length; i++)
        {
            useSponse.Add(i);
        }

        for(int i = 0; i < enemys.Length; i++)
        {
            if(enemys[i] == null)
            {
                Debug.LogError("変数 enemys" +  i + "番目はnullです。追加してください");
            }

            if(enemy_percent[i] <= 0.0f)
            {
                Debug.LogWarning("変数 enemy_percent" + i + "番目が0以下の値が入っています。");
            }

            enemyDict.Add(enemys[i], enemy_percent[i]);
        }

        SoundManager.Instance.PlayBGM("MainBGM", true, 1.0f);
        
        StartCoroutine("EnemyResponse");
    }

    private int ResponseNumberSelect()
    {
        int useNum = 0;

        for(int i = 0; i < responsePos.Length; i++)
        {
            if(ResponseController.Instance.GetSponseUsing(i) == false)
            {
                useSponse.Add(i);
            }
        }
        
        if(useSponse.Count > 0)
        {
            int selectNum = Random.Range(0, useSponse.Count);

            useNum = useSponse[selectNum];

            ResponseController.Instance.SetSponseUsing(useNum, true);

            useSponse.Clear();

            return useNum;
        }

        //要素がなかった場合は-1を返す
        return -1;
    }


    IEnumerator EnemyResponse()
    {
        GameObject enemySelect = null;

        while(true)
        {
            yield return new WaitForSeconds(responseWaitTime);

            //intの場合の戻り値が min <= 戻り値 < max となっているため注意
            responseNumber = ResponseNumberSelect();

            //もし-1が返ってきた場合は、それ以降の処理をしないようにする
            if(responseNumber != -1)
            {
                enemySelect = Probability.DetermineFromDict(enemyDict);

                GameObject responseEnemy = Instantiate(enemySelect) as GameObject;

                responseEnemy.GetComponent<EnemyBase_State>().SetSponseNumber(responseNumber);

                responseEnemy.transform.position = responsePos[responseNumber].position;
            }
        }
    }

}
