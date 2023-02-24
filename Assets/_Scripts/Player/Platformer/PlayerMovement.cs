using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed;
    public float acceleration;

    [Range(0, 1)]
    public float deccelerationConstant;

    [Header("Jump")]
    public float jumpHeight;
    public float coyoteTime;
    public float jumpQueueTime;

    [Header("Collisions")]
    public LayerMask groundMask;
    public Rect groundedBoxBounds;

    private Rigidbody2D rb;
    private float jumpQueue = 0f;
    private float coyoteTimer = 0f;

    private Vector2 playerDirection;

	// Start is called before the first frame update
	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //horizontal movement
        float xVel = rb.velocity.x;
        
        float xAccel = playerDirection.x * acceleration * Time.deltaTime;
        if (playerDirection.x == 0)
        {
            xVel *= deccelerationConstant;
        }
        else
        {
            xVel += xAccel;
            if (xVel > maxSpeed)
            {
                xVel = maxSpeed;
            }
            else if (xVel < -maxSpeed)
            {
                xVel = -maxSpeed;
            }
        }
        rb.velocity = new Vector2(xVel, rb.velocity.y);

		//jump
		Vector2 point = new Vector2(transform.position.x + groundedBoxBounds.x, transform.position.y + groundedBoxBounds.y);
		Vector2 size = new Vector2(groundedBoxBounds.width, groundedBoxBounds.height);
		bool isGrounded = Physics2D.OverlapBox(point, size, 0f, groundMask);
		if (isGrounded)
		{
			coyoteTimer = 0f;
		}
		else
		{
			coyoteTimer += Time.deltaTime;
		}

		jumpQueue += Time.deltaTime;

	}

	public void Jump()
    {
		jumpQueue = 0;
		if (coyoteTimer < coyoteTime && jumpQueue < jumpQueueTime)
		{
			rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
			jumpQueue = -0.2f;
		}

	}

    public void SetDirection(Vector2 direction)
    {
        playerDirection = direction;

	}

	private void OnDrawGizmos()
    {
        Vector3 point = new Vector3(groundedBoxBounds.x, groundedBoxBounds.y, 0f);
        Vector3 size = new Vector3(groundedBoxBounds.width, groundedBoxBounds.height, 1f);
        Gizmos.DrawCube(transform.position + point, size);
    }
}
