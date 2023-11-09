using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] Transform _transform;
    [SerializeField] Rigidbody2D _rb;

    private Vector2 input;
    public float playerSpeed = 8.0f;
    public float jumpForce;
    public float jumpForceAir;

    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;

    bool doubleJump = false;
    float waitTime = 0;

    

    void FixedUpdate()
    {
        // Left Right Movement
        _transform.position += new Vector3(input.x / 100 * playerSpeed, 0 * playerSpeed);
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

    private void Update()
    {
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
