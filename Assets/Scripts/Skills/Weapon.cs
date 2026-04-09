using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Animator attackAnimation;

    public AudioClip ClashingSound;
    public AudioClip WeaponMovingSound;

    public float WeaponRange;
    public float WeaponDamage;
    public float WeaponRefractionDamage;

    public enum EquipementType
    {
        Sword,
        Spear,
        Axe,
        Bow,
        Armor
    }

    public EquipementType WeaponName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void OnCollisionEnter2D(Collision2D coll)
}
