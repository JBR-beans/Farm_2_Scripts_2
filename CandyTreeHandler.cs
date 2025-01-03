
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class CandyTreeHandler : UdonSharpBehaviour
{
	public int _layer;

	public ParticleSystem[] _particles;
	public Animator _animator;
	public string _stateName;
	public AudioSource _audioSource;
	public AudioClip[] _audioClips;
	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == _layer)
		{
			_particles[Random.Range(0, _particles.Length)].Play();
			CandyTreeHitFX();
		}
	}

	public void CandyTreeHitFX()
	{
		if (_animator != null)
		{
			_animator.SetTrigger("shaketree");
		}
		_audioSource.PlayOneShot(_audioClips[Random.Range(0, _audioClips.Length)], 0.1f);
	}
}
