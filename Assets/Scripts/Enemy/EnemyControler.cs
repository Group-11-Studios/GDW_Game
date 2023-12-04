using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    [SerializeField] GameObject magicPrefab;

    [SerializeField] GameObject idle;
    [SerializeField] GameObject move;

    public float health;
    GameObject player;

    public bool isRange;
    float lastAttack;

    public LayerMask whatIsPlayer;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }



    private void FixedUpdate()
    {
        if (isRange)
        {
            if (lastAttack <= Time.time - 1)
            {

                if (player.transform.position.x <= transform.position.x + 10 && player.transform.position.x >= transform.position.x - 10 && player.transform.position.y <= transform.position.y + 2 && player.transform.position.y >= transform.position.y - 2)
                {
                    GameObject arrow = Instantiate(magicPrefab, transform.position, Quaternion.identity);
                    if (transform.rotation == Quaternion.Euler(0, 0, 0))
                    {
                        arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(20, 0.0f);
                    }
                    else
                    {
                        arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(-20, 0.0f);
                    }
                    Destroy(arrow, 15);

                }
                lastAttack = Time.time;
            }

        }
        else
        {
            if (player.transform.position.x <= transform.position.x + 6 && player.transform.position.x >= transform.position.x - 6 && player.transform.position.y <= transform.position.y + 2 && player.transform.position.y >= transform.position.y - 2)
            {
                if (player.transform.position.x <= transform.position.x + 1 && player.transform.position.x >= transform.position.x - 1)
                {
                    move.SetActive(false);
                    idle.SetActive(true);
                }
                else if (transform.rotation == Quaternion.Euler(0, 0, 0))
                {
                    transform.position += new Vector3(0.06f, 0);
                    move.SetActive(true);
                    idle.SetActive(false);
                }
                else
                {
                    transform.position -= new Vector3(0.06f, 0);
                    move.SetActive(true);
                    idle.SetActive(false);
                }

            }
            else
            {
                move.SetActive(false);
                idle.SetActive(true);
            }
            if (lastAttack <= Time.time - 1)
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, 2, whatIsPlayer);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<PlayerControler>().health -= 5;
                }
                lastAttack = Time.time;
            }
        }
    }

    void Update()
    {

   


        if (transform.position.x < player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
