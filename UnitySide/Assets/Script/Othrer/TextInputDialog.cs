using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInputDialog : MonoBehaviour
{
    [SerializeField]
    private Text inputText = null;

    [SerializeField]
    private Button decisionButton = null;

    [SerializeField]
    private int limitNum = 8;

    private void Start()
    {
        decisionButton.onClick.AddListener(DecisionDown);
    }

    public void DecisionDown()
    {
        if(inputText.text == "")
        {
            Debug.LogError("何も入力されていません");
            return;
        }

        //int nowScore = ScoreCount.Instance.GetScore();
        RecordSQL.Instance.CheckRank(inputText.text, 310);
        StartCoroutine(SendResult());
    }

    IEnumerator SendResult()
    {
        yield return new WaitForSeconds(3.0f);

        this.gameObject.SetActive(false);
    }
}
