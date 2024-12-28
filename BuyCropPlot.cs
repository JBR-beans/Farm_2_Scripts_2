
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Persistence;
using VRC.SDKBase;
using VRC.Udon;

public class BuyCropPlot : UdonSharpBehaviour
{
	public UdonBehaviour _SceneReferences;
	public UdonBehaviour _LocalReferences;
	public GameObject _Item;
	public int _Cost;
	public AudioSource _sfxSharedUIAudioSource;
	public AudioClip _sfxBuy1;

	public void Start()
	{
		_sfxSharedUIAudioSource = (AudioSource)_SceneReferences.GetProgramVariable("_sfxSharedUIAudioSource");
		_sfxBuy1 = (AudioClip)_SceneReferences.GetProgramVariable("_sfxBuy1");

	}
	public override void Interact()
	{
		Buy();
	}
	public void Buy()
	{
		int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");

		if (_currentMoney >= _Cost)
		{

			_Item.SetActive(true);
			_LocalReferences.SetProgramVariable("_boughtCrop", true);
			_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _Cost);
			BuyFX();
			this.gameObject.SetActive(false);
		}
	}
	public void BuyFX()
	{
		
		_sfxSharedUIAudioSource.PlayOneShot(_sfxBuy1);
	}
}
