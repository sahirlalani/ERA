using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalProjectile : MonoBehaviour
{
    PlayerController player;
    Rigidbody2D SpellBody;

    public float damage, speed, projectileDuration;
    private float activeDuration = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

        public void FixedUpdate()
    {
        transform.position += transform.right * speed * Time.deltaTime;

    }

    public void CheckSkillActiveTime()
    {
        if (activeDuration >= projectileDuration)
        {
            Destroy(this.gameObject);
        }
        else if (activeDuration < projectileDuration)
        {
            activeDuration +=  Time.deltaTime;
        }
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        //AudioSource.PlayClipAtPoint(_collisionSound, transform.position);
        
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Debug.Log("Hit Enemy.");
        }

        //GameObject _spawnedEffect = Instantiate(CollisionParticle, collision.gameObject.transform.position, collision.gameObject.transform.rotation, collision.gameObject.transform);
        //Destroy (_spawnedEffect, 1f); 

        Destroy(this.gameObject);
        
    }
}
