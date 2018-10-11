using UnityEngine;
using UnityEngine.UI;

public class Player_BaseState : MonoBehaviour
{
    [SerializeField]
    private Text hpText = null;

    [SerializeField]
    private int player_hp = 100;

    // Use this for initialization
    void Start()
    {
        hpText.text = player_hp.ToString();
    }

    public void Player_Damage(int dm)
    {
        player_hp -= dm;
        if(player_hp <= 0)
        {
            player_hp = 0;
        }

        hpText.text = player_hp.ToString();
    }

}
