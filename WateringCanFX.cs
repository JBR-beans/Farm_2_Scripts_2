
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class WateringCanFX : UdonSharpBehaviour
{
	public int _layerWateringCan;
	public ParticleSystem _particlesWateringCan;
	public AudioSource _sfxSource;
	public AudioClip _sfxClip;

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == _layerWateringCan)
		{
			_particlesWateringCan.Play();
			_sfxSource.PlayOneShot(_sfxClip);
		}
	}

}
