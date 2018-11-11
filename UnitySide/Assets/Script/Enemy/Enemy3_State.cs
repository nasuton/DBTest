using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3_State : MonoBehaviour
{
    //playerの位置
    [SerializeField]
    private GameObject targetPos = null;

    [SerializeField]
    private GameObject[] muzzlePos = new GameObject[3];

    [SerializeField]
    private GameObject bullet_Pre = null;

    [SerializeField]
    private float rotationSpeed = 10.0f;

    [SerializeField]
    private float bulletSpeed = 100.0f;

    [SerializeField]
    private float shotWaitTime = 0.1f;

    void Start()
    {
        targetPos = GameObject.Find("player");

        StartCoroutine("Enemy3_Action");
        StartCoroutine("Enemy3_Rotation");
    }

    IEnumerator Enemy3_Action()
    {
        while (true)
        {

            Enemy3_ShotAction();

            yield return new WaitForSeconds(shotWaitTime);
        }
    }

    IEnumerator Enemy3_Rotation()
    {
        while(true)
        {

            Enemy3_LookTarget();

            yield return 0;
        }
    }

    //ターゲットに向けて連射で打ってくる
    void Enemy3_ShotAction()
    {
        for (int i = 0; i < muzzlePos.Length; i++)
        {
            GameObject bullets = Instantiate(bullet_Pre) as GameObject;

            Vector3 shotForce;

            shotForce = gameObject.transform.forward * bulletSpeed;

            bullets.GetComponent<Rigidbody>().AddForce(shotForce);


            bullets.transform.position = muzzlePos[i].transform.position;
        }
    }

    void Enemy3_LookTarget()
    {
        var newRotation = Quaternion.LookRotation(targetPos.transform.position - transform.position).eulerAngles;
        newRotation.x = 0;
        newRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRotation), rotationSpeed * Time.deltaTime);
    }
}
