using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy1_State : MonoBehaviour
{
    private GameObject targetPos = null;

    [SerializeField]
    private GameObject muzzlePos = null;

    [SerializeField]
    private GameObject bullet_Pre = null;

    [SerializeField]
    private float shotWaitTime = 0.1f;

    [SerializeField]
    private float rotationSpeed = 10.0f;

    [SerializeField]
    private float bulletSpeed = 100.0f;

    private Vector3 shotRotation = Vector3.zero;

    void Start()
    {
        transform.DOMoveY(1.0f, 1.0f);

        targetPos = GameObject.Find("player");

        StartCoroutine("TargetLookAt");
    }

    IEnumerator TargetLookAt()
    {
        yield return new WaitForSeconds(1.0f);

        var newRotation = Quaternion.LookRotation(targetPos.transform.position - transform.position).eulerAngles;
        newRotation.z = 0.0f;

        transform.DORotate(newRotation, 1.0f);

        yield return new WaitForSeconds(2.0f);

        StartCoroutine("Enemy1_ShotAction");
        StartCoroutine("Enemy1_RotateAction");
    }

    IEnumerator Enemy1_ShotAction()
    {
        while(true)
        {
            //現在の正面に球を打つ
            GameObject bullets = Instantiate(bullet_Pre) as GameObject;
            Vector3 shotForce;
            shotForce = gameObject.transform.forward * bulletSpeed;
            bullets.GetComponent<Rigidbody>().AddForce(shotForce);
            bullets.transform.position = muzzlePos.transform.position;
            shotRotation.x = 90.0f;
            bullets.transform.rotation = Quaternion.Euler(shotRotation);

            yield return new WaitForSeconds(shotWaitTime);
        }
    }

    IEnumerator Enemy1_RotateAction()
    {
        while(true)
        {
            //プレイヤーの方を向く回転の動き
            var newRotation = Quaternion.LookRotation(targetPos.transform.position - transform.position).eulerAngles;
            shotRotation = newRotation;
            newRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRotation), rotationSpeed * Time.deltaTime);

            yield return 0;
        }
    }

}
