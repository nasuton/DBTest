using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryObject : MonoBehaviour {

    [SerializeField]
    private float destroyTime = 1.0f;

	// Use this for initialization
	void Awake ()
    {
        Destroy(gameObject, destroyTime);
	}
}
