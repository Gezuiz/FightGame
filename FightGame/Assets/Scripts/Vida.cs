using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public int health = 100;

    public Transform hitbox;;
    public float hitboxRadius;
    public LayerMaks damageSource;
    private bool isHit;

    private bool hitCooldown = false;

    public Rigidbody2D rb;
    public float knockbackForce = 10;
    public float knockbackForceUp = 2;
  
    void Update()
    {
        isHit = Physics2D.OverlapCircle(hitbox.position, hitboxRadius, damageSource);
        
        if(isHit ==  true && hitCooldown == false)
        {
            hitCooldown = true;
            Invoke("Cooldown", 0.8f);
            health = health - 25;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void knockBack()
    {
        Transform attacker = getClosestDamageSource();
        Vector3 knockbackDirection = new Vector3(tranform.position.x - attacker.transform.position.x, 0);
    }
}
