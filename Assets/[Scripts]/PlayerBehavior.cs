using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Movement Properties")]
    public float speed = 10.0f;
    public float horizontalForce = 10.0f;
    public float verticalForce = 1.0f;
    public float airFactor = 0.5f;
    public float groundRadius;

    public Transform groundPoint;
    public LayerMask groundLayerMask;

    public bool isGrounded;

    [Header("Animations")]
    public Animator animator;

    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groundLayerMask);
        Move();
        Jump();
        AirCheck();
    }

    public void Move()
    {
        var x = Input.GetAxisRaw("Horizontal");

        if (x != 0.0f)
        {
            Flip(x);

            rb2D.AddForce(Vector2.right * ((x > 0.0f) ? 1.0f : -1.0f) * horizontalForce * ((isGrounded) ? 1 : airFactor));

            rb2D.velocity = Vector2.ClampMagnitude(rb2D.velocity, speed);
            animator.SetInteger("AnimationState", 1);
        }

        if (isGrounded && x == 0)
        {
            animator.SetInteger("AnimationState", 0);
        }
    }

    public void Jump()
    {
        var y = Input.GetAxisRaw("Jump");
        if (isGrounded && y > 0.0f)
        {
            rb2D.AddForce(Vector2.up * verticalForce, ForceMode2D.Impulse);
        }
    }

    public void AirCheck()
    {
        if (!isGrounded)
        {
            animator.SetInteger("AnimationState", 2);
        }
    }

    public void Flip(float x)
    {
        if (x != 0)
            transform.localScale = new Vector3((x > 0.0f) ? 1.0f : -1.0f, 1.0f, 1.0f);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundPoint.position, groundRadius);
    }
}
