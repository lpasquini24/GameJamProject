using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
	// Objects
	private Rigidbody2D rb;

	//[Title("Movement")]
	[SerializeField] public float maxSpeed = 5;
	[SerializeField] public float speedMultiplier = 1;
	[SerializeField] public float slipperyness = 0.9f;
	[SerializeField] public float deceleration = 0;

	private void Awake() => rb = GetComponent<Rigidbody2D>();

	public void MovePlayer(Vector2 direction)
	{
		// Add movement
		rb.velocity = Vector2.Lerp(rb.velocity, direction * (maxSpeed * speedMultiplier), 1f - slipperyness);
		rb.velocity -= deceleration * Time.fixedDeltaTime * rb.velocity;
	}

	public void StopMovement()
	{
		rb.velocity = Vector2.zero;
	}

}
