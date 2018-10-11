using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_State : MonoBehaviour
{
    //playerの位置
    [SerializeField]
    private GameObject targetPos = null;

    [SerializeField]
    private float rotationSpeed = 10.0f;

    [SerializeField]
    private float moveSpeed = 0.01f;

    [SerializeField]
    private float bulletSpeed = 100.0f;

    [SerializeField]
    private float shotWaitTime = 0.1f;

    [SerializeField]
    private float changeTime = 20.0f;

    [SerializeField]
    private GameObject muzzlePos = null;

    [SerializeField]
    private GameObject bullet_Pre = null;

    private float changeCountTime = 0.0f;

    void Start()
    {
        targetPos = GameObject.Find("player");

        StartCoroutine("Enemy2_Action");
        StartCoroutine("Enemy2_Rotation");
    }

    IEnumerator Enemy2_Action()
    {
        while(true)
        {
            changeCountTime += Random.Range(0.1f, 3.0f);

            if(changeTime <= changeCountTime)
            {
                Enemy2_ShotAction();
                if (changeTime * 2.0f <= changeCountTime)
                {
                    changeCountTime = 0.0f;
                }
                yield return new WaitForSeconds(shotWaitTime);
            }
            else
            {
                Enemy2_Move();
            }

            float distance = Vector3.Distance(transform.position, targetPos.transform.position);

            if(distance <= 0.5f)
            {
                StartCoroutine("ShotActionOnly");
                yield break;
            }

            yield return 0;
        }
    }

    IEnumerator Enemy2_Rotation()
    {
        while(true)
        {
            Enemy2_LookTarget();

            yield return 0;
        }
    }

    IEnumerator ShotActionOnly()
    {
        while(true)
        {
            Enemy2_ShotAction();

            yield return new WaitForSeconds(shotWaitTime);
        }
    }

    void Enemy2_Move()
    {
        //ターゲットに向かって進む処理
        transform.position += transform.forward * moveSpeed;
    }

    void Enemy2_ShotAction()
    {
        GameObject bullets = Instantiate(bullet_Pre) as GameObject;

        Vector3 shotForce;

        shotForce = gameObject.transform.forward * bulletSpeed;

        bullets.GetComponent<Rigidbody>().AddForce(shotForce);

        bullets.transform.position = muzzlePos.transform.position;
    }

    void Enemy2_LookTarget()
    {
        var newRotation = Quaternion.LookRotation(targetPos.transform.position - transform.position).eulerAngles;
        newRotation.x = 0;
        newRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRotation), rotationSpeed * Time.deltaTime);
    }
}
