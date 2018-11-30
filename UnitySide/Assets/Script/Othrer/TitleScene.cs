using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    private void Update()
    {
        //Enterキーが押された場合は、ランキングシーンへ
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("RankingScene");
        }

        //Spaceキーが押された場合は、ゲームスタート
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("GameMainScene");
        }
    }
}
