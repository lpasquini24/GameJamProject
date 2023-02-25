using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class TiedObjectManager : MonoBehaviour
{
	[SerializeField] private UnityEvent TiedObjectEvent;

	[Header("Movement")]
	[SerializeField, Tooltip("The number of times this object will trigger events")] private int numberOfTriggers = 1;

	[SerializeField] private bool movementIsDirectional = false;

	[Header("Directional")]
	[SerializeField] private float directionalMovementAmount;

    [Header("Exact")]
    [SerializeField] private Vector3 exactMovementAmount;
	[SerializeField] private float movementSpeed = 1;

	private Vector3 locationToMoveTo;
	private Vector3 directionalVector;
	private int triggerCounter = 1;

    private void Start() => locationToMoveTo = transform.position;

    public void TouchingPlayer(Collision2D _collision)
	{
		if (!_collision.gameObject.CompareTag("Player")) return;

		directionalVector = GetDirectionOnContact(_collision.relativeVelocity);
		TriggerTiedObjectEvent();
    }

	public Vector2 GetDirectionOnContact(Vector2 _direction)
	{
		Vector3 _fourDirectionalVector;

		bool _movingHorizontal = Mathf.Abs(_direction.x) > Mathf.Abs(_direction.y);

        if (_movingHorizontal)
			_fourDirectionalVector = _direction.x > 0 ? Vector2.right : Vector2.left;

        else 
			_fourDirectionalVector = _direction.y > 0 ? Vector2.up : Vector2.down; 

        return _fourDirectionalVector;
	}

	public void TriggerTiedObjectEvent()
	{
		if (triggerCounter > numberOfTriggers)
		{
			var rbs = GetComponentsInChildren<Rigidbody2D>();
			Array.ForEach(rbs, rb => rb.bodyType = RigidbodyType2D.Static); 
			return;
		}

		if(movementIsDirectional)
			locationToMoveTo = (directionalMovementAmount * directionalVector) + transform.position;

		else
			locationToMoveTo = transform.position + exactMovementAmount;

		TiedObjectEvent?.Invoke();

		triggerCounter++;
	}

	private void FixedUpdate()
	{
		Vector3 _nextPosition = Vector2.Lerp(transform.position, locationToMoveTo, movementSpeed);
        transform.position = _nextPosition;

    }
}
