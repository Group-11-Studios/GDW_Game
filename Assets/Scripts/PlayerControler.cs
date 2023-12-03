using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] Transform _transform;
    [SerializeField] Transform attackPos;
    [SerializeField] Transform CrushPos;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Transform attackPoints;
    [SerializeField] GameObject arrowPrefab;

    private Vector2 input;
    public float playerSpeed = 8.0f;
    public float jumpForce;
    public float jumpForceAir;

    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;
    public LayerMask EnemyLayer;

    private float swordCool;
    private float crushCool;
    private float bowCool;

    bool doubleJump = false;
    float waitTime = 0;

    bool facingRight = true;
    float lastDash = 0.6f;
    

    void FixedUpdate()
    {
        // Left Right Movement
        if (lastDash > 0.6) // check if dashing if yes stop movement
        {
            _transform.position += new Vector3(input.x / 100 * playerSpeed, 0 * playerSpeed);
        }
    }

    public void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
        if (input.y > 0)
        {
            OnJump();
        }
        else if (input.y < 0)
        {
            // Call Crouch Func Here
        }

        // Update facing direction
        if (input.x > 0)
        {
            facingRight = true;
            attackPoints.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (input.x < 0)
        {
            facingRight = false;
            attackPoints.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }


    }


    public void OnJump()
    {
        // Check if player can jump (is on the ground or has jumped once and has not landed)
        bool check = IsGrounded();
        if (check && doubleJump)
        {
            // Normal Jump From Ground
            _rb.AddForce(Vector2.up * jumpForce);
        }
        else if (check)
        {
            // Double Jump From Air (Reduced Power)
            _rb.AddForce(Vector2.up * jumpForceAir);
        }
    }

    public void OnDash()
    {
        if (facingRight && dashCoolDown())
        {
            _rb.AddForce(new Vector2(500, 0));
            lastDash = 0;
        }
        else if (!facingRight && dashCoolDown())
        {
            _rb.AddForce(new Vector2(-500, 0));
            lastDash = 0;
        }
    }

    public bool dashCoolDown()
    {
        if (lastDash >= 0.6)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Checks if player is on the ground or can double jump returns true if yes
    bool IsGrounded()
    {
        if(Physics2D.BoxCast(_transform.position, boxSize, 0, -_transform.up, maxDistance, layerMask)) // Checks if on ground
        {
            return true;
        }
        else if (doubleJump) // if not on ground checks if can double jump
        {
            doubleJump = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    void DashAttack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(_transform.position, 1, EnemyLayer);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<EnemyControler>().health -= 20 * Time.deltaTime;
        }
    }

    public void OnSword()
    {
        if (Time.time >= swordCool + 0.15)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, 2, EnemyLayer);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyControler>().health -= 10;
            }
            swordCool = Time.time;
        }
    }

    public void OnCrush()
    {

        if (Time.time >= crushCool + 0.4)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(CrushPos.position, 3, EnemyLayer);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyControler>().health -= 15;
            }
            crushCool = Time.time;
        }
    }

    public void OnBow()
    {
        if (Time.time >= bowCool + 0)
        {
            if (Input.GetMouseButton(1))
            {
                GameObject arrow = Instantiate(arrowPrefab, attackPos.position, Quaternion.identity);
                if (facingRight)
                {
                    arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(20, 0.0f);
                }
                else
                {
                    arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(-20, 0.0f);
                }
                Destroy(arrow, 15);
            }
            bowCool = Time.time;
        }
    }




    private void Update()
    {
        if (lastDash < 0.6)
        {
            if (_rb.velocity == new Vector2(0, _rb.velocity.y) && lastDash >= 0.1)
            {
                lastDash = 0.6f;
            }

            lastDash += Time.deltaTime;

            DashAttack();

            if (lastDash >= 0.6)
            {
                _rb.velocity = new Vector3(0, _rb.velocity.y);
            }
        }

        if (!doubleJump)  // Checks if player landed on ground after double jump
        {
            if (waitTime >= 1)  // waits 1 second before checking
            {
                if (Physics2D.BoxCast(_transform.position, boxSize, 0, -_transform.up, maxDistance, layerMask))
                {
                    doubleJump = true;
                    waitTime = 0;
                }
            }
            else
            {
                waitTime += Time.deltaTime;
            }
            
        }

    }
}
