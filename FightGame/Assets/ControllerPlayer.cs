using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror
{
    public class ControllerPlayer : NetworkBehaviour
    {
        public CharacterController controller;
        public float speed;
        public Rigidbody player;
        public float jumpHeight;

        private void FixedUpdate()
        {
            if (isLocalPlayer)
            {
                float horizontal = Input.GetAxis("Horizontal");
                Vector3 direction = new Vector3(horizontal, 0f, 0f);

                if(direction.magnitude >= 0.1f)
                {
                    controller.Move(direction * speed * Time.deltaTime);
                }
                if (Input.GetKey("space"))
                {
                    player.AddForce(transform.up * jumpHeight);
                }
            }
            
        }

    }
}
