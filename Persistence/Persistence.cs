
using System;
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
	//public TextMeshProUGUI _dbgtxt_P_CurrentMoney;

	private const string _key_currentMoney = "_currentMoney";
    private const string _keyQuestCompleted = "_questsCompleted";
	public int _currentMoney;
	public VRCPlayerApi _playerAPI;
	public bool _isFirstSessionLoad;

	public DateTime _lastSaveDateTime;
	private const string _key_lastSaveDateTime = "_key_lastSaveDateTime";
	public TextMeshProUGUI _displayLastSaveDateTime;

	public void Save_Data()
    {
		if (Networking.LocalPlayer.isLocal)
        {
			UdonBehaviour[] _crops = (UdonBehaviour[])_SceneReferences.GetProgramVariable("_crops");

			int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");
            int[] _currentCrops = (int[])_SceneReferences.GetProgramVariable("_currentCrops");
			bool[] _boughtCrops = new bool[_crops.Length];

			string[] _keys_currentCrops = new string[_currentCrops.Length];
			string[] _keys_bought = new string[_currentCrops.Length];

			// generate keys
			for (int i = 0; i < _crops.Length; i++)
			{
				_keys_currentCrops[i] = "_currentCrop" + i.ToString();
				_keys_bought[i] = "_boughtCrops" + i.ToString();

			}

			// pull data from local storage and push to server
			for (int i = 0; i < _crops.Length; i++)
            {
                PlayerData.SetInt(_keys_currentCrops[i], _currentCrops[i]);

				_boughtCrops[i] = (bool)_crops[i].GetProgramVariable("_boughtCrop");

				PlayerData.SetBool(_keys_bought[i], _boughtCrops[i]);

			}

			PlayerData.SetInt(_key_currentMoney, _currentMoney);
			LastSaveDateTime();
		}

	}

	public void LastSaveDateTime()
	{
		_lastSaveDateTime = DateTime.Now;
		PlayerData.SetString(_key_lastSaveDateTime, _lastSaveDateTime.ToString());
		_displayLastSaveDateTime.text = _lastSaveDateTime.ToString();
	}

	public void Enable_BoughtCropPlots(UdonBehaviour[] _crops, bool[] _boughtCrops)
	{
		GameObject[] _cropRoots = (GameObject[])_SceneReferences.GetProgramVariable("_cropRoots");

		for (int i = 0; i < _crops.Length; i++)
		{
			if (_boughtCrops[i] == true)
			{
				_cropRoots[i].gameObject.SetActive(true);
			}
		}
	}

    public void Load_Data()
    {
		if (Networking.LocalPlayer.isLocal)
		{
			var currentMoney = PlayerData.GetInt(Networking.LocalPlayer, _key_currentMoney);

			// scripts that house all variables
			UdonBehaviour[] _crops = (UdonBehaviour[])_SceneReferences.GetProgramVariable("_crops");

			// data arrays
			int[] _currentCrops = (int[])_SceneReferences.GetProgramVariable("_currentCrops");
			bool[] _boughtCrops = (bool[])_SceneReferences.GetProgramVariable("_boughtCrops");
			// initialize key arrays
			string[] _keys_currentCrops = new string[_crops.Length];
			string[] _keys_bought = new string[_crops.Length];

			// generate keys
			for (int i = 0; i < _crops.Length; i++)
			{
				_keys_currentCrops[i] = "_currentCrop" + i.ToString();
				_keys_bought[i] = "_boughtCrops" + i.ToString();


			}

			// pull data from server and populate data arrays
			for (int i = 0; i < _crops.Length; i++)
			{
				_currentCrops[i] = PlayerData.GetInt(Networking.LocalPlayer, _keys_currentCrops[i]);
				_boughtCrops[i] = PlayerData.GetBool(Networking.LocalPlayer, _keys_bought[i]);

				if (_boughtCrops[i] == true)
				{
					_crops[i].gameObject.SetActive(true);
					GameObject g = (GameObject)_crops[i].GetProgramVariable("_buyButton");
					g.SetActive(false);
				}

			}

			// get last save timestamp
			string lastsavedatetime = PlayerData.GetString(Networking.LocalPlayer, _key_lastSaveDateTime);
			_displayLastSaveDateTime.text = lastsavedatetime;

			// assign pulled data to be stored in SceneReferences 
			_SceneReferences.SetProgramVariable("_currentMoney", currentMoney);
			_SceneReferences.SetProgramVariable("_currentCrops", _currentCrops);
			_SceneReferences.SetProgramVariable("_boughtCrops", _boughtCrops);

			// run local logic as needed
			Enable_BoughtCropPlots(_crops, _boughtCrops);

		}
			
	}

  /*  public void PersistData_Save()
    {
        if (_playerAPI.isLocal)
        {
            int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");
            int _questsCompleted = (int)_SceneReferences.GetProgramVariable("_questsCompleted"); 

			var currentMoney = PlayerData.GetInt(Networking.LocalPlayer, _TESTkeyCurrentMoney);
			var questsCompleted = PlayerData.GetInt(Networking.LocalPlayer, _keyQuestCompleted);

			//PlayerData.SetInt(_TESTkeyCurrentMoney, _currentMoney);
			PlayerData.SetInt(_keyQuestCompleted, _questsCompleted);

		}
    }*/

/*    public void PersistData_Load()
    {
        if (_playerAPI.isLocal)
        {
            var currentMoney = PlayerData.GetInt(Networking.LocalPlayer, _TESTkeyCurrentMoney);
			var questsCompleted = PlayerData.GetInt(Networking.LocalPlayer, _keyQuestCompleted);

			int _currentMoney = currentMoney;
            int _questsCompleted = questsCompleted;

			_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney);
			_SceneReferences.SetProgramVariable("_questsCompleted", _questsCompleted);
		}
    }*/

    public override void OnPlayerDataUpdated(VRCPlayerApi player, PlayerData.Info[] infos)
    {
        if (player.isLocal)
        {
            Load_Data();

		}
    }
}
