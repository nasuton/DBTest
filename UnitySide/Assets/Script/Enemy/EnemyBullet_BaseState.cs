using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet_BaseState : MonoBehaviour
{
    [SerializeField]
    private int bulletDamage = 2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player_BaseState>().Player_Damage(bulletDamage);
            Destroy(gameObject);
        }
    }
}
