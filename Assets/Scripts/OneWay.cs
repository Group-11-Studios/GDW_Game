using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWay : MonoBehaviour
{
    [SerializeField] Transform _transform;
    [SerializeField] GameObject Player;
    public bool _IsUpper;
    public bool playerInUpper;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_IsUpper)
        {
            if (collision.gameObject.tag == "Player")
            {
                Player.transform.position = new Vector3(Player.transform.position.x, _transform.position.y);
                Player.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
            }
        }
        else
        {
            if (collision.gameObject.tag == "Player")
            {
                playerInUpper = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_IsUpper)
        {
            if (collision.gameObject.tag == "Player")
            {
                playerInUpper = false;
            }
        }
    }


}
