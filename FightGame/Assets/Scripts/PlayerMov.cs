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
    public bool doublejump = true;
    public int dashpower;
    public bool dash_Ok = true;
    public bool FirstClick;
    #endregion

    void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.Space))
        {
            if(FirstClick == true)
            {
                jump();
                FirstClick = false;
                StartCoroutine(SpaceRecharge());
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if(inAir == false)
            {
                rigid.velocity = Vector3.forward * moveSpeed;
                flip = true;
                Rotate();
            }
            else
            {
                if (flip == false)
                {
                    rigid.velocity += Vector3.forward * moveSpeed * 10 * Time.deltaTime;
                }
                else
                {
                    rigid.velocity += Vector3.forward * moveSpeed * 5 * Time.deltaTime;
                }
                flip = true;
                Rotate();
            }

        }
        else if (Input.GetKey(KeyCode.D))
        {

            if (inAir == false)
            {
                rigid.velocity = Vector3.back * moveSpeed;
                flip = false;
                Rotate();
                
            }
            else
            {
                if(flip == true)
                {
                    rigid.velocity += Vector3.back * moveSpeed * 10 * Time.deltaTime;
                }
                else
                {
                    rigid.velocity += Vector3.back * moveSpeed * 5 * Time.deltaTime;
                }
                flip = false;
                Rotate();
            }
        }
        if (inAir == false)
        {
            doublejump = true;

        }
        if (inAir == true)
        {
            rigid.velocity += Vector3.up * Physics2D.gravity.y * (2.5f - 1) * Time.deltaTime;
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
            transform.eulerAngles = new Vector3(0, 0, 0); // Flipped
        }

        if (!flip)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void jump()
    {
        if (inAir == false)
        {
            rigid.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);

        }
        if (inAir == true && doublejump == true)
        {
            rigid.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
            doublejump = false;
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

    IEnumerator SpaceRecharge()
    {
        yield return new WaitForSeconds(0.2f);
        FirstClick = true;
    }

    private void Start()
    {
        FirstClick = true;
        
    }

}