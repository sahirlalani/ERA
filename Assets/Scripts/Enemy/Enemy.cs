using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Enemy Rigidbody
    protected Rigidbody2D _rb;

    public PlayerController playerController;

    //Max health value
    public float _maxHealth = 10.0f;
    public float KilledExp = 30f;
    //Current health value
    public float _currentHealth = 10.0f;
    //Enemy Death sound
    public AudioClip _deathSound;

    //Enemy Blood particle
    public GameObject _bloodParticle;

    //Store direction to player per frame
    Vector3 _dirToPlayer;
    //Was direction calculated this frame already
    bool _calculatedThisFrame = false;

    //Enemy Speed
    public float _enemySpeed = 5.0f;
    //Enemy damage
    public float _contactDamage = 0.5f;

    public static bool EnemyEnabled = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Reset health to max
        ResetHealth();
        playerController = PlayerController.MainPlayer;
    }

    // Update is called once per frame
    private void Update()
    {
        if (EnemyEnabled == false) { return; }
        UpdateEnemy();
    }

    private void LateUpdate()
    {
        _calculatedThisFrame = false;
    }

    public virtual void UpdateEnemy()
    {
        //kills enemy if dead
        KillIfDead();
    }

    public void ResetHealth()
    {
        //Reset health value
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        //Inflict damage upon enemy
        _currentHealth -= damage;

        SpawnBloodEffect();
    }

    public void HealDamage(float heal)
    {
        _currentHealth += heal;

        //check if overhealed
        if (_currentHealth >= _maxHealth)
        {
            //cap at max
            _currentHealth = _maxHealth;
        }
    }

    public void KillIfDead()
    {
        if (CheckIfDead())
        {
            //Destroy object
            Destroy(gameObject);
            playerController.EarnExp(KilledExp);
        }
    }

    public bool CheckIfDead()
    {
        //Check if health leass than or equal to 0
        if (_currentHealth <= 0.0f)
        {
            //enemy dead
            return true;
        }

        return false;
    }

    public void SeekPlayer(Vector3 dirToPlayer)
    {
        //Move towards player direction
        _rb.velocity = dirToPlayer.normalized * _enemySpeed;
    }

    public void FleePlayer(Vector3 dirToPlayer)
    {
        _rb.velocity = dirToPlayer.normalized * -_enemySpeed;
    }

    public Vector3 GetDirToPlayer()
    {
        //Direction to player
        Vector3 dirToPlayer;

        if (!_calculatedThisFrame)
        {
            //Get player and calculate direction
            dirToPlayer = playerController.trans.position - transform.position;

            _dirToPlayer = dirToPlayer;
            //Was calculated this frame
            _calculatedThisFrame = true;
        }
        else
        {
            //just grab the the calculated value
            dirToPlayer = _dirToPlayer;
        }

        return dirToPlayer;
    }

    public float GetDistToPlayer()
    {
        return GetDirToPlayer().magnitude;
    }

    private void SpawnBloodEffect()
    {
        //Spawn blood at point
        GameObject spawnedBlood = Instantiate(_bloodParticle, this.gameObject.transform.position, this.gameObject.transform.rotation, this.gameObject.transform);
        //Destroy blood after 1 second
        Destroy(spawnedBlood, 1.0f);
    }

    public virtual void OnPlayerCollision(PlayerController playerController)
    {
        
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //Do contact damage to player overtime
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(_contactDamage);

            OnPlayerCollision(player);
            Debug.Log("On Player Collision activated.");
        }

        //if (collision.gameObject.layer == LayerMask.NameToLayer("EnemySkill"))
        //{
            //Destroy(collision.gameObject);
        //}

        if (collision.gameObject.tag == "Skill")
        {
            Debug.Log("Skill hit me.");
        }
    }

}
