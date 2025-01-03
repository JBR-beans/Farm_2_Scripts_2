
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.SDKBase.Midi;
using VRC.Udon;

public class SceneReferences : UdonSharpBehaviour
{
	// fields with "total" count lifetime, for example as achievement or to unlock things
	// duplicate crop to make more different plants
	public UdonBehaviour _SelfReference;
	public UdonBehaviour _QuestsHandler;
	/*[Header("Quests")]
	public bool _isQuesting;
	public GameObject _questSupplyDrop;
	public string _cropTag;
	private string _cropData;
	public string[] _cropNames;
	public int _cropIDs;
	private int _ranID;
	public TextMeshProUGUI _hudCurrentQuestProgress;
	public int _questMinAmount;
	public int _questMaxAmount;
	public int _questCurrentAmount;
	public int _questCurrentNeededAmount;
	public int _moneyBonus;
	public int _questsCompleted;*/

	public GameObject _collider1;
	public GameObject _collider2;

	public UdonBehaviour[] _crops;
	public string[] _nameCrops;
	public int[] _currentCrops;
	public int[] _totalCrops;
	public int[] _valueCrops;
	public int[] _upgradelevelYield;
	public int[] _upgradelevelResetTime;
	public Mesh[] _cropMeshes;
	public MeshFilter[] _cropMeshFilters;
	public int[] _costCrops;
	public bool[] _boughtCrops;
	public GameObject[] _cropRoots;
	public int _currentCandy;
	public AudioClip[] _bgmClipLibrary;


	//hud
	public TextMeshProUGUI[] _HUDcurrent;
	public TextMeshProUGUI[] _displayCropID;
	public TextMeshProUGUI[] _displayCropNames;
	public TextMeshProUGUI[] _displayCropAmounts;
	public TextMeshProUGUI[] _displayCropValues;
	public TextMeshProUGUI[] _displayCropLevelYields;
	public TextMeshProUGUI[] _displayCropLevelResetTimes;
	public TextMeshProUGUI _displayCurrentCandy;

	//public Button _buttonToggleEditMode;
	public Image _imageToggleEditMode;
	public TextMeshProUGUI _displayAutoBots;




	/*	public string GenerateCropData(string data, int cropID)
		{

			string _cropData = data + cropID.ToString();

			return _cropData;

		}
		public void RerollQuest()
		{
			// possibly cost money, otherwise just a reroll button
			QuestStarted();
		}
		public void QuestStarted()
		{

			_questCurrentNeededAmount = Random.Range(_questMinAmount, _questMaxAmount);

			int ran = Random.Range(1, _cropNames.Length);
			for(int i = 1; i < _cropNames.Length; i++)
			{
				if (i == ran)
				{
					_ranID = i;
					_cropTag = _cropNames[i];
				}
			}

			_cropData = GenerateCropData("_currentCrop", _ranID);
			_isQuesting = true;
		}
		public void UpdateQuestProgress()
		{
			if (_isQuesting == true)
			{
				_questCurrentAmount = (int)_SelfReference.GetProgramVariable(_cropData);

				_hudCurrentQuestProgress.text = "Collect " + _questCurrentNeededAmount.ToString() + " " + _cropTag;
				_hudCurrentQuestProgress.text += "\n";
				_hudCurrentQuestProgress.text += "Collected: " + _questCurrentAmount.ToString();

				if (_questCurrentAmount >= _questCurrentNeededAmount)
				{
					QuestCompleted();
				}
			}

			//_hudCurrentQuestProgress.text = ;
		}
		public void QuestCompleted()
		{
			// do logic
			_questSupplyDrop.SetActive(true);
			_questsCompleted++;
			_currentMoney += _moneyBonus;
			_isQuesting = false;
			// start new random ambient quest

			// NEEDS USER INPUT
			// if user has lots of crops, quest randomizer will constantly award money 
			// need user input to start new quest, cannot be automatic without some kind of cooldown or check
			// QuestStarted();
		}*/

