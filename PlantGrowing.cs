using System;
using System.Diagnostics;
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK3.Persistence;
using VRC.SDKBase;
using VRC.Udon;

public class PlantGrowing : UdonSharpBehaviour
{

	[Header("Modifiers")]
	public bool _useSeeds;

	[Header("INTERNAL | Persistence")]
	public bool _isFirstSessionLoad = true;
	public string _keyUpgradeLevelResetTime;
	public int _upgradeLevelResetTime = 1;
	public string _keyUpgradeLevelYield;
	public int _upgradeLevelYield = 1;
	public string _keyCurrentCropAmount;
	public int _currentCrop;
	public VRCPlayerApi _playerAPI;

	[Header("configure crop")]
	public Mesh _cropMesh;
	public MeshFilter _cropMeshFilter;
	// _particlesCrop
	
	public int _cropID;
	public string _cropTag;
	public float _maxGrowTime;
	public float _growSpeedMultiplier;
	public int _maxGrowthPhase;
	public GameObject _cropRoot;
	public UdonSharpBehaviour _SceneReferences;

	[Header("Configure Upgrades")]
	public int _Yield = 1;
	public int _modiferYield;
	public int _maxLevelYield;
	public int _upgradeCostYield;

	public float _ResetTime;
	public float _modifierResetTime;
	public float _maxLevelResetTime;
	public int _upgradeCostResetTime;

	public int _upgradeCostAutoPlant;
	public int _upgradeCostAutoWater;
	public int _upgradeCostAutoHarvest;

	[Header("changed in script")]
	public bool _isAutoBotActive;

	[Header("Mesh triggers")]
	public GameObject _meshHarvested;
	public GameObject _meshPlanted;
	public GameObject _meshWaterTrigger;
	public GameObject _meshHarvestTrigger;

	[Header("configure VFX/SFX")]
	public ParticleSystem _particlesDirt;
	public ParticleSystem[] _particlesCrop;
	public ParticleSystem _particleWater;
	public AudioSource _sfxSource;
	public AudioClip _sfxClipHarvest;
	public AudioClip _sfxClipPlanted;
	public AudioClip _sfxClipWatered;

	[Header("Indicators")]
	public Image _imgGrowingVisual;
	public Image _imgResetingVisual;

	public GameObject _popupOutOfSeeds;
	public GameObject _popupNeedsWater;
	public GameObject _popupReadyToHarvest;

	[Header("Upgrade Text View")]
	public TextMeshProUGUI _txtResetTime;

	[Header("debug views")]
	public TextMeshProUGUI _debugView1;
	//public TextMeshProUGUI _debugView2;

	[Header("INTERNAL")]
	public int _growthPhase = 0;
	private float _currentgrowtime = 0;
	private float _ticker = 0;
	private bool _cycleReseting;
	
	
	
	public void FixedUpdate()
	{

		// set _currentgrowtime to 0.0f to temporarily halt growth phases
		if (_currentgrowtime < _maxGrowTime && _currentgrowtime >= 0.1f)
		{
			Growing();
		}

		if (_currentgrowtime > _maxGrowTime)
		{
			// needs to be watered
			if (_growthPhase <= _maxGrowthPhase)
			{
				GrowthPhaseCompleted();
			}

			// ready to be harvested
			if (_growthPhase > _maxGrowthPhase)
			{
				LastGrowthPhase();
			}
		}

		if (_cycleReseting == true)
		{
			_imgResetingVisual.color = Color.red;
			_ticker += Time.deltaTime;

			_imgResetingVisual.fillAmount = (float)ConvertFrom_Range1_Input_To_Range2_Output(0, _ResetTime, 0, 1, _ticker);
			_imgGrowingVisual.fillAmount = 0;

			if (_ticker > _ResetTime)
			{
				_meshPlanted.SetActive(false);
				_meshHarvested.SetActive(true);
				
				_cycleReseting = false;
				_ticker = 0;

				ReadyToPlant();

				if (_isAutoBotActive == true)
				{
					ResetPlant();
				}
			}
		}
	}
	public void ReadyToPlant()
	{
		_imgResetingVisual.color = Color.green;
	}

	private double ConvertFrom_Range1_Input_To_Range2_Output(double _input_range_min, double _input_range_max, double _output_range_min, double _output_range_max, double _input_value_tobe_converted)
	{
		double diffOutputRange = Math.Abs((_output_range_max - _output_range_min));

		double diffInputRange = Math.Abs((_input_range_max - _input_range_min));

		double convFactor = (diffOutputRange / diffInputRange);

		return (_output_range_min + (convFactor * (_input_value_tobe_converted - _input_range_min)));
	}

