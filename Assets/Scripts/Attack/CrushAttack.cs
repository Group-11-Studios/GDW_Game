using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushAttack : MonoBehaviour
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
            if (Input.GetKeyDown(KeyCode.E))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyControler>().health -= damage;
                }
            }
        }
        else
        {
            timeBetweenAttacks -= Time.deltaTime;
        }

        void OnDrawGizmoSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPos.position, attackRange);
        }
    }
}
