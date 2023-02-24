using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualsManager : MonoBehaviour
{
	// Objects
	private PlayerManager playerManager;
	private void Start() => playerManager = GetComponent<PlayerManager>();

	public void SwitchPlayerParticles() => playerManager.SelectedPlayer.GetComponentInChildren<PlayerVisuals>().PlayerSwitchParticles?.Play();

    public void SetSelectedParticles()
    {
        foreach (var player in playerManager.Players)
        {
            player.GetComponentInChildren<PlayerVisuals>().PlayerSelectedParticles?.Stop();
        }
       playerManager.SelectedPlayer.GetComponentInChildren<PlayerVisuals>()?.PlayerSelectedParticles?.Play();
    }

    public void MovementParticles() => playerManager.SelectedPlayer.GetComponentInChildren<PlayerVisuals>().PlayerMovementParticles?.Play();

}
