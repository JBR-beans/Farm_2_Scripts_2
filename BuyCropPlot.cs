
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Persistence;
using VRC.SDKBase;
using VRC.Udon;

public class BuyCropPlot : UdonSharpBehaviour
{
	public UdonBehaviour _SceneReferences;
	public int _cropID;
	
	public bool _setCostManually;
	public int _Cost;
	public bool _setCostText;
	public TextMeshProUGUI _textCost;
	public GameObject _Item;
	public bool _useArray;
	public GameObject[] _Items;

	[Header("INTERNAL")]
	public AudioSource _sfxSharedUIAudioSource;
	public AudioClip _sfxBuy1;

	[Header("INTERNAL | Persistence")]
	public bool _bufferIsBought;
	public string _keyIsBought;
	public bool _isBought = false;
	public VRCPlayerApi _playerAPI;
	public void Start()
	{
		_keyIsBought = "_isBought" + _cropID.ToString();


		if (_setCostManually == false)
		{
			_Cost = _cropID * 100;
		}
		
		if (_setCostText == true)
		{
			_textCost.text = _Cost.ToString();
		}
		PersistData_Load_Local();

	}
	public override void OnPlayerDataUpdated(VRCPlayerApi player, PlayerData.Info[] infos)
	{
		if (player.isLocal)
		{
			PersistData_Load_Local();

			if (_isBought == true)
			{
				if (_useArray == true)
				{
					foreach (GameObject go in _Items)
					{
						go.SetActive(true);
						this.gameObject.SetActive(false);
					}
				}
				else
				{
					_Item.SetActive(true);
					this.gameObject.SetActive(false);
				}
			}
		}
	}
	public void PersistData_Load_Local()
	{
		PersistData_Get_Local();
		PersistData_Assign_Local();
	}
	public void PersistData_Get_Local()
	{
		_bufferIsBought = PlayerData.GetBool(Networking.LocalPlayer, _keyIsBought);
	}
	public void PersistData_Assign_Local()
	{
		_isBought = _bufferIsBought;
	}

	public void PersistData_Save_Local()
	{
		PlayerData.SetBool(_keyIsBought, true);
	}
	public override void Interact() 
	{
		Buy();
	}
	
	 public void Buy()
	{
		_sfxSharedUIAudioSource = (AudioSource)_SceneReferences.GetProgramVariable("_sfxSharedUIAudioSource");
		_sfxBuy1 = (AudioClip)_SceneReferences.GetProgramVariable("_sfxBuy1");
		if (_useArray == true)
		{
			int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");

			if (_currentMoney >= _Cost)
			{
				foreach (GameObject go in _Items)
				{
					go.SetActive(true);
				}
				_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _Cost);
				UdonBehaviour _persistence = (UdonBehaviour)_SceneReferences.GetProgramVariable("_persistence");
				_persistence.SendCustomEvent("PersistData_Save");
				_sfxSharedUIAudioSource.PlayOneShot(_sfxBuy1);
				_isBought = true;
				PersistData_Save_Local();
				this.gameObject.SetActive(false);
			}
		}
		else
		{
			int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");

			if (_currentMoney >= _Cost)
			{
				_Item.SetActive(true);
				_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _Cost);
				UdonBehaviour _persistence = (UdonBehaviour)_SceneReferences.GetProgramVariable("_persistence");
				_persistence.SendCustomEvent("PersistData_Save");
				_sfxSharedUIAudioSource.PlayOneShot(_sfxBuy1);
				_isBought = true;
				PersistData_Save_Local();
				this.gameObject.SetActive(false);
			}
		}
	}
}
