using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseController : MonoBehaviour
{
    private static ResponseController mInstance;

    [SerializeField]
    private bool[] sponsePosUsingList = new bool[5];

    public static ResponseController Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject obj = new GameObject("ResponseControllerObj");
                mInstance = obj.AddComponent<ResponseController>();
            }
            return mInstance;
        }
        set
        {

        }
    }

    private void Awake()
    {
        for(int i = 0; i < sponsePosUsingList.Length; i++)
        {
            sponsePosUsingList[i] = false;
        }
    }

    public void SetSponseUsing(int num, bool usingflag)
    {
        if(sponsePosUsingList.Length - 1 < num)
        {
            Debug.LogError("set sponse num over.");
        }

        sponsePosUsingList[num] = usingflag;
    }

    public bool GetSponseUsing(int num)
    {
        if(sponsePosUsingList.Length - 1 < num)
        {
            Debug.LogError("get sponse num over.");
        }

        return sponsePosUsingList[num];
    }
}
