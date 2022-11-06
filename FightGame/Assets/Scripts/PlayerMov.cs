using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

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
    public bool FirstTap;
    public bool FirstClick;
    public Animator anim;
    public TextMesh Nickname;
    public GameObject DamageRange;
    public int Vida = 0;
    #endregion

    void FixedUpdate()
    {
        if(!hasAuthority)
        {
            return;
        }


        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (FirstTap == true)
                {
                    jump();
                    FirstTap = false;
                    anim.SetBool("Pulo", true);
                    StartCoroutine(SpaceRecharge());
                }
            }
            else if (Input.GetKey(KeyCode.A))
            {
                if (inAir == false)
                {
                    rigid.velocity = Vector3.forward * moveSpeed;
                    flip = true;
                    anim.SetFloat("Velocidade", 0.1f);
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
                    anim.SetFloat("Velocidade", 0.1f);
                    Rotate();

                }
                else
                {
                    if (flip == true)
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
            else
            {
                anim.SetFloat("Velocidade", 0.0f);
            }

            if (inAir == false)
            {
                doublejump = true;

            }
            if (inAir == true)
            {
                rigid.velocity += Physics2D.gravity.y * (Vector3.up * Time.deltaTime)/2;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                Dash();
                StartCoroutine(DashRecharge());
            }

            if (Input.GetKey(KeyCode.Mouse1))
            {
                if (FirstClick == true)
                {
                    anim.SetTrigger("Tapa1");
                    FirstClick = false;
                    StartCoroutine(MouseRecharge());
                }
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (FirstClick == true)
                {
                    anim.SetTrigger("Tapa2");
                    FirstClick = false;
                    StartCoroutine(MouseRecharge());
                }
            }

            if (Input.GetKey(KeyCode.Mouse2))
            {
                if (FirstClick == true)
                {
                    anim.SetTrigger("Super");
                    FirstClick = false;
                    StartCoroutine(MouseRecharge());
                }
            }
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

    private void Dash()
    {
        if (dash_Ok)
        {
            if (flip)
            {
                rigid.AddForce(new Vector3(0, 0, dashpower), ForceMode.Impulse);
                dash_Ok = false;
                anim.SetTrigger("Dash");
            }

            if (!flip)
            {
                rigid.AddForce(new Vector3(0, 0, -dashpower), ForceMode.Impulse);
                dash_Ok = false;
                anim.SetTrigger("Dash");
            }
        }
    }

    IEnumerator DashRecharge()
    {
        if (inAir)
        {
            yield return new WaitForSeconds(2);
            dash_Ok = true;
        }

        if(inAir == false)
        {
            yield return new WaitForSeconds(1);
            dash_Ok = true;

        }

    }

    IEnumerator SpaceRecharge()
    {
        yield return new WaitForSeconds(0.2f);
        FirstTap = true;
    }

    private void Start()
    {
        FirstTap = true;
        FirstClick = true;
        DamageRange.SetActive(false);
    }

    IEnumerator MouseRecharge()
    {
        Cmd_AtivaBola(true);
        yield return new WaitForSeconds(0.8f);
        FirstClick = true;
        Cmd_AtivaBola(false);
    }

    [Command]
    public void Cmd_AtivaBola(bool ativo)
    {
        Rpc_AtivaBola(ativo);
    }

    [ClientRpc]
    public void Rpc_AtivaBola(bool ativo)
    {
        DamageRange.SetActive(ativo);
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
                anim.SetBool("Pulo", false);
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
    
    private void OnTriggerEnter(Collider Damage)
    {
        if (!hasAuthority || !Damage.CompareTag("Damage")) 
        {
            return;
        }
        float posBola = (transform.position.z > Damage.transform.position.z) ? 1 : -1;
        Vida += 10;
        rigid.AddForce(new Vector3(0, Vida / 8, Vida / 5 * posBola), ForceMode.Impulse);
    }
}