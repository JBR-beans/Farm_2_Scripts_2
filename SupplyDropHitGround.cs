
using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.Android;
using VRC.SDKBase;
using VRC.Udon;

public class SupplyDropHitGround : UdonSharpBehaviour
{
	public bool _useParticleBurst;
	public ParticleSystem _burstParticles;
	public Single _burstMax;
	public bool _useItem;
	public GameObject _Item;

	public bool _additionalItemEnabled;
	public GameObject _additionalItem;

	public bool _additionalItemArrayEnabled;
	public GameObject[] _additionalItemArray;

	public GameObject _PS1;
	public GameObject _PS2;
	public AudioSource _sfxCrate;
	public AudioClip _sfxClip;
	public void OnCollisionEnter(Collision collision)
	{
		_PS1.SetActive(false);
		_PS2.SetActive(false);

		_PS1.transform.transform.position = transform.position;
		_PS2.transform.transform.position = transform.position;

		if (_useItem == true)
		{
			_Item.SetActive(true);
		}

		_PS1.SetActive(true);
		_PS2.SetActive(true);

		if (_additionalItemEnabled == true)
		{
			_additionalItem.SetActive(true);
		}

		if (_additionalItemArrayEnabled == true)
		{
			foreach (GameObject go in _additionalItemArray)
			{
				go.SetActive(true);
			}
		}

		_sfxCrate.PlayOneShot(_sfxClip);

		if (_useParticleBurst == true)
		{
			var emission = _burstParticles.emission;

			emission.SetBursts(
			new ParticleSystem.Burst[]
				{
					new ParticleSystem.Burst(0.0f, _burstMax)
				}
			);

			_burstParticles.Play();
		}
		this.gameObject.SetActive(false);
	}
}
