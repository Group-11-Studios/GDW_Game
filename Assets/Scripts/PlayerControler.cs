using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] Transform _transform;
    [SerializeField] Transform attackPos;
    [SerializeField] Transform CrushPos;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Transform attackPoints;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] GameObject fist;
    [SerializeField] GameObject Attackanim;
    [SerializeField] GameObject bowanim;

    [SerializeField] GameObject Idle;
    [SerializeField] GameObject Movement;

    [SerializeField] GameObject lose;
    [SerializeField] GameObject winner;
    [SerializeField] TMP_Text time;
    [SerializeField] GameObject endScreen;

    [SerializeField] GameObject healthBar;

    private Vector2 input;
    public float playerSpeed = 8.0f;
    public float jumpForce;
    public float jumpForceAir;

    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;
    public LayerMask EnemyLayer;

    public float health = 200;

    bool end;

    private float swordCool;
    private float crushCool;
    private float bowCool;

    bool doubleJump = false;
    float waitTime = 0;

    bool facingRight = true;
    float lastDash = 0.6f;
    bool moving;

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
            Idle.SetActive(false);
            Movement.SetActive(true);
            Attackanim.SetActive(false);
            bowanim.SetActive(false);
            moving = true;
        }
        else if (input.x < 0)
        {
            facingRight = false;
            attackPoints.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            Idle.SetActive(false);
            Movement.SetActive(true);
            Attackanim.SetActive(false);
            bowanim.SetActive(false);
            moving = true;
        }
        else
        {
            Idle.SetActive(true);
            Movement.SetActive(false);
            Attackanim.SetActive(false);
            bowanim.SetActive(false);
            moving = false;
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
            Movement.SetActive(false);
            Idle.SetActive(false);
            Attackanim.SetActive(true);
            bowanim.SetActive(false);
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

        if (Time.time >= crushCool + 1.5)
        {
            fist.SetActive(true);
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(CrushPos.position, 3, EnemyLayer);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyControler>().health -= 25;
            }
         

            crushCool = Time.time;
        }
    }

    public void OnBow()
    {
        if (Time.time >= bowCool + 0.1)
        {
            if (Input.GetMouseButton(1))
            {
                Movement.SetActive(false);
                Idle.SetActive(false);
                Attackanim.SetActive(false);
                bowanim.SetActive(true);
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


    public void GameOver(bool win)
    {
        end = true;
        endScreen.SetActive(true);
        if (win)
        {
            winner.SetActive(true);
            lose.SetActive(false);
        }
        else
        {
            winner.SetActive(false);
            lose.SetActive(true);
        }

        float endTime = Time.time;

        int mins = Mathf.FloorToInt(endTime / 60);
        int secs = Mathf.FloorToInt(endTime % 60);

        
        time.text = mins.ToString() + ":" + secs.ToString();
    }


    private void Update()
    {
        if (crushCool + 0.5 <= Time.time)
        {
            fist.SetActive(false);
        }

        if (swordCool + 0.2 <= Time.time && Attackanim.activeSelf)
        {
            if (moving)
            {
                Idle.SetActive(false);
                Movement.SetActive(true);
                Attackanim.SetActive(false);
                bowanim.SetActive(false);
            }
            else
            {
                Idle.SetActive(true);
                Movement.SetActive(false);
                Attackanim.SetActive(false);
                bowanim.SetActive(false);
            }
        }
        if (bowCool + 0.2 <= Time.time && bowanim.activeSelf)
        {
            if (moving)
            {
                Idle.SetActive(false);
                Movement.SetActive(true);
                Attackanim.SetActive(false);
                bowanim.SetActive(false);
            }
            else
            {
                Idle.SetActive(true);
                Movement.SetActive(false);
                Attackanim.SetActive(false);
                bowanim.SetActive(false);
            }
        }


        if (health <= 0)
        {
            
            if (!end)
            {
                end = true;
                GameOver(false);
                healthBar.SetActive(false);
            }
        }
        else
        {
            healthBar.transform.localScale = new Vector3(health * 0.192f, 1, 1);
        }

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
