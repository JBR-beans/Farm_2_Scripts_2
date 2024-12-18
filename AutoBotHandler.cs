﻿
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class AutoBotHandler : UdonSharpBehaviour
{
    public UdonBehaviour _SceneReferences;
    public UdonBehaviour _CropPlotReferences;
	public UdonBehaviour _CropPlotBuyButtonReference;
	public UdonBehaviour _EventCaller;
    public GameObject _AutoBot;
    private int _autobotcount;
	private int _maxautobot;

    public bool _isAutoBotAssigned;

    public GameObject _btnAssign;
	public GameObject _btnDismiss;

    public GameObject _supplyDropCrate;

    public void CheckAutobotCount()
    {
        int count = (int)_SceneReferences.GetProgramVariable("_totalAutoBot");
		int max = (int)_SceneReferences.GetProgramVariable("_maxAutoBot");
		_maxautobot = max;
		_autobotcount = count;
	}

    public void ToggleEditMode()
    {

		CheckAutobotCount();
		// dont display edit mode buttons for crop plots not yet bought, referenced from the buy button script
		bool isbought = (bool)_CropPlotBuyButtonReference.GetProgramVariable("_isBought");
		if (_isAutoBotAssigned == true && isbought == true)
		{
			_btnDismiss.SetActive(!_btnDismiss.activeSelf);
			_btnAssign.SetActive(false);
		}
		else if (_isAutoBotAssigned == false && isbought == true) 
		{
			_btnAssign.SetActive(!_btnAssign.activeSelf);
			_btnDismiss.SetActive(false);
		}
	}

	public void DisableButtons()
	{
		_btnAssign.SetActive(false);
		_btnDismiss.SetActive(false);
	}

    public void AssignAutoBot()
    {
		CheckAutobotCount();
		if (_autobotcount < _maxautobot)
		{
			_btnAssign.SetActive(false);
			_supplyDropCrate.SetActive(true);
			_SceneReferences.SetProgramVariable("_totalAutoBot", _autobotcount + 1);
			_btnAssign.SetActive(false);
			_btnDismiss.SetActive(false);
			_isAutoBotAssigned = true;
			UpgradesOn();
			_EventCaller.SendCustomEvent("SendEventsToBehaviors2");
		}
	}

    public void DismissAutoBot()
    {
		CheckAutobotCount();
		_btnDismiss.SetActive(false);
		// autobot runs dismissal animation
		// dismissal logic is triggered
		_AutoBot.SetActive(false);
		_btnAssign.SetActive(false);
		_btnDismiss.SetActive(false);
		_SceneReferences.SetProgramVariable("_totalAutoBot", _autobotcount - 1);
		_isAutoBotAssigned = false;
		UpgradesOff();
		_EventCaller.SendCustomEvent("SendEventsToBehaviors2");
	}

    public void UpgradesOn()
    {
        string croptag = (string)_CropPlotReferences.GetProgramVariable("_cropTag");
        int cropvalue = (int)_SceneReferences.GetProgramVariable("_value" + croptag);
        cropvalue = cropvalue / 2;
		_SceneReferences.SetProgramVariable("_value" + croptag, cropvalue);

		_CropPlotReferences.SetProgramVariable("_isAutoBotActive", true);
	}
    public void UpgradesOff()
    {
		string croptag = (string)_CropPlotReferences.GetProgramVariable("_cropTag");
		int cropvalue = (int)_SceneReferences.GetProgramVariable("_value" + croptag);
		cropvalue = cropvalue * 2;
		_SceneReferences.SetProgramVariable("_value" + croptag, cropvalue);
		_CropPlotReferences.SetProgramVariable("_isAutoBotActive", false);
	}
}