	private void Growing()
	{
		_currentgrowtime += Time.fixedDeltaTime;
		_cropRoot.transform.localScale += new Vector3(Time.fixedDeltaTime* _growSpeedMultiplier, Time.fixedDeltaTime * _growSpeedMultiplier, Time.fixedDeltaTime * _growSpeedMultiplier);

		_imgGrowingVisual.fillAmount = (float)ConvertFrom_Range1_Input_To_Range2_Output(0, _maxGrowTime, 0, 1, _currentgrowtime);
		_imgResetingVisual.fillAmount = 0;

	}

	
	public void GrowthPhaseCompleted()
	{
		// while 0, growing is paused
		// set to greater than 0.1f to start growing cycle again
		_growthPhase++;
		_currentgrowtime = 0;

		_meshWaterTrigger.SetActive(true);
		_popupNeedsWater.SetActive(true);
		
		_imgGrowingVisual.fillAmount = 0;
		_imgResetingVisual.fillAmount = 0;

		if (_isAutoBotActive == true)
		{
			_meshWaterTrigger.SetActive(false);

			_sfxSource.PlayOneShot(_sfxClipWatered);

			WaterPlant();
		}

	}
	
	public void LastGrowthPhase()
	{
		// just stalling _currentgrowtime without reseting it to 0
		_currentgrowtime = 0;
		
		_meshHarvestTrigger.SetActive(true);
		_popupReadyToHarvest.SetActive(true);

		_popupNeedsWater.SetActive(false);
		_meshWaterTrigger.SetActive(false);

		_imgGrowingVisual.fillAmount = 0;
		_imgResetingVisual.fillAmount = 0;

		if (_isAutoBotActive == true)
		{
			HarvestPlant();
			_meshHarvestTrigger.SetActive(false);
		}
	}

	public void ResetPlant()
	{
		/*if (_useSeeds == true)
		{
			switch (_cropTag)
			{
				case "crop1":

					int _seedCrop1 = (int)_SceneReferences.GetProgramVariable("_seedCrop1");
					if (_seedCrop1 > 0)
					{
						_SceneReferences.SetProgramVariable("_seedCrop1", _seedCrop1 - 1);
						StartGrowing();
					}
					if (_seedCrop1 <= 0)
					{

						_popupOutOfSeeds.SetActive(true);
					}
					_sfxSource.PlayOneShot(_sfxClipPlanted);

					break;

				case "crop2":

					int _seedCrop2 = (int)_SceneReferences.GetProgramVariable("_seedCrop2");
					if (_seedCrop2 > 0)
					{
						_SceneReferences.SetProgramVariable("_seedCrop2", _seedCrop2 - 1);
						StartGrowing();
					}
					if (_seedCrop2 <= 0)
					{
						_popupOutOfSeeds.SetActive(true);
					}
					_sfxSource.PlayOneShot(_sfxClipPlanted);
					break;

				case "crop3":

					int _seedCrop3 = (int)_SceneReferences.GetProgramVariable("_seedCrop3");
					if (_seedCrop3 > 0)
					{
						_SceneReferences.SetProgramVariable("_seedCrop3", _seedCrop3 - 1);
						StartGrowing();
					}
					if (_seedCrop3 <= 0)
					{
						_popupOutOfSeeds.SetActive(true);
					}
					_sfxSource.PlayOneShot(_sfxClipPlanted);
					break;

				case "crop4":

					int _seedCrop4 = (int)_SceneReferences.GetProgramVariable("_seedCrop4");
					if (_seedCrop4 > 0)
					{
						_SceneReferences.SetProgramVariable("_seedCrop4", _seedCrop4 - 1);
						StartGrowing();
					}
					if (_seedCrop4 <= 0)
					{
						_popupOutOfSeeds.SetActive(true);
					}
					_sfxSource.PlayOneShot(_sfxClipPlanted);
					break;

				case "crop5":

					int _seedCrop5 = (int)_SceneReferences.GetProgramVariable("_seedCrop5");
					if (_seedCrop5 > 0)
					{
						_SceneReferences.SetProgramVariable("_seedCrop5", _seedCrop5 - 1);
						StartGrowing();
					}
					if (_seedCrop5 <= 0)
					{
						_popupOutOfSeeds.SetActive(true);
					}
					_sfxSource.PlayOneShot(_sfxClipPlanted);
					break;

			}
		}*/
		if (_useSeeds == false)
		{
			StartGrowing();
			_sfxSource.PlayOneShot(_sfxClipPlanted);
		}
		
	}
	private void StartGrowing()
	{

		_meshPlanted.SetActive(true);
		_meshHarvested.SetActive(false);
		_popupOutOfSeeds.SetActive(false);

		_popupNeedsWater.SetActive(false);
		_meshWaterTrigger.SetActive(false);
		
		_currentgrowtime = 0.1f;
		_growthPhase = 0;
		_particlesDirt.Play();

		// triggers Growing() implicitely because _currentgrowtime is greater than 0
	}
	public void WaterPlant()
	{
		_popupNeedsWater.SetActive(false);
		_meshWaterTrigger.SetActive(false);
		_particleWater.Play();
		_currentgrowtime = 0.1f;

	}
	public string GenerateCropData(string data, int cropID)
	{

		string _cropData = data + _cropID.ToString();

		return _cropData;

	}

