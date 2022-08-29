using UnityEngine;
using System.Collections;
using Mirror;

public class playerMovement : NetworkBehaviour
{

    public float moveSpeed;
    public float jumpHeight;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            GetComponent<Rigidbody>().velocity = new Vector2(0, jumpHeight);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            GetComponent<Rigidbody>().velocity = new Vector2(-moveSpeed, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponent<Rigidbody>().velocity = new Vector2(moveSpeed, 0);
        }
    }
}