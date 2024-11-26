
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
	private const string _TESTkeyCurrentMoney = "_currentMoney";
	public int _currentMoney;
	public VRCPlayerApi _playerAPI;
	public bool _isFirstSessionLoad;


	 public void Start()
    {
        _playerAPI = Networking.LocalPlayer;
        PersistData_Load();
    }

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
}
