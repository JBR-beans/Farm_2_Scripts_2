using System;
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;

public class PlantGrowing : UdonSharpBehaviour
{
	// crop tag is set immediately before reseting plant
	// if changed, will apply on next growth cycle

	[Header("Modifiers")]
	public bool _useSeeds;
	[Header("configure crop")]
	public float _maxGrowTime;
	public float _growSpeedMultiplier;
	public int _maxGrowthPhase;
	public int _amountHarvested;
	public GameObject _cropRoot;
	public UdonSharpBehaviour _SceneReferences;

	public GameObject _meshHarvested;
	public GameObject _meshPlanted;
	public GameObject _meshWaterTrigger;
	public GameObject _meshHarvestTrigger;

	[Header("configure VFX/SFX")]
	public ParticleSystem _particlesDirt;
	public ParticleSystem _particlesCrop;
	public ParticleSystem _particleWater;
	public AudioSource _sfxSource;
	public AudioClip _sfxClipHarvest;
	public AudioClip _sfxClipPlanted;
	public AudioClip _sfxClipWatered;

	[Header("configure UI")]
	public Button _btnPlant;
	public Button _btnWater;
	public Button _btnHarvest;
	public Button _btnAutoWater;
	public Button _btnAutoHarvest;
	public Button _btnAutoPlant;
	public Button _btnUpgradeYield;

	[Header("Indicators")]
	public Image _imgGrowingVisual;
	public Image _imgResetingVisual;

	// interact with to attempt ResetPlant()
	// if unsuccessful, can be reattempted
	// if successful, disables popup
	public GameObject _popupOutOfSeeds;
	public GameObject _popupNeedsWater;
	public GameObject _popupReadyToHarvest;

	[Header("INTERNAL")]
	public string _cropTag;
	public int _growthPhase = 0;
	private float _currentgrowtime = 0;
	public bool _autoWater = false;
	public bool _autoHarvest = false;
	public bool _autoPlant = false;

	private float _ticker = 0;
	private bool _cycleReseting;
	[Header("Max time til crop can be planted again")]
	public float _maxTimeCropReset;
	
	public void Update()
	{
		if (_cycleReseting == true)
		{
			_imgResetingVisual.color = Color.red;
			_ticker += Time.deltaTime;

			_imgResetingVisual.fillAmount = (float)ConvertFrom_Range1_Input_To_Range2_Output(0, _maxTimeCropReset, 0, 1, _ticker);
			_imgGrowingVisual.fillAmount = 0;

			if (_ticker > _maxTimeCropReset)
			{
				_meshPlanted.SetActive(false);
				_meshHarvested.SetActive(true);
				
				_cycleReseting = false;
				_ticker = 0;

				ReadyToPlant();

				if (_autoPlant == true)
				{
					ResetPlant();
				}
			}
		}
	}
	public void ReadyToPlant()
	{
		// using this in place of a reusable checkmark for now
		_imgResetingVisual.color = Color.green;
	}
	public void FixedUpdate()
	{
		// set _currentgrowtime to 0.0f to temporarily halt growth phases

		// simply a growing phase
		if (_currentgrowtime < _maxGrowTime && _currentgrowtime >= 0.1f)
		{
			Growing();
		}

		if (_currentgrowtime > _maxGrowTime)
		{
			// growth phase completed, check if its the last phase or needs water

			// needs to be watered
			if (_growthPhase < _maxGrowthPhase)
			{
				GrowthPhaseCompleted();
			}

			// ready to be harvested
			if (_growthPhase == _maxGrowthPhase)
			{
				LastGrowthPhase();
			}
		}
	}

	private double ConvertFrom_Range1_Input_To_Range2_Output(double _input_range_min, double _input_range_max, double _output_range_min, double _output_range_max, double _input_value_tobe_converted)
	{
		double diffOutputRange = Math.Abs((_output_range_max - _output_range_min));

		double diffInputRange = Math.Abs((_input_range_max - _input_range_min));

		double convFactor = (diffOutputRange / diffInputRange);

		return (_output_range_min + (convFactor * (_input_value_tobe_converted - _input_range_min)));
	}

