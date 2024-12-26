
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

	private const string _key_currentMoney = "_currentMoney";
    private const string _keyQuestCompleted = "_questsCompleted";
	public int _currentMoney;
	public VRCPlayerApi _playerAPI;
	public bool _isFirstSessionLoad;


	 public void Start()
    {
        
    }


    public void Save_Data()
    {
		if (Networking.LocalPlayer.isLocal)
        {
			int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");
			PlayerData.SetInt(_key_currentMoney, _currentMoney);

		}

	}

    public void Load_Data()
    {
		var currentMoney = PlayerData.GetInt(Networking.LocalPlayer, _key_currentMoney);
        _SceneReferences.SetProgramVariable("_currentMoney", currentMoney);
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
