using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private float timeBetweenAttacks;
    private float startTimeBetweenAttacks;

    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage;

    void Update()
    {
        if (timeBetweenAttacks <= 0)
        {
            timeBetweenAttacks = startTimeBetweenAttacks;
            if (Input.GetMouseButtonDown(0))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().health -= damage;
                }
            }
        }
        else
        {
            timeBetweenAttacks -= Time.deltaTime;
        }

        void OnDrawGizmoSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(attackPos.position, attackRange);
        }
    }
}
