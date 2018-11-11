using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Probability
{
    //--------------------------------------------------------
    // 真偽を判定
    //--------------------------------------------------------
    
    /// <summary>
    /// 入力した確率で判定する(int型)
    /// 結果はfloat型の方からもらってくる
    /// </summary>
    public static bool DetectFromPercent(int percent)
    {
        return DetectFromPercent((float)percent);
    }

    /// <summary>
    /// 入力した確率で判定する(flaot型)
    /// </summary>
    public static bool DetectFromPercent(float percent)
    {
        //小数点以下の桁数を求める
        int digitNum = 0;
        if(percent.ToString().IndexOf(".") > 0)
        {
            digitNum = percent.ToString().Split('.')[1].Length;
        }

        //小数点以下をなくすように乱数の上限と判定の境界を上げる
        //上記の処理から得た桁数から、小数点を消すための倍率を求める
        int rate = (int)Mathf.Pow(10, digitNum);

        //乱数の上限と真と判定するボーダーを設定
        int randomValueLimit = 100 * rate;
        int border = (int)(rate * percent);

        //パターンはrandomValueLimit個、
        //その中で真を返す数はborder個なので、
        //border / randomValueLimitの倍率になる
        return Random.Range(0, randomValueLimit) < border;
    }

    //--------------------------------------------------------
    // 複数の中から一つを選択
    //--------------------------------------------------------

    // Tの部分に入るのはstringでもGameObjectでも大丈夫です
    // また、確率の合計が100以上でも以下でも問題ない
    // これは確率ではなく重みだからです

    /// <summary>
    /// 入力したDictから一つを決定し、そのDictのkeyを返す(int型)
    /// 結果はfloat型の方からもらってくる
    /// </summary>
    public static T DetermineFromDict<T>(Dictionary<T, int> targetDict)
    {
        Dictionary<T, float> targetFloatDict = new Dictionary<T, float>();

        foreach(KeyValuePair<T, int> pair in targetDict)
        {
            targetFloatDict.Add(pair.Key, (float)pair.Value);
        }

        return DetermineFromDict(targetFloatDict);
    }

    /// <summary>
    /// 入力したDictから一つを決定し、そのDictのkeyを返す(float型)
    /// </summary>
    public static T DetermineFromDict<T>(Dictionary<T, float> targetDict)
    {
        //累計確率
        //引数に渡されているDictionaryのfloatをすべて足す
        float totalPer = 0.0f;
        foreach(float per in targetDict.Values)
        {
            totalPer += per;
        }

        //0～累計確率を上限とした乱数を作成
        float rand = Random.Range(0, totalPer);

        //上記で設定した乱数から
        //各確率を引いていき、0以下になった時の対象が抽選される
        foreach(KeyValuePair<T, float> pair in targetDict)
        {
            rand -= pair.Value;

            if(rand <= 0)
            {
                return pair.Key;
            }
        }

        //ここまで来たらエラーを返す
        Debug.LogError("抽選ができませんでした");
        return new List<T>(targetDict.Keys)[0];
    }
}
