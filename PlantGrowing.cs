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
	[Header("Crop Data")]
	public UdonSharpBehaviour _SceneReferences;
	public GameObject _CropRoot;
	public int _cropID;
	public string _cropName;
	public int _currentCrop;
	public int _totalCrop;
	public int _valueCrop;
	public bool _boughtCrop;

	[Header("Visuals")]
	public MeshFilter _cropMeshFilter;
	public ParticleSystem _particlesMeshFX;
	public ParticleSystem _particlesCollectable;
	public ParticleSystem _particlesDirt;
	public ParticleSystem _particleWater;
	public ParticleSystem _particlesSmallExplosion;
	public GameObject _meshHarvested;
	public GameObject _meshPlanted;
	public GameObject _meshWaterTrigger;
	public GameObject _meshHarvestTrigger;
	public Image _imgGrowingVisual;
	public Image _imgResetingVisual;
	public GameObject _popupNeedsWater;
	public GameObject _popupReadyToHarvest;

	[Header("Audio")]
	public AudioSource _sfxSource;
	public AudioClip _sfxClipHarvest;
	public AudioClip _sfxClipPlanted;
	public AudioClip _sfxClipWatered;
	public AudioClip _sfxClipCollect;

	[Header("UI")]
	public TextMeshProUGUI _displayCropID;
	public TextMeshProUGUI _displayCropName;
	public TextMeshProUGUI _displayCropAmount;
	public TextMeshProUGUI _displayCropValue;
	public TextMeshProUGUI _displayCropLevelYield;
	public TextMeshProUGUI _displayCropLevelResetTime;
	public GameObject _buyButton;
	public TextMeshProUGUI _displayResetTime;

	[Header("Upgrades")]
	public int _Yield = 1;
	public float _ResetTime;
	public int _modiferYield;
	public int _maxLevelYield;
	public int _upgradeLevelYield = 1;
	public int _upgradeLevelResetTime = 1;
	public float _modifierResetTime;
	public float _maxLevelResetTime;

	[Header("Upgrade Costs")]
	public int _upgradeCostYield;
	public int _upgradeCostResetTime;

	[Header("INTERNAL")]
	public int _growthPhase = 0;
	private float _currentgrowtime = 0;
	private float _ticker = 0;
	private bool _cycleReseting;
	public float _maxGrowTime;
	public float _growSpeedMultiplier;
	public int _maxGrowthPhase;
	public GameObject _cropRoot;
	public bool _isAutoBotActive;


	public void BuyPlot()
	{
		int money = (int)_SceneReferences.GetProgramVariable("_currentMoney");
		int _cost = 1;
		if (money >= _cost)
		{

		}
	}
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
		
		StartGrowing();
		_sfxSource.PlayOneShot(_sfxClipPlanted);
	}
	private void StartGrowing()
	{

		_meshPlanted.SetActive(true);
		_meshHarvested.SetActive(false);

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

	public void HarvestAutobot()
	{
		_sfxSource.PlayOneShot(_sfxClipHarvest);
		AutoSell();

	}
	public void AutoSell()
	{

		int earned = _Yield * _valueCrop;

		_SceneReferences.SetProgramVariable("_currentMoney", (int)_SceneReferences.GetProgramVariable("_currentMoney") + earned);

	}
	public void HarvestFx()
	{
		_sfxSource.PlayOneShot(_sfxClipHarvest);

		var emission1 = _particlesMeshFX.emission;

		if (_Yield <= _maxLevelYield)
		{
			emission1.SetBursts(
		new ParticleSystem.Burst[]
		{
				new ParticleSystem.Burst(0.0f, (Single)_Yield)
		});
		}

		_particlesSmallExplosion.Play();
		_particlesMeshFX.Play();

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
		if (_isAutoBotActive == true)
		{
			HarvestAutobot();
		}
		_cropRoot.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

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
				//_upgradeLevelYield = PlayerData.GetInt(Networking.LocalPlayer, _keyUpgradeLevelYield);
				_upgradeLevelYield++;
				//PlayerData.SetInt(_keyUpgradeLevelYield, _upgradeLevelYield);

				_SceneReferences.SetProgramVariable("_currentMoney", _money - _upgradeCostYield);
				//UdonBehaviour _p = (UdonBehaviour)_SceneReferences.GetProgramVariable("_persistence");
				//_p.SendCustomEvent("PersistData_Save");
				
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
				//_upgradeLevelResetTime = PlayerData.GetInt(Networking.LocalPlayer, _keyUpgradeLevelResetTime);
				_upgradeLevelResetTime++;
				//PlayerData.SetInt(_keyUpgradeLevelResetTime, _upgradeLevelResetTime);

				_SceneReferences.SetProgramVariable("_currentMoney", _money - _upgradeCostResetTime);
				//UdonBehaviour _p = (UdonBehaviour)_SceneReferences.GetProgramVariable("_persistence");
				//_p.SendCustomEvent("PersistData_Save");

				float _upgradeMod = _upgradeLevelResetTime * _modifierResetTime;

				_ResetTime = _ResetTime - _upgradeMod;
				_displayResetTime.text = _ResetTime.ToString();
				UpgradeFX();

			}
		}
	}

	public void Start()
	{
		_ResetTime += _cropID;
	}

}

