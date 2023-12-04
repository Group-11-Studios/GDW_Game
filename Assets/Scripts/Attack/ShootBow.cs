using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootBow : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float arrowSpeed;
    public float arrowTimer;
    public int damage;
    public LayerMask whatIsEnemies;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(arrowSpeed, 0.0f);
            Destroy(arrow, arrowTimer);
        }


    }
}