	// general logic for crop growing
	private void Growing()
	{
		_currentgrowtime += Time.fixedDeltaTime;
		_cropRoot.transform.localScale += new Vector3(Time.fixedDeltaTime* _growSpeedMultiplier, Time.fixedDeltaTime * _growSpeedMultiplier, Time.fixedDeltaTime * _growSpeedMultiplier);

		_imgGrowingVisual.fillAmount = (float)ConvertFrom_Range1_Input_To_Range2_Output(0, _maxGrowTime, 0, 1, _currentgrowtime);
		_imgResetingVisual.fillAmount = 0;

		ButtonHandler(false, false, false);
	}

	
	public void GrowthPhaseCompleted()
	{
		// while 0, growing is paused
		// set to greater than 0.1f to start growing cycle again
		_currentgrowtime = 0;
		_meshWaterTrigger.SetActive(true);
		_popupNeedsWater.SetActive(true);
		ButtonHandler(false, true, false);
		_imgGrowingVisual.fillAmount = 0;
		_imgResetingVisual.fillAmount = 0;

		if (_autoWater == true)
		{
			_meshWaterTrigger.SetActive(false);
			_sfxSource.PlayOneShot(_sfxClipWatered);
			_particleWater.Play();
			WaterPlant();
		}

	}
	
	public void LastGrowthPhase()
	{
		// just stalling _currentgrowtime without reseting it to 0
		_currentgrowtime = 0;

		// either enabling the button to harvest, or checking if the upgrade is bought
		// button is hidden once harvested automatically
		ButtonHandler(false, false, true);
		_meshHarvestTrigger.SetActive(true);
		_popupReadyToHarvest.SetActive(true);
		_imgGrowingVisual.fillAmount = 0;
		_imgResetingVisual.fillAmount = 0;

		if (_autoHarvest == true)
		{
			HarvestPlant();
			_meshHarvestTrigger.SetActive(false);
		}
	}
	private void ButtonHandler(bool plant, bool water, bool harvest)
	{
		_btnPlant.gameObject.SetActive(plant);
		_btnWater.gameObject.SetActive(water);
		_btnHarvest.gameObject.SetActive(harvest);
	}
	
	// public custom events to be called by buttons
	public void ResetPlant()
	{
		// "plants" a new crop
		if (_useSeeds == true)
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
		}
		else
		{
			StartGrowing();
			_sfxSource.PlayOneShot(_sfxClipPlanted);
		}
		
