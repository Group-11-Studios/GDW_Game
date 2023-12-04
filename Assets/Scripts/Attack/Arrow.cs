using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;
    public bool isArrow;

    public int damage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && isArrow)
        {
            other.GetComponent<EnemyControler>().health -= damage;
        }
        if(other.gameObject.tag == "Player" && !isArrow)
        {
            other.GetComponent<PlayerControler>().health -= damage;
        }
        Destroy(gameObject);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb.velocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

}
