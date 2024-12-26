
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
			int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");

            int[] _currentCrops = (int[])_SceneReferences.GetProgramVariable("_currentCrops");
            string[] _keys_currentCrops = new string[_currentCrops.Length];
            for(int i = 0; i < _keys_currentCrops.Length; i++)
            {
                _keys_currentCrops[i] = "_currentCrop" + i.ToString();
                PlayerData.SetInt(_keys_currentCrops[i], _currentCrops[i]);
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
    public void Load_Data()
    {
		if (Networking.LocalPlayer.isLocal)
		{
			var currentMoney = PlayerData.GetInt(Networking.LocalPlayer, _key_currentMoney);

			int[] _currentCrops = (int[])_SceneReferences.GetProgramVariable("_currentCrops");
			string[] _keys_currentCrops = new string[_currentCrops.Length];

			for (int i = 0; i < _currentCrops.Length; i++)
			{
				_keys_currentCrops[i] = "_currentCrop" + i.ToString();
				_currentCrops[i] = PlayerData.GetInt(Networking.LocalPlayer, _keys_currentCrops[i]);
			}

			string lastsavedatetime = PlayerData.GetString(Networking.LocalPlayer, _key_lastSaveDateTime);

			_displayLastSaveDateTime.text = lastsavedatetime;
			_SceneReferences.SetProgramVariable("_currentMoney", currentMoney);
			_SceneReferences.SetProgramVariable("_currentCrops", _currentCrops);
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
