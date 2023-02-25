using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
	//[Title("Selected Player")]
	public UnityEvent SwitchPlayer;
	private TopDownMovement selectedPlayer;
	private int selectedPlayerCount = 0;
	private TopDownMovement[] players;

	// Movement
	private Vector2 movementDirection;

	// Getters
	public TopDownMovement SelectedPlayer { get => selectedPlayer; }
    public TopDownMovement[] Players { get => players; }


    // Startup
    // ----------------
    private void Start()
	{
        players = GetComponentsInChildren<TopDownMovement>();
		selectedPlayer = Players[0];
	}


	// Loop
	// ----------------
	private void FixedUpdate() => UpdateMovement();


	// Methods
	// ----------------
	private void UpdateMovement()
	{
		selectedPlayer.MovePlayer(movementDirection);
	}

	private void TriggerSwitchPlayer()
	{
		selectedPlayer.StopMovement();

		bool _canGoNextPlayer = selectedPlayerCount < Players.Length - 1;
		
		selectedPlayerCount = _canGoNextPlayer ? selectedPlayerCount + 1 : 0;
		selectedPlayer = Players[selectedPlayerCount];

		SwitchPlayer?.Invoke();
	}

	// Inputs
	public void OnMove(InputValue value) => movementDirection = value.Get<Vector2>();

	public void OnFire(InputValue value) => TriggerSwitchPlayer();

}
