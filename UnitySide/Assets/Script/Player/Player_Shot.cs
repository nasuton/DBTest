using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shot : MonoBehaviour {

    [SerializeField]
    private GameObject bullet1_pre = null;

    [SerializeField]
    private GameObject muzzlePos = null;

    [SerializeField]
    private float bulletSpeed = 100.0f;

    [SerializeField]
    private float waitShotTime = 0.5f;

    private void Start()
    {
        StartCoroutine("PlayerShotAction");
    }

    private IEnumerator PlayerShotAction()
    {
        while(true)
        {
            if (Input.GetKey(KeyCode.W))
            {
                GameObject bullets = Instantiate(bullet1_pre) as GameObject;

                Vector3 shotForce;

                shotForce = gameObject.transform.forward * bulletSpeed;

                bullets.GetComponent<Rigidbody>().AddForce(shotForce);

                bullets.transform.position = muzzlePos.transform.position;
            }

            yield return new WaitForSeconds(waitShotTime);
        }
    }

}
