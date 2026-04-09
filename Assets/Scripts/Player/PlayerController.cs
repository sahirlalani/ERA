using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    public Transform trans;

    //public GameObject UI;
    public UI UpperUI;

    public float MovementSpeed, jumpForce;
    bool isGrounded;

    //Magic spell and variables 
    public GameObject MagicCircle;
    public MagicCircle magicCircle;
    public GameObject FireBall;
    public GameObject IceShard;
    public GameObject HealSpell;
    public Transform MagicPoint;
    public bool IsCasting = false;
    //public bool CanCast = true;

    //Different weapon states
    //public GameObject SwordsmanMode;
    //public GameObject MagicianMode;
    //public GameObject NothingMode;


    //Setting health values & mana values & exp values
    public float InvicibleCooldown = 1f;
    private float CurrentInvicibleCooldown;
    private float ManaPool = 100.0f;
    private float MaxHp = 100.0f;
    private float ExpNeeded = 1000.0f;
    float CurrentExp = 0f;
    public float CurrentMana;
    public float CurrentHp;
    private float RegenHealthCooldown = 15f;
    private float RegenManaCooldown = 15f;
    private float selfHealAmount = 8f;
    //private float healSpellCooldown = 15f;
    private float HealSpellManaCost = 10f;
    
    private int PlayerLevel = 1;

    public bool LeftDir = false;
    bool jumpInput;

    public static PlayerController MainPlayer;

    public void Awake()
    {
        Debug.Log("I am awake.");
        MainPlayer = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        //MagicCircle magicCircle = MagicCircle.GetComponent<MagicCircle>();
        trans = GetComponent<Transform>();
        UI UpperUI = GetComponent<UI>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentInvicibleCooldown -= Time.deltaTime;        
        float InputMovement  = Input.GetAxis("Horizontal");


        //Debug.Log(InputMovement);

        if (InputMovement > 0)
        {
            LeftDir = false;
            SpriteDir();
            //Debug.Log("Going Right.");
        }
        if (InputMovement < 0)
        {
            LeftDir = true;
            SpriteDir();
            //Debug.Log("Going Left");
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
        {
            jumpInput = true;
            //Debug.Log("Pressed W");
        }
        else
        {
            jumpInput = false;
        }

        _rb.velocity = new Vector2(InputMovement * MovementSpeed, _rb.velocity.y);

        /*if (IsCasting == false)
        {
            StartCoroutine(CastSpell());
        }*/

        if (Input.GetKeyUp(KeyCode.F) && IsCasting == false && CurrentMana >= FireBall.GetComponent<MagicSpell>().ManaCost)
        {
            StartCoroutine(CastingFireball());
        }
        else if(Input.GetKeyUp(KeyCode.Q) && IsCasting == false && CurrentMana >= IceShard.GetComponent<MagicSpell>().ManaCost)
        {
            StartCoroutine(CastingIceShard());
        }
        else if (Input.GetKeyUp(KeyCode.E) && IsCasting == false && CurrentMana >= HealSpellManaCost)
        {
            HealSpellSkill();
        }

        CheckExp();        
        RegenHealth();
        RegenMana();

        UpperUI.UpdateExp(CurrentExp, ExpNeeded);
        UpperUI.UpdateHealth(CurrentHp, MaxHp);
        UpperUI.UpdateMana(CurrentMana, ManaPool);
    }
/*
    public IEnumerator CastSpell()
    {
        float waitTime = 2f;
        IsCasting = true;
        MagicCircle.SetActive(true);

        if (Input.GetKey(KeyCode.F) && CurrentMana >= FireBall.GetComponent<MagicSpell>().ManaCost)
        {
            MagicCircle.GetComponent<MagicCircle>().FireMagicCircle();
            SpellCost(FireBall.GetComponent<MagicSpell>().ManaCost);
            GameObject _spawnedFireball = Instantiate(FireBall, MagicCircle.transform.position, MagicCircle.transform.rotation, MagicPoint);
            //waitTime += fireballWaitTimeOrSometing;
        }
        else if (Input.GetKey(KeyCode.T) && CurrentMana >= IceShard.GetComponent<MagicSpell>().ManaCost)
        {
            magicCircle.IceMagicCircle();
            SpellCost(IceShard.GetComponent<MagicSpell>().ManaCost); 
            GameObject _spawnedIceShard = Instantiate(IceShard, MagicCircle.transform.position, MagicCircle.transform.rotation, MagicPoint);
            waitTime += IceShard.GetComponent<MagicSpell>().SkillCooldown; 
        }  
        else
        {
            
        }
        yield return new WaitForSeconds(2);
        MagicCircle.SetActive(false);
        magicCircle.ResetMagicCircle();
        IsCasting = false;
        yield return new WaitForSeconds(waitTime - 2);
    }

    */
    void FixedUpdate()
    {
        if (jumpInput && isGrounded)
        {
            Jump();
        }
    }

    void SpriteDir()
    {
        if (LeftDir == true)
        {
            trans.localScale = new Vector3(-1, 1, 1);
            //this.gameObject.transform.scale = -1;
        }
        else
        {
            //trans.scale = 1;
            trans.localScale = new Vector3(1, 1, 1);
        }
    }

    void Jump()
    {
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false; 
        Debug.Log("I jumped");
    }
    public IEnumerator CastingFireball()
    {
        IsCasting = true;
        MagicCircle.GetComponent<MagicCircle>().FireMagicCircle();
        MagicCircle.SetActive(true);
        SpellCost(FireBall.GetComponent<MagicSpell>().ManaCost);
        GameObject _spawnedFireball = Instantiate(FireBall, MagicCircle.transform.position, MagicCircle.transform.rotation);
        _spawnedFireball.GetComponent<MagicSpell>().Spell_Director = LeftDir ? -1 : 1; 

        yield return new WaitForSeconds(2f);
        MagicCircle.SetActive(false);
        magicCircle.ResetMagicCircle();
        IsCasting = false;

        yield return new WaitForSeconds(FireBall.GetComponent<MagicSpell>().SkillCooldown - 2);
        
    }
    private IEnumerator CastingIceShard()
    {
        IsCasting = true;
        magicCircle.IceMagicCircle();
        MagicCircle.SetActive(true);
        SpellCost(IceShard.GetComponent<MagicSpell>().ManaCost); 
        GameObject _spawnedIceShard = Instantiate(IceShard, MagicCircle.transform.position, MagicCircle.transform.rotation);
        _spawnedIceShard.GetComponent<MagicSpell>().Spell_Director = LeftDir ? -1 : 1; 


        yield return new WaitForSeconds(IceShard.GetComponent<MagicSpell>().SkillCooldown);

        MagicCircle.SetActive(false);
        magicCircle.ResetMagicCircle();
        IsCasting = false;
    }

    public void HealSpellSkill()
    {
        GameObject _spawnedHealParticle = Instantiate(HealSpell, this.gameObject.transform.position, this.gameObject.transform.rotation, this.gameObject.transform);
        Destroy(_spawnedHealParticle, 3f);
        HealDamage(selfHealAmount);
        SpellCost(HealSpellManaCost);
    }
    

    public void ResetHealth()
    {
        CurrentHp = MaxHp;
    }

    public void RegenHealth()
    {
        if (RegenHealthCooldown <= 0 && CurrentHp < MaxHp)
        {
            CurrentHp += 1;
            RegenHealthCooldown = 15f;
        }
        else 
        {
            RegenHealthCooldown -= Time.deltaTime;
        }
    }

    public void ResetMana()
    {
        CurrentMana = ManaPool;
    }

    public void RegenMana()
    {
        if (RegenManaCooldown <= 0 && CurrentMana < ManaPool)
        {
            CurrentMana += 1;
            RegenManaCooldown = 15f;
        }
        else 
        {
            RegenManaCooldown -=  Time.deltaTime;
        }
    }

    public void TakeDamage(float damageValue)
    {
        if (CurrentInvicibleCooldown < 0)
        {
            CurrentHp -= damageValue;
            CurrentInvicibleCooldown = InvicibleCooldown;
        }
    }

    public void HealDamage(float healAmount)
    {
        CurrentHp += healAmount;
        
        if (CurrentHp > MaxHp)
        {
            ResetHealth();
        }
    }

    public void SpellCost(float ManaUsed)
    {
        if (ManaUsed > CurrentMana)
        {
            return;
        }
        else 
        {
            CurrentMana -= ManaUsed;
        }
        //CurrentMana -= ManaUsed;
    }

    public void CheckExp()
    {
        if (CurrentExp >= ExpNeeded && PlayerLevel < 5)
        {
            PlayerLevelUp();
        }
        else if (CurrentExp >= ExpNeeded && PlayerLevel == 5)
        {
            CurrentExp = ExpNeeded;
        }
    }

    public void EarnExp(float ExpToAdd)
    {
        CurrentExp += ExpToAdd;
    }

    public void PlayerLevelUp()
    {
        MaxHp +=  10;
        ManaPool +=  10;

        ResetHealth();
        ResetMana();

        CurrentExp = 0;
        ExpNeeded += 1000;

        PlayerLevel += 1;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dummy")
        {
            TakeDamage(5f);
        }
        for (int i  = 0; i < collision.contacts.Length; i++)
        {
            if (collision.contacts[i].normal.y > 0.5)
            {
                isGrounded = true;
            }
        }
    }

}
