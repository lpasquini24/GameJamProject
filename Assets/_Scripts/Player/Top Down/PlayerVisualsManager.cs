using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualsManager : MonoBehaviour
{
	// Objects
	private PlayerManager playerManager;
	private void Start() => playerManager = GetComponent<PlayerManager>();

	public void SwitchPlayerParticles() => playerManager.SelectedPlayer.GetComponentInChildren<PlayerVisuals>().PlayerSwitchParticles?.Play();

	public void MovementParticles() => playerManager.SelectedPlayer.GetComponentInChildren<PlayerVisuals>().PlayerMovementParticles?.Play();

}
