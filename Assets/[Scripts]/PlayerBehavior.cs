using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float speed = 10.0f;
    public float horizontalForce = 10.0f;
    public float verticalForce = 1.0f;

    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        float y = Input.GetAxisRaw("Jump") * Time.deltaTime;
        Vector2 horizontalVector = Vector2.zero;
        Vector2 verticalVector = Vector2.zero;

        if (x != 0.0f)
        {
            Flip(x);

            //transform.position += new Vector3(y, 0.0f);

            horizontalVector = Vector2.right * ((x > 0.0f) ? 1.0f : -1.0f) * horizontalForce;


            rb2D.AddForce(horizontalVector);

            rb2D.velocity = Vector2.ClampMagnitude(rb2D.velocity, speed);
        }
        if (y > 0.0f)
        {
            verticalVector = Vector2.up * verticalForce;
            rb2D.AddForce(verticalVector);
        }

    }

    public void Flip(float x)
    {
        if (x != 0)
            transform.localScale = new Vector3((x > 0.0f) ? 1.0f : -1.0f, 1.0f, 1.0f);
    }
}