		ButtonHandler(true, false, false);
	}
	private void StartGrowing()
	{
		_meshPlanted.SetActive(true);
		_meshHarvested.SetActive(false);
		_popupOutOfSeeds.SetActive(false);
		_currentgrowtime = 0.1f;
		_growthPhase = 0;
		_particlesDirt.Play();
		ButtonHandler(false, false, false);

		// triggers Growing() implicitely because _currentgrowtime is greater than 0
	}
	public void WaterPlant()
	{
		_popupNeedsWater.SetActive(false);
		_meshWaterTrigger.SetActive(false);
		ButtonHandler(false, false, false);
		_currentgrowtime = 0.1f;
		_growthPhase++;
	}
	public void HarvestPlant()
	{

		_cropRoot.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		int _CropsPlanted = (int)_SceneReferences.GetProgramVariable("_CropsPlanted");
		ButtonHandler(true, false, false);

		_sfxSource.PlayOneShot(_sfxClipHarvest);
		_meshHarvestTrigger.SetActive(false);
		_popupReadyToHarvest.SetActive(false);

		switch (_cropTag)
		{
			case "crop1":

				int _currentCrop1 = (int)_SceneReferences.GetProgramVariable("_currentCrop1");
				int _totalCrop1 = (int)_SceneReferences.GetProgramVariable("_totalCrop1");

				// add logic for what happens with the harvested plants data
				_SceneReferences.SetProgramVariable("_currentCrop1", _currentCrop1 + _amountHarvested);
				_SceneReferences.SetProgramVariable("_totalCrop1", _totalCrop1 + _amountHarvested);

				break;

			case "crop2":

				int _currentCrop2 = (int)_SceneReferences.GetProgramVariable("_currentCrop2");
				int _totalCrop2 = (int)_SceneReferences.GetProgramVariable("_totalCrop2");

				// add logic for what happens with the harvested plants data
				_SceneReferences.SetProgramVariable("_currentCrop2", _currentCrop2 + _amountHarvested);
				_SceneReferences.SetProgramVariable("_totalCrop2", _totalCrop2 + _amountHarvested);

				break;

			case "crop3":

				int _currentCrop3 = (int)_SceneReferences.GetProgramVariable("_currentCrop3");
				int _totalCrop3 = (int)_SceneReferences.GetProgramVariable("_totalCrop3");

				// add logic for what happens with the harvested plants data
				_SceneReferences.SetProgramVariable("_currentCrop3", _currentCrop3 + _amountHarvested);
				_SceneReferences.SetProgramVariable("_totalCrop3", _totalCrop3 + _amountHarvested);

				break;

			case "crop4":

				int _currentCrop4 = (int)_SceneReferences.GetProgramVariable("_currentCrop4");
				int _totalCrop4 = (int)_SceneReferences.GetProgramVariable("_totalCrop4");

				// add logic for what happens with the harvested plants data
				_SceneReferences.SetProgramVariable("_currentCrop4", _currentCrop4 + _amountHarvested);
				_SceneReferences.SetProgramVariable("_totalCrop4", _totalCrop4 + _amountHarvested);

				break;

			case "crop5":

				int _currentCrop5 = (int)_SceneReferences.GetProgramVariable("_currentCrop5");
				int _totalCrop5 = (int)_SceneReferences.GetProgramVariable("_totalCrop5");

				// add logic for what happens with the harvested plants data
				_SceneReferences.SetProgramVariable("_currentCrop5", _currentCrop5 + _amountHarvested);
				_SceneReferences.SetProgramVariable("_totalCrop5", _totalCrop5 + _amountHarvested);


				break;

		}
		

		_particlesCrop.Play();


		_cycleReseting = true;

		_SceneReferences.SetProgramVariable("_CropsPlanted", _CropsPlanted + 1);
	}

	// upgrades per local crop plot
	public void UpgradeAutoWater()
	{
		int _money = (int)_SceneReferences.GetProgramVariable("_currentMoney");
		int _cost = (int)_SceneReferences.GetProgramVariable("_costUpgradeAutoWater");
		if (_cost <= _money)
		{
			_autoWater = true;
			_SceneReferences.SetProgramVariable("_currentMoney", _money - _cost);
			_btnAutoWater.interactable = false;
		}
	}
	public void UpgradeAutoHarvest()
	{
		int _money = (int)_SceneReferences.GetProgramVariable("_currentMoney");
		int _cost = (int)_SceneReferences.GetProgramVariable("_costUpgradeAutoHarvest");
		if (_cost <= _money)
		{
			_autoHarvest = true;
			_SceneReferences.SetProgramVariable("_currentMoney", _money - _cost);
			_btnAutoHarvest.interactable = false;
		}
	}
	public void UpgradeAutoPlant()
	{
		int _money = (int)_SceneReferences.GetProgramVariable("_currentMoney");
		int _cost = (int)_SceneReferences.GetProgramVariable("_costUpgradeAutoPlants");
		if (_cost <= _money)
		{
			_autoPlant = true;
			_SceneReferences.SetProgramVariable("_currentMoney", _money - _cost);
			_btnAutoPlant.interactable = false;
		}
	}

	public void UpgradeYield()
	{
		int _money = (int)_SceneReferences.GetProgramVariable("_currentMoney");
		int _cost = (int)_SceneReferences.GetProgramVariable("_costUpgradeYield");
		if (_cost <= _money)
		{
			if (_amountHarvested < 3)
			{
				_amountHarvested++;
			}

			_SceneReferences.SetProgramVariable("_currentMoney", _money - _cost);
			_btnUpgradeYield.interactable = false;
		}
		
	}
}
