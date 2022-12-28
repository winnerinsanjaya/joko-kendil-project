using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public Transform groundCheck;
    public LayerMask groundLayer;

    private bool isFacingright = true;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float moveSpeed;

    float moveDirection;

    [SerializeField]
    private float jumpAmount;



    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        if(!isFacingright && moveDirection > 0f)
        {

            Flip();
        }
        else if (isFacingright && moveDirection < 0f)
        {
            Flip();
        }
    }


    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        isFacingright = !isFacingright;
        Vector3 localscale = transform.localScale;
        localscale.x *= -1f;
        transform.localScale = localscale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.AddForce(transform.up * jumpAmount, ForceMode2D.Impulse);
        }
    }
}
