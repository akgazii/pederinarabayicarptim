using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 4f;
    private float baseSpeed = 2f; 
    private float jumpingPower = 6f;
    private bool isFacingRight = true;

    private int jumpStreak = 0;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

       
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }

       
        if (Input.GetButton("Jump") && IsGrounded() && rb.velocity.y <= 0f)
        {
            Jump();

            
            jumpStreak++;
            speed = baseSpeed + (jumpStreak * 0.3f);
        }

       
        if (!Input.GetButton("Jump") && IsGrounded())
        {
            jumpStreak = 0;
            speed = baseSpeed;
        }

       
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.3f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