	/*public void HUDSetCurrentCropTotals()
	{


		_hudCurrentCrop1.text = _currentCrop1.ToString();
		_hudCurrentCrop2.text = _currentCrop2.ToString();
		_hudCurrentCrop3.text = _currentCrop3.ToString();
		_hudCurrentCrop4.text = _currentCrop4.ToString();
		_hudCurrentCrop5.text = _currentCrop5.ToString();
		_hudCurrentCrop6.text = _currentCrop6.ToString();
		_hudCurrentCrop7.text = _currentCrop7.ToString();
		_hudCurrentCrop8.text = _currentCrop8.ToString();
		_hudCurrentCrop9.text = _currentCrop9.ToString();
		_hudCurrentCrop10.text = _currentCrop10.ToString();
		
		_debug.text = _debugText;
	}

	public void HUDSetTotalCropTotals()
	{
		_hudTotalCrop1.text = _totalCrop1.ToString();
		_hudTotalCrop2.text = _totalCrop2.ToString();
		_hudTotalCrop3.text = _totalCrop3.ToString();
		_hudTotalCrop4.text = _totalCrop4.ToString();
		_hudTotalCrop5.text = _totalCrop5.ToString();
		_hudTotalCrop6.text = _totalCrop6.ToString();
		_hudTotalCrop7.text = _totalCrop7.ToString();
		_hudTotalCrop8.text = _totalCrop8.ToString();
		_hudTotalCrop9.text = _totalCrop9.ToString();
		_hudTotalCrop10.text = _totalCrop10.ToString();
	}*/


	[Header("Persistence Configuration")]
	public UdonBehaviour _persistence;
	[Header("Game World")]
	public AudioSource _sfxSharedUIAudioSource;
	public AudioClip _sfxBuy1;
	public AudioClip _sfxBoughtItem;
	public TextMeshProUGUI _hudCheaperCropsUpgradeBought;

	public int _totalAutoBot = 0;
	public int _costUpgradeMaxAutoBot;
	public int _maxAutoBot;

	public TextMeshProUGUI _debug;
	public string _debugText;

/*
	[Header("Display current crop amounts")]
	public TextMeshProUGUI _hudCurrentCrop1;
	public TextMeshProUGUI _hudCurrentCrop2;
	public TextMeshProUGUI _hudCurrentCrop3;
	public TextMeshProUGUI _hudCurrentCrop4;
	public TextMeshProUGUI _hudCurrentCrop5;
	public TextMeshProUGUI _hudCurrentCrop6;
	public TextMeshProUGUI _hudCurrentCrop7;
	public TextMeshProUGUI _hudCurrentCrop8;
	public TextMeshProUGUI _hudCurrentCrop9;
	public TextMeshProUGUI _hudCurrentCrop10;

	[Header("Display total crop amounts")]
	public TextMeshProUGUI _hudTotalCrop1;
	public TextMeshProUGUI _hudTotalCrop2;
	public TextMeshProUGUI _hudTotalCrop3;
	public TextMeshProUGUI _hudTotalCrop4;
	public TextMeshProUGUI _hudTotalCrop5;
	public TextMeshProUGUI _hudTotalCrop6;
	public TextMeshProUGUI _hudTotalCrop7;
	public TextMeshProUGUI _hudTotalCrop8;
	public TextMeshProUGUI _hudTotalCrop9;
	public TextMeshProUGUI _hudTotalCrop10;
*/
	[Header("Current money")]
	public TextMeshProUGUI _hudCurrentMoney;

	public int _currentCrop = 0;
	public int _totalCrop = 0;

	public int _currentMoney = 0;

	public int _totalMoney = 0;
	public int _CropsPlanted = 0;

	public bool _unlockedAutoBot = false;
/*
	[Header("Crop references and data")]
	[Header("Leave index 0 blank, 1 based array")]*/
	public void Upgrade_MaxAutoBot()
	{
		if (_currentMoney >= _costUpgradeMaxAutoBot)
		{
			_currentMoney -= _costUpgradeMaxAutoBot;
			_maxAutoBot++;
		}
	}

	public void Upgrade_FX()
	{
		_sfxSharedUIAudioSource.PlayOneShot(_sfxBuy1);
	}
	public void Start()
	{
		Initialize_Data();
		Assign_Unsaved_Data();
	}

	public void Update()
	{
		Update_Displays();
		_hudCurrentMoney.text = _currentMoney.ToString();
	}

