﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMelee : MonoBehaviour
{
    //public GameObject[] Bullet;
    [Range(0.0f, 10.0f)]
    public float meleeRange=3;
    public LayerMask enemyLayer;
    private void Start()
    {
    }
    private void Update()
    {
        //MeleeAttack();
    }
    public void MeleeAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, meleeRange, enemyLayer);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<FollowPlayerFly>().TakeDmg(50);
        }
    }
    private void OnDrawGizmos()
    {
        if (transform.position==null)
            return;
        Gizmos.DrawWireSphere(transform.position,meleeRange);
    }
}
