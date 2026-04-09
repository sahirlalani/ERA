using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpell : MonoBehaviour
{
    PlayerController player;
    Rigidbody2D SpellBody;

    public enum Attribute
    {
        Fire,
        Ice,
        Wind,
        Lightning,
        Wood,
        Poison
    }


    public GameObject AudioManager;
    public AudioClip _SkillSound;
    public GameObject CollisionParticle;

    public float ManaCost;
    public float SkillPower;
    public float SkillSpeed;
    public float SkillCooldown = 0f;
    public float SkillDuration;
    private float RemainingCooldown;
    private float SkillActiveFor;

    public int Spell_Director = 1;


    public void Start()
    {
        player = FindObjectOfType<PlayerController>();
        //player.SpellCost(ManaCost);
        
        //audioManager.PlaySFX("Fireball");
        AudioSource.PlayClipAtPoint(_SkillSound, transform.position);
        //AudioSource.Play(_SkillSound);

        SpellBody = GetComponent<Rigidbody2D>();

        SkillActiveFor =  0;
        
        RemainingCooldown = SkillCooldown;
    }
    
    // Update is called once per frame
    public void Update()
    {
        CheckSkillActiveTime();
                
        //AudioSource.PlayClipAtPoint(_movingSound, transform.position);
    }

    /*
    public bool CanCast()
    {
        if (SkillCooldown)
    }
    */

    public void FixedUpdate()
    {
        transform.position += transform.right * SkillSpeed * Time.deltaTime * Spell_Director;

    }

    public void CheckSkillActiveTime()
    {
        if (SkillActiveFor >= SkillDuration)
        {
            Destroy(this.gameObject);
        }
        else if (SkillActiveFor < SkillDuration)
        {
            SkillActiveFor +=  Time.deltaTime;
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        //AudioSource.PlayClipAtPoint(_collisionSound, transform.position);
        
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(SkillPower);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(SkillPower);
            Debug.Log("Hit Enemy.");
        }

       GameObject _spawnedEffect = Instantiate(CollisionParticle, collision.gameObject.transform.position, collision.gameObject.transform.rotation, collision.gameObject.transform);
       Destroy (_spawnedEffect, 1f); 

       Destroy(this.gameObject);
        
    }
}
