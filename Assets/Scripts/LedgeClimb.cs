using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeClimb : MonoBehaviour
{
    [SerializeField] Transform _transform;
    [SerializeField] GameObject Player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.transform.position = _transform.position;
            Player.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
        }
    }
}