	public void Initialize_Data()
	{
		Assign_ID();
		Initialize_CropRoots();
		Initialize_Names();
		Initialize_CurrentAmount();
		Initialize_TotalAmount();
		Initialize_Value();
		Initialize_Bought();
		Initialize_UpgradeLevel_Yield();
		Initialize_UpgradeLevel_ResetTime();
		Initialize_Displays();
	}
	public void Assign_Unsaved_Data()
	{
		Assign_CropMeshes();
		Assign_CropRoots();
		Assign_Name();
		Assign_Value();
	}
	public void Assign_Saved_Data()
	{

		Assign_Saved_Value();
		Assign_Saved_CurrentAmount();
		Assign_Saved_TotalAmount();
		Assign_Saved_Bought();
		Assign_Saved_UpgradeLevel_Yield();
		Assign_Saved_UpgradeLevel_ResetTime();
	}
	public void Assign_ID()
	{
		for (int i = 0; i < _crops.Length; i++)
		{
			_crops[i].SetProgramVariable("_cropID", i);
		}
	}
	// unsaved initialize
	public void Initialize_CropCost()
	{
		_costCrops = new int[_crops.Length];
	}
	public void Initialize_CropRoots()
	{
		_cropRoots = new GameObject[_crops.Length];
	}
	public void Initialize_Bought()
	{
		_boughtCrops = new bool[_crops.Length];
	}
	public void Initialize_Names()
	{
		_nameCrops = new string[_cropMeshes.Length];

		for (int i = 0; i < _cropMeshes.Length; i++)
		{
			_nameCrops[i] = _cropMeshes[i].name;

		}
	}
	public void Initialize_CurrentAmount()
	{
		_currentCrops = new int[_crops.Length];
	}
	public void Initialize_TotalAmount()
	{
		_totalCrops = new int[_crops.Length];
	}
	public void Initialize_Value()
	{

		_valueCrops = new int[_crops.Length];

		for (int i = 0; i < _crops.Length; i++)
		{
			_valueCrops[i] = (i * 10) + 10;
		}
	}
	public void Initialize_UpgradeLevel_Yield()
	{
		_upgradelevelYield = new int[_crops.Length];

	}
	public void Initialize_UpgradeLevel_ResetTime()
	{
		_upgradelevelResetTime = new int[_crops.Length];
	}
	public void Initialize_Displays()
	{

		_displayCropNames = new TextMeshProUGUI[_crops.Length];

		_displayCropAmounts = new TextMeshProUGUI[_crops.Length];

		_displayCropValues = new TextMeshProUGUI[_crops.Length];

		_displayCropLevelYields = new TextMeshProUGUI[_crops.Length];

		_displayCropLevelResetTimes = new TextMeshProUGUI[_crops.Length];

		_displayCropID = new TextMeshProUGUI[_crops.Length];

		for (int i = 0; i < _crops.Length; i++)
		{
			_displayCropNames[i] = (TextMeshProUGUI)_crops[i].GetProgramVariable("_displayCropName");

			_displayCropAmounts[i] = (TextMeshProUGUI)_crops[i].GetProgramVariable("_displayCropAmount");

			_displayCropValues[i] = (TextMeshProUGUI)_crops[i].GetProgramVariable("_displayCropValue");

			_displayCropLevelYields[i] = (TextMeshProUGUI)_crops[i].GetProgramVariable("_displayCropLevelYield");

			_displayCropLevelResetTimes[i] = (TextMeshProUGUI)_crops[i].GetProgramVariable("_displayCropLevelResetTime");

			_displayCropID[i] = (TextMeshProUGUI)_crops[i].GetProgramVariable("_displayCropID");
		}
	}

	// unsaved assign
	/*public void Assign_CropCost()
	{

	}*/
	public void Assign_CropRoots()
	{
		for (int i = 0; i < _crops.Length;i++)
		{
			_cropRoots[i] = (GameObject)_crops[i].GetProgramVariable("_CropRoot");
		}
	}
	public void Assign_Name()
	{
		for (int i = 0; i < _crops.Length; i++)
		{
			_crops[i].SetProgramVariable("_cropName", _nameCrops[i]);
		}
	}
	public void Assign_Value()
	{
		for (int i = 0; i < _crops.Length; i++)
		{
			_crops[i].SetProgramVariable("_valueCrop", _valueCrops[i]);
		}
	}
	public void Assign_CropMeshes()
	{
		for (int i = 0; i < _crops.Length; i++)
		{
			MeshFilter mf = (MeshFilter)_crops[i].GetProgramVariable("_cropMeshFilter");
			ParticleSystem ps1 = (ParticleSystem)_crops[i].GetProgramVariable("_particlesMeshFX");
			ParticleSystem ps2 = (ParticleSystem)_crops[i].GetProgramVariable("_particlesCollectable");

			ps1.GetComponent<ParticleSystemRenderer>().mesh = _cropMeshes[i];
			ps2.GetComponent<ParticleSystemRenderer>().mesh = _cropMeshes[i];
			mf.mesh = _cropMeshes[i];

		}
	}

	// saved

