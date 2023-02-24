using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerInputManager : MonoBehaviour
{
	public void OnMove(InputValue value)
	{
		foreach (PlayerMovement move in movements)
		{
			move.SetDirection(value.Get<Vector2>());
		}
	}
	public void OnFire(InputValue value) => DoJumps();

	public Vector2 playerDirection;

	private PlayerMovement[] movements;

	private void DoJumps()
	{
		foreach(PlayerMovement move in movements)
		{
			move.Jump();
		}
	}

	private void Start()
	{
		movements = GetComponentsInChildren<PlayerMovement>();

	}

}
