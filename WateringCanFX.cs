
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class WateringCanFX : UdonSharpBehaviour
{
	public int _layerWateringCan;
	public ParticleSystem _particlesWateringCan;


	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == _layerWateringCan)
		{
			_particlesWateringCan.Play();
		}
	}

}