	public void Assign_Saved_CurrentAmount()
	{

		for (int i = 0; i < _crops.Length; i++)
		{
			_crops[i].SetProgramVariable("_currentCrop", _currentCrops[i]);
		}
	}
	public void Assign_Saved_TotalAmount()
	{
		for (int i = 0; i < _crops.Length; i++)
		{
			_crops[i].SetProgramVariable("_totalCrop", _totalCrops[i]);
		}
	}
	public void Assign_Saved_Value()
	{
		for (int i = 0; i < _crops.Length; i++)
		{
			_crops[i].SetProgramVariable("_valueCrop", _valueCrops[i]);
		}
	}
	public void Assign_Saved_Bought()
	{
		for (int i = 0; i < _crops.Length; i++)
		{
			_crops[i].SetProgramVariable("_bought", _boughtCrops[i]);
		}
	}
	public void Assign_Saved_UpgradeLevel_Yield()
	{
		for (int i = 0; i < _crops.Length; i++)
		{
			if (_upgradelevelYield[i] < 1)
			{
				_upgradelevelYield[i] = 1;
			}
			_crops[i].SetProgramVariable("_upgradelevelYield", _upgradelevelYield[i]);
			
		}
	}
	public void Assign_Saved_UpgradeLevel_ResetTime()
	{
		for (int i = 0; i < _crops.Length; i++)
		{
			if (_upgradelevelResetTime[i] < 1)
			{
				_upgradelevelResetTime[i] = 1;
			}
			_crops[i].SetProgramVariable("_upgradelevelResetTime", _upgradelevelResetTime[i]);
		}
	}
	public void Update_Displays()
	{
		_displayCurrentCandy.text = _currentCandy.ToString();

		_displayAutoBots.text = "AutoBots in use: " + _totalAutoBot.ToString() + "/" + _maxAutoBot.ToString();
		for (int i = 0; i < _crops.Length; i++)
		{
			_HUDcurrent[i].text = _currentCrops[i].ToString();

			_displayCropID[i].text = _crops[i].GetProgramVariable("_cropID").ToString();

			_displayCropNames[i].text = _nameCrops[i].ToString();

			_displayCropAmounts[i].text = _currentCrops[i].ToString();

			_displayCropValues[i].text = _valueCrops[i].ToString();

			_displayCropLevelYields[i].text = _crops[i].GetProgramVariable("_upgradeLevelYield").ToString();

			_displayCropLevelResetTimes[i].text = _crops[i].GetProgramVariable("_upgradeLevelResetTime").ToString();

		}
	}


	/*//public Mesh[] _cropMeshes;
	[Header("crop1")]
	public string _tagCrop1 = "crop1";
	public int _currentCrop1 = 0;
	public int _totalCrop1 = 0;
	public int _valueCrop1 = 0;

	[Header("crop2")]
	public string _tagCrop2 = "crop2";
	public int _currentCrop2 = 0;
	public int _totalCrop2 = 0;
	public int _valueCrop2 = 0;

	[Header("crop3")]
	public string _tagCrop3 = "crop3";
	public int _currentCrop3 = 0;
	public int _totalCrop3 = 0;
	public int _valueCrop3 = 0;

	[Header("crop4")]
	public string _tagCrop4 = "crop4";
	public int _currentCrop4 = 0;
	public int _totalCrop4 = 0;
	public int _valueCrop4 = 0;

	[Header("crop5")]
	public string _tagCrop5 = "crop5";
	public int _currentCrop5 = 0;
	public int _totalCrop5 = 0;
	public int _valueCrop5 = 0;

	[Header("crop6")]
	public string _tagCrop6 = "crop6";
	public int _currentCrop6 = 0;
	public int _totalCrop6 = 0;
	public int _valueCrop6 = 0;

	[Header("crop7")]
	public string _tagCrop7 = "crop7";
	public int _currentCrop7 = 0;
	public int _totalCrop7 = 0;
	public int _valueCrop7 = 0;

	[Header("crop8")]
	public string _tagCrop8 = "crop8";
	public int _currentCrop8 = 0;
	public int _totalCrop8 = 0;
	public int _valueCrop8 = 0;

	[Header("crop9")]
	public string _tagCrop9 = "crop9";
	public int _currentCrop9 = 0;
	public int _totalCrop9 = 0;
	public int _valueCrop9 = 0;

	[Header("crop10")]
	public string _tagCrop10 = "crop10";
	public int _currentCrop10 = 0;
	public int _totalCrop10 = 0;
	public int _valueCrop10 = 0;*/

}

