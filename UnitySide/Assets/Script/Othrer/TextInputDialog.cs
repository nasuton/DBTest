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
        RecordSQL.Instance.CheckRank(inputText.text, 300);
        this.gameObject.SetActive(false);
    }
}
