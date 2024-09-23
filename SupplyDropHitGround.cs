
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SupplyDropHitGround : UdonSharpBehaviour
{
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

		_Item.SetActive(true);

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

		this.gameObject.SetActive(false);
	}
}
