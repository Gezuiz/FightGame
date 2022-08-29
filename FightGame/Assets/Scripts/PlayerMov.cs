using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMov : NetworkBehaviour
{

    public float moveSpeed;
    public float jumpHeight;
    public Rigidbody rigid;
    public bool inAir = false;


    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if(inAir == false)
            {
                rigid.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
            }
           
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if(inAir == false)
            {
                rigid.velocity = new Vector2(-moveSpeed, 0);
            }
            else
            {
                rigid.velocity = new Vector2(-moveSpeed, -2);
            }

        }
        else if (Input.GetKey(KeyCode.D))
        {

            if (inAir == false)
            {
                rigid.velocity = new Vector2(moveSpeed, 0);
            }
            else
            {
                rigid.velocity = new Vector2(moveSpeed, -2);
            }
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        {
            if (collision.gameObject.tag == "Ground")
            {
                inAir = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        {
            if (collision.gameObject.tag == "Ground")
            {
                inAir = false;
            }
        }
    }



}