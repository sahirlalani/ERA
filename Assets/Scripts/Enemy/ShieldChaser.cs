using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldChaser : Enemy
{
    public Transform trans;
    public float BounceBack = 10;
    protected override void Start()
    {
        base.Start();
        trans = GetComponent<Transform>();
    }

    public override void UpdateEnemy()
    {
        base.UpdateEnemy();

        SeekPlayer(GetDirToPlayer());
        //Vector3 dir = GetDirToPlayer();
        //dir.y = 0;
        //_rb.AddForce(dir.normalized * _enemySpeed, ForceMode2D.Impulse);
    }

    public override void OnPlayerCollision(PlayerController playerController)
    {
        Vector3 dir = GetDirToPlayer();
        dir.y = 0;
        //_rb.velocity = dir.normalized * BounceBack * -1;
        _rb.AddForce(dir.normalized * _enemySpeed * BounceBack * -1, ForceMode2D.Impulse);
    }
}
