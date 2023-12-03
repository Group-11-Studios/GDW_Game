using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    public float health;
    private float healthOld;
    // Start is called before the first frame update
    void Start()
    {
        healthOld = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if (health != healthOld) 
        {
            Debug.Log(health);
        }
        healthOld = health;
    }
}
