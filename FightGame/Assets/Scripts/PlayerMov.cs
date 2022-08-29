using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMov : NetworkBehaviour
{
    #region Variaveis
    public float moveSpeed;
    public float jumpHeight;
    public Rigidbody rigid;
    public bool inAir = false;
    public bool flip;
    public int doublejump = 2;
    public int dashpower;
    public bool dash_Ok = true;
    #endregion

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if(inAir == false)
            {
                rigid.velocity = new Vector2(-moveSpeed, 0);
                flip = true;
                Rotate();
                
            }
            else
            {
                rigid.velocity = new Vector2(-moveSpeed, -2);
                flip = true;
                Rotate();
            }

        }
        else if (Input.GetKey(KeyCode.D))
        {

            if (inAir == false)
            {
                rigid.velocity = new Vector2(moveSpeed, 0);
                flip = false;
                Rotate();
                
            }
            else
            {
                rigid.velocity = new Vector2(moveSpeed, -2);
                flip = false;
                Rotate();
            }
        }
        if (inAir == false)
        {
            doublejump = 2;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (dash_Ok)
            {
                if (flip)
                {
                    rigid.AddForce(new Vector3(-dashpower, 0, 0), ForceMode.Impulse);
                    dash_Ok = false;
                }

                if (!flip)
                {
                    rigid.AddForce(new Vector3(dashpower, 0, 0), ForceMode.Impulse);
                    dash_Ok = false;
                }
            }
            
            StartCoroutine(Dash());
            
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

    private void Rotate()
    {
        if (flip)
        {
            transform.eulerAngles = new Vector3(0, 180, 0); // Flipped
        }

        if (!flip)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void jump()
    {
        if (inAir == false)
        {
            rigid.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);

        }
        if (inAir == true && doublejump > -1)
        {
            rigid.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
            doublejump = -1;
        }
    }

    IEnumerator Dash()
    {
        if (inAir)
        {
            yield return new WaitForSeconds(1);
            dash_Ok = true;
        }

        if(inAir == false)
        {
            yield return new WaitForSeconds(0.5f);
            dash_Ok = true;

        }

    }

}