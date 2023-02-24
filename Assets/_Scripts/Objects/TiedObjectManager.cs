using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TiedObjectManager : MonoBehaviour
{
	[SerializeField] private UnityEvent TiedObjectEvent;

	//[Title("Movement")]
	[SerializeField, Tooltip("The number of times this object will trigger events")] private int numberOfTriggers = 1;

	[SerializeField] private bool movementIsDirectional = false;
	//[ShowIf("@movementIsDirectional")]
	[SerializeField] private float directionalMovementAmount;
	//[HideIf("@movementIsDirectional")]
	[SerializeField] private Vector3 exactMovementAmount;
	[SerializeField] private float movementSpeed = 1;


	private Vector3 locationToMoveTo;
	private Vector3 directionalVector;
	private bool triggerMovement = false;
	private int triggerCounter = 1;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		directionalVector = GetDirectionOnContact(collision.gameObject);
		TriggerTiedObjectEvent();
	}

	public Vector2 GetDirectionOnContact(GameObject collision)
	{
		Vector3 _direction = (collision.transform.position - transform.position).normalized;
		Vector3 _fourDirectionalVector;

		if (_direction.x > 0) _fourDirectionalVector = new Vector2(1, _direction.y);
		else _fourDirectionalVector = new Vector2(-1, _direction.y);

		if (_direction.y > 0) _fourDirectionalVector = new Vector2(_fourDirectionalVector.x, 1);
		else _fourDirectionalVector = new Vector2(_fourDirectionalVector.x, -1);

		return _fourDirectionalVector;
	}

	public void TriggerTiedObjectEvent()
	{
		if(triggerCounter > numberOfTriggers) return;

		triggerMovement = true;

		if(movementIsDirectional)
			locationToMoveTo = (directionalMovementAmount * directionalVector) + transform.position;
		else
			locationToMoveTo = transform.position + exactMovementAmount;

		TiedObjectEvent?.Invoke();

		triggerCounter++;
	}

	private void FixedUpdate()
	{
		if (triggerMovement)
			transform.position = Vector2.Lerp(transform.position, locationToMoveTo, movementSpeed);
	}
}
