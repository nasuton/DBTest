using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase_State : MonoBehaviour
{
    [SerializeField]
    private int enemy_hp = 100;

    [SerializeField]
    private int enemy_score = 10;

    private int sponseNumber = 0;

    public void Enemy_Damage(int dm)
    {
        enemy_hp -= dm;
        if(enemy_hp <= 0)
        {
            Enemy_Dath();
        }
    }

    public void SetSponseNumber(int num)
    {
        sponseNumber = num;
    }

    private void Enemy_Dath()
    {
        ResponseController.Instance.SetSponseUsing(sponseNumber, false);
        ScoreCount.Instance.SetScore(enemy_score);
        Destroy(gameObject);
    }
}