	public void HarvestCropType()
	{

		

		int _CropsPlanted = (int)_SceneReferences.GetProgramVariable("_CropsPlanted");

		string _totalCropType = GenerateCropData("_totalCrop", _cropID);
		string _currentCropType = GenerateCropData("_currentCrop", _cropID); // _currentCrop1
		string _valueCrop = GenerateCropData("_valueCrop", _cropID);

		int _totalCrop = (int)_SceneReferences.GetProgramVariable(_totalCropType);

		
		if (_isAutoBotActive == false)
		{
			
			int _currentCrop = (int)_SceneReferences.GetProgramVariable(_currentCropType);

			_SceneReferences.SetProgramVariable(_currentCropType, _currentCrop + _Yield);
		}
		
		if (_isAutoBotActive == true)
		{
			int moneyearned = _Yield * (int)_SceneReferences.GetProgramVariable(_valueCrop);
			int money = (int)_SceneReferences.GetProgramVariable("_currentMoney");
			money += moneyearned;
			_SceneReferences.SetProgramVariable("_currentMoney", money);
			UdonBehaviour _persistence = (UdonBehaviour)_SceneReferences.GetProgramVariable("_persistence");
			_persistence.SendCustomEvent("PersistData_Save");
		}
		
		_SceneReferences.SetProgramVariable(_totalCropType, _totalCrop + _Yield);
	}

	public void CollectCrop()
	{
		var c = _particlesCrop[1].collision;
		//c.enabled = false;
		//int _CropsPlanted = (int)_SceneReferences.GetProgramVariable("_CropsPlanted");

		//string _totalCropType = GenerateCropData("_totalCrop", _cropID);
		string _currentCropType = GenerateCropData("_currentCrop", _cropID); // _currentCrop1
		//string _valueCrop = GenerateCropData("_valueCrop", _cropID);

		//int _totalCrop = (int)_SceneReferences.GetProgramVariable(_totalCropType);


		int _currentCrop = (int)_SceneReferences.GetProgramVariable(_currentCropType);

		_SceneReferences.SetProgramVariable(_currentCropType, _currentCrop + 1);
		if (_debugView1.text.Length > 200)
		{
			_debugView1.text = "";
		}
		_debugView1.text += '\n' + "Item Collected" + _cropTag;

		//c.enabled = true;
	}

	public void HarvestFx()
	{
		_sfxSource.PlayOneShot(_sfxClipHarvest);

		var emission = _particlesCrop[0].emission;

		if (_Yield < _maxLevelYield)
		{
			emission.SetBursts(
		new ParticleSystem.Burst[]
		{
				new ParticleSystem.Burst(0.0f, (Single)_Yield)
		});
		}


		if (_Yield >= _maxLevelYield)
		{
			Single s = _maxLevelYield;
			emission.SetBursts(
		new ParticleSystem.Burst[]
		{
				new ParticleSystem.Burst(0.0f, s)
		});
		}


		_particlesCrop[0].Play();

	}
	public void HarvestPlant()
	{
		// check if particle system is at max
		// if it is, disallow harvesting
		// first run FX which sends particles out to collect
		// collection triggers incrementing crop amount

		//HarvestCropType();

		_meshHarvestTrigger.SetActive(false);
		_popupReadyToHarvest.SetActive(false);
		if (_isAutoBotActive == false)
		{
			HarvestFx();
		}
		_cropRoot.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		PersistData_Save();
		_cycleReseting = true;
	}

	public void UpgradeFX()
	{
		AudioSource _sfxSharedUIAudioSource = (AudioSource)_SceneReferences.GetProgramVariable("_sfxSharedUIAudioSource");
		AudioClip _sfxBuy1 = (AudioClip)_SceneReferences.GetProgramVariable("_sfxBuy1");
		_sfxSharedUIAudioSource.PlayOneShot(_sfxBuy1);
	}
	
