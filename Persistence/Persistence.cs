
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Persistence;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common;

public class Persistence : UdonSharpBehaviour
{
	public UdonBehaviour _SceneReferences;
	public TextMeshProUGUI _dbgtxt_P_CurrentMoney;
	private const string _TESTkeyCurrentMoney = "currentMoney";
	public int _currentMoney;
	public VRCPlayerApi _playerAPI;
	
	
	public void PersistData_Save()
	{
		if (_playerAPI.isLocal)
		{
			int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");

			var currentMoney = PlayerData.GetInt(Networking.LocalPlayer, _TESTkeyCurrentMoney);

			PlayerData.SetInt(_TESTkeyCurrentMoney, _currentMoney);
		}
		

		
	}

	public void PersistData_Load()
	{
		if (_playerAPI.isLocal)
		{
			var currentMoney = PlayerData.GetInt(Networking.LocalPlayer, _TESTkeyCurrentMoney);

			int _currentMoney = currentMoney;

			_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney);
		}
	}

	public override void OnPlayerDataUpdated(VRCPlayerApi player, PlayerData.Info[] infos)
	{
		if (player.isLocal)
		{
			PersistData_Load();
			_playerAPI = player;
		}
	}


	/*public void Start()
	{
		PersistData_Load();
	}*/

	/*if (player.isLocal)
	{
		UpdateTextComponent();
	}*/

	/*private void UpdateTextComponent()
	{
		_dbgtxt_P_CurrentMoney = (TextMeshProUGUI)_SceneReferences.GetProgramVariable("_hudCurrentMoney");
		_dbgtxt_P_CurrentMoney.text = $"Current Money (persisted): {PlayerData.GetInt(Networking.LocalPlayer, _TESTkeyCurrentMoney)}";
	}
*/
	// corruption checking
	/*

	public bool _isSaveDataCorrupt;

	if (_isSaveDataCorrupt == false)
	{
		var currentMoney = PlayerData.GetInt(Networking.LocalPlayer, _TESTkeyCurrentMoney);

		int _currentMoney = currentMoney;

		_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney);
	}

	if (_isSaveDataCorrupt == true)
	{
		_dbgtxt_P_CurrentMoney.text = "Please attempt rejoining the instance.";
	}

	// to go in save
	if (PlayerData.GetInt(Networking.LocalPlayer, _TESTkeyCurrentMoney) == _currentMoney)
	{
		_isSaveDataCorrupt = false;
		_dbgtxt_P_CurrentMoney.text += " | false";
	}
	if (PlayerData.GetInt(Networking.LocalPlayer, _TESTkeyCurrentMoney) != _currentMoney)
	{
		_isSaveDataCorrupt = true;
		_dbgtxt_P_CurrentMoney.text += " | true";
	}
	*/
}
