using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConectSQL : MonoBehaviour
{
    //結果を格納するテキスト
    public Text resultText;

    //idを入力するインプットフィールド
    public Text inputText;

    //selecttest.phpを指定
    //今回のアドレスはlocalhost
    public string serverAddress = "localhost/selecttest.php";

    //SendSignalボタンを押した際に実行する
    public void SendButton_Action()
    {
        if(inputText.text == null)
        {
            Debug.LogError("text is null.");
        }
        //コルーチン開始
        StartCoroutine("Access");
    }

    private IEnumerator Access()
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();

        //インプットフィールドからidの取得
        //複数phpに送信したいデータがある場合は今回の場合dic.Add("hoge", value)のように足していけばよい
        dic.Add("id", inputText.GetComponent<Text>().text);

        StartCoroutine(Post(serverAddress, dic));

        yield return 0;
    }

    private IEnumerator Post(string url, Dictionary<string, string> post)
    {
        WWWForm form = new WWWForm();

        foreach(KeyValuePair<string, string> post_arg in post)
        {
            form.AddField(post_arg.Key, post_arg.Value);
        }

        WWW www = new WWW(url, form);

        //TimeOutSecond = 3s
        yield return StartCoroutine(CheckTimeOut(www, 3.0f));

        if (www.error != null)
        {
            //そもそも接続ができていないとき
            Debug.LogError("HttpPost NG :" + www.error);
        }
        else if (www.isDone)
        {
            //送られてきたデータをテキストに反映
            resultText.GetComponent<Text>().text = www.text;
        }
    }

    private IEnumerator CheckTimeOut(WWW www, float timeout)
    {
        float requestTime = Time.time;

        while(!www.isDone)
        {
            if(Time.time - requestTime < timeout)
            {
                yield return null;
            }
            else
            {
                Debug.Log("TimeOUT!!");
                /*
                 * ここにタイムアウト処理を書く
                 */

                break;
            }
        }

        yield return null;
    }

}
