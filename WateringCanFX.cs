
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class WateringCanFX : UdonSharpBehaviour
{
	public int _layerWateringCan;
	public ParticleSystem _particlesWateringCan;
	public ParticleSystem _particlesWateringHose;
	public AudioSource _sfxSource;
	public AudioClip _sfxClip;

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == _layerWateringCan)
		{
			if (_particlesWateringCan != null)
			{
				_particlesWateringCan.Play();
				_sfxSource.PlayOneShot(_sfxClip);
			}
		}
	}

	public override void OnPickupUseDown()
	{
		if (_particlesWateringHose !=  null)
		{
			_particlesWateringHose.Play();
			_sfxSource.PlayOneShot(_sfxClip);
		}
	}
}
