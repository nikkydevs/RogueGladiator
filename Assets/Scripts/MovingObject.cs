using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour
{
    private Rigidbody2D rb2D;

    protected Vector2 velocity;

    protected virtual void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        velocity = new Vector2(horizontalInput, verticalInput);
        velocity.Normalize();
    }

    protected virtual void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);
    }

//     protected virtual Vector2 GetVelocity()
//     {
        
//     }
}