	public void UpgradeYield()
	{
		int _money = (int)_SceneReferences.GetProgramVariable("_currentMoney");

		if (_upgradeCostYield <= _money)
		{
			if (_upgradeLevelYield < _maxLevelYield)
			{
				_upgradeLevelYield = PlayerData.GetInt(Networking.LocalPlayer, _keyUpgradeLevelYield);
				_upgradeLevelYield++;
				PlayerData.SetInt(_keyUpgradeLevelYield, _upgradeLevelYield);

				_SceneReferences.SetProgramVariable("_currentMoney", _money - _upgradeCostYield);
				UdonBehaviour _p = (UdonBehaviour)_SceneReferences.GetProgramVariable("_persistence");
				_p.SendCustomEvent("PersistData_Save");
				
				_Yield = _upgradeLevelYield;
				UpgradeFX();
			}
		}
	}

	public void UpgradeResetTime()
	{
		int _money = (int)_SceneReferences.GetProgramVariable("_currentMoney");
		if (_upgradeCostResetTime <= _money)
		{
			if (_upgradeLevelResetTime < _maxLevelResetTime)
			{
				_upgradeLevelResetTime = PlayerData.GetInt(Networking.LocalPlayer, _keyUpgradeLevelResetTime);
				_upgradeLevelResetTime++;
				PlayerData.SetInt(_keyUpgradeLevelResetTime, _upgradeLevelResetTime);

				_SceneReferences.SetProgramVariable("_currentMoney", _money - _upgradeCostResetTime);
				UdonBehaviour _p = (UdonBehaviour)_SceneReferences.GetProgramVariable("_persistence");
				_p.SendCustomEvent("PersistData_Save");

				float _upgradeMod = _upgradeLevelResetTime * _modifierResetTime;

				_ResetTime = _ResetTime - _upgradeMod;
				_txtResetTime.text = _ResetTime.ToString();
				UpgradeFX();

			}
		}
	}

	public string _debugText;
	public void Start()
	{
		_playerAPI = Networking.LocalPlayer;
		_ResetTime += _cropID;
		GenerateKeys();

		PersistData_Load();
		GenerateCropMesh();

	}

	public void GenerateCropMesh()
	{
		var tmp = (Mesh[])_SceneReferences.GetProgramVariable("_cropMeshes");
		int l = tmp.Length;
		Mesh[] meshes = new Mesh[l];
		meshes = (Mesh[])_SceneReferences.GetProgramVariable("_cropMeshes");
		_cropMesh = meshes[_cropID];
		_cropMeshFilter.mesh = _cropMesh;

		foreach (ParticleSystem p in _particlesCrop)
		{
			p.GetComponent<ParticleSystemRenderer>().mesh = _cropMesh;
		}
		
	}

	public void GenerateKeys()
	{
		_keyCurrentCropAmount = GenerateCropData("_currentCrop", _cropID);
		_keyUpgradeLevelResetTime = GenerateCropData("_upgradeLevelResetTime", _cropID);
		_keyUpgradeLevelYield = GenerateCropData("_upgradeLevelYield", _cropID);
	}

	public void PersistData_Save()
	{
		if (_playerAPI.isLocal)
		{

			int _currentCrop = (int)_SceneReferences.GetProgramVariable(_keyCurrentCropAmount);

			PlayerData.SetInt(_keyCurrentCropAmount, _currentCrop);
			PlayerData.SetInt(_keyUpgradeLevelResetTime, _upgradeLevelResetTime);
			PlayerData.SetInt(_keyUpgradeLevelYield, _upgradeLevelYield);
			
		}
	}

	public void PersistData_Load()
	{
		if (_playerAPI.isLocal)
		{

			var currentCrop = PlayerData.GetInt(Networking.LocalPlayer, _keyCurrentCropAmount);
			int _currentCrop = currentCrop;
			_SceneReferences.SetProgramVariable(_keyCurrentCropAmount, _currentCrop);
			_txtResetTime.text = _ResetTime.ToString();
			// if that didnt work, take the math out of here and let it just be in the functions ^^^

			var _bufferUpgradeLevelResetTime = PlayerData.GetInt(Networking.LocalPlayer, _keyUpgradeLevelResetTime);
			if ( _bufferUpgradeLevelResetTime > 0 )
			{
				_upgradeLevelResetTime = _bufferUpgradeLevelResetTime;

				/*float _upgradeMod = _upgradeLevelResetTime * _modifierResetTime;
				float resettime = _ResetTime + _cropID;

				_ResetTime = resettime - _upgradeMod;*/
			}

			

			var _bufferUpgradeLevelYield = PlayerData.GetInt(Networking.LocalPlayer, _keyUpgradeLevelYield);
			if (_bufferUpgradeLevelYield > 0 )
			{
				_upgradeLevelYield = _bufferUpgradeLevelYield;
				/*_Yield = _upgradeLevelYield;*/
			}
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
