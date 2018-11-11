using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BulletState : MonoBehaviour
{
    [SerializeField]
    private int bulletDamage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Enemy")
        {
            other.GetComponent<EnemyBase_State>().Enemy_Damage(bulletDamage);
            Destroy(gameObject);
        }
    }

}