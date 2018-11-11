using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.2f;

    [SerializeField]
    private float jampPower = 130.0f;

    private Rigidbody playerRigid = null;

    private bool isGround = false;

    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
    }


    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * moveSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += transform.right * moveSpeed * -1;
        }

        if (isGround != false && Input.GetKeyDown(KeyCode.Space))
        {
            playerRigid.AddForce(transform.up * jampPower);
        }
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGround = false;
    }
}
