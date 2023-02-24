using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
	// Switch Particles
	[SerializeField] private ParticleSystem playerSwitchParticles;
	public ParticleSystem PlayerSwitchParticles { get => playerSwitchParticles; }

	// Movement
	[SerializeField] private ParticleSystem playerMovementParticles;
	public ParticleSystem PlayerMovementParticles { get => playerSwitchParticles; }


}
