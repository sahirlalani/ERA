using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinArcher : Enemy
{
    public Transform trans;
    public Transform arrowCreationPoint;
    public GameObject ArrowPrefab;
    public Vector2 JumpCooldown, ArrowShootCooldown, JumpRange;
    public float JumpHeight;
    private float RemainingJumpCooldown, RemainingArrowShootCooldown;

    protected override void Start()
    {
        base.Start();
        trans = GetComponent<Transform>();
        //InvokeRepeating("Shoot");
    }
    public override void UpdateEnemy()
    {
        base.UpdateEnemy();

        RemainingJumpCooldown -= Time.deltaTime;
        RemainingArrowShootCooldown -= Time.deltaTime;

        if (RemainingJumpCooldown < 0)
        {
            RemainingJumpCooldown = Random.Range(JumpCooldown.x, JumpCooldown.y);
            _rb.AddForce (new Vector2 (Random.Range(JumpRange.x, JumpRange.y), JumpHeight), ForceMode2D.Impulse);
        }

        if (RemainingArrowShootCooldown < 0)
        {
            RemainingArrowShootCooldown = Random.Range(ArrowShootCooldown.x, ArrowShootCooldown.y);
            Instantiate(ArrowPrefab, trans.position, Quaternion.Euler(0, 0, 180));
            //InvokeRepeating ("ShootArrow", 0f, RemainingArrowShootCooldown);
        }
    }

    public void ShootArrow()
    {
        Instantiate(ArrowPrefab, trans.position, Quaternion.identity);
    }
}
