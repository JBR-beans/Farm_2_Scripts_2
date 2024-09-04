using System.Runtime.CompilerServices;
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;

public class PlantGrowing : UdonSharpBehaviour
{
	// crop tag is set immediately before reseting plant
	// if changed, will apply on next growth cycle

	[Header("configure crop")]
	public string _cropTag;
	public float _maxGrowTime;
	public float _growSpeedMultiplier;
	public int _maxGrowthPhase;

	[Header("")]
	public int _amountHarvested;
	
	[Header("")]
	public GameObject _cropRoot;
	public UdonSharpBehaviour _SceneReferences;

	[Header("configure UI")]
	public TextMeshProUGUI _label;
	public Button _btnPlant;
	public Button _btnWater;
	public Button _btnHarvest;
	public Button _btnAutoWater;
	public Button _btnAutoHarvest;
	public Button _btnAutoPlant;

	[Header("INTERNAL")]
	public int _growthPhase = 0;
	private float _currentgrowtime = 0;

	public bool _autoWater = false;
	public bool _autoHarvest = false;
	public bool _autoPlant = false;

	public void FixedUpdate()
	{
		// implicitely stops growth phases if _currentgrowtime is under 0.1f, creating a pause in growing without spamming that its stopped in FixedUpdate, without using bools 
		// start growing again by setting to 0.1f

		// simply an growing phase
		if (_currentgrowtime < _maxGrowTime && _currentgrowtime >= 0.1f)
		{
			Growing();
		}

		if (_currentgrowtime > _maxGrowTime)
		{
			// needs to be watered
			if (_growthPhase < _maxGrowthPhase)
			{
				GrowthPhaseCompleted();
			}

			// ready to be harvested
			if (_growthPhase == _maxGrowthPhase)
			{
				LastGrowthPhase();
				
				//HarvestPlant();
			}
		}
	}

	// general logic for crop growing
	private void Growing()
	{
		_currentgrowtime += Time.fixedDeltaTime;
		_cropRoot.transform.localScale += new Vector3(Time.fixedDeltaTime* _growSpeedMultiplier, Time.fixedDeltaTime * _growSpeedMultiplier, Time.fixedDeltaTime * _growSpeedMultiplier);

		ButtonHandler(false, false, false);
	}
	public void GrowthPhaseCompleted()
	{
		_currentgrowtime = 0;

		ButtonHandler(false, true, false);

		if (_autoWater == true)
		{
			WaterPlant();
		}

	}
	public void LastGrowthPhase()
	{
		// just stalling _currentgrowtime without reseting it to 0
		_currentgrowtime = 0;

		ButtonHandler(false, false, true);

		if (_autoHarvest == true)
		{
			HarvestPlant();
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

		// either here or on the button you can add logic involving tracking plant data or moneys to buy a new plant, etc.
		_label.text = _cropTag;

		int _cropCost = (int)_SceneReferences.GetProgramVariable("_cropCost");
		int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");

		if (_cropTag == "crop1")
		{
			int _costCrop1 = (int)_SceneReferences.GetProgramVariable("_costCrop1") + _cropCost;
			if (_currentMoney >= _costCrop1)
			{
				
				_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _costCrop1);
				StartGrowing();
			}
		}

		if (_cropTag == "crop2")
		{
			int _costCrop2 = (int)_SceneReferences.GetProgramVariable("_costCrop2") + _cropCost;
			if (_currentMoney >= _costCrop2)
			{

				_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _costCrop2);
				StartGrowing();
			}
		}
		ButtonHandler(true, false, false);
	}
	private void StartGrowing()
	{
		_currentgrowtime = 0.1f;
		_growthPhase = 0;
		ButtonHandler(false, false, false);
	}
	public void WaterPlant()
	{
		ButtonHandler(false, false, false);
		_currentgrowtime = 0.1f;
		_growthPhase++;
	}
	public void HarvestPlant()
	{

		_cropRoot.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		int _CropsPlanted = (int)_SceneReferences.GetProgramVariable("_CropsPlanted");
		ButtonHandler(true, false, false);

		if (_cropTag == "crop1")
		{
			int _currentCrop1 = (int)_SceneReferences.GetProgramVariable("_currentCrop1");
			int _totalCrop1 = (int)_SceneReferences.GetProgramVariable("_totalCrop1");

			// add logic for what happens with the harvested plants data
			_SceneReferences.SetProgramVariable("_currentCrop1", _currentCrop1 + _amountHarvested);
			_SceneReferences.SetProgramVariable("_totalCrop1", _totalCrop1 + _amountHarvested);
			
		}
		
		if (_cropTag == "crop2")
		{
			int _currentCrop2 = (int)_SceneReferences.GetProgramVariable("_currentCrop2");
			int _totalCrop2 = (int)_SceneReferences.GetProgramVariable("_totalCrop2");

			_SceneReferences.SetProgramVariable("_currentCrop2", _currentCrop2 + _amountHarvested);
			_SceneReferences.SetProgramVariable("_totalCrop2", _totalCrop2 + _amountHarvested);
		}

		
		if (_autoPlant == true)
		{
			ResetPlant();
		}
		_SceneReferences.SetProgramVariable("_CropsPlanted", _CropsPlanted + 1);
	}

	// upgrades per local crop plot
	public void UpgradeAutoWater()
	{
		int _money = (int)_SceneReferences.GetProgramVariable("_currentMoney");
		int _cost = (int)_SceneReferences.GetProgramVariable("_costUpgradeAutoWater");
		if (_cost < _money)
		{
			_autoWater = true;
			_btnAutoWater.interactable = false;
		}
	}
	public void UpgradeAutoHarvest()
	{
		int _money = (int)_SceneReferences.GetProgramVariable("_currentMoney");
		int _cost = (int)_SceneReferences.GetProgramVariable("_costUpgradeAutoHarvest");
		if (_cost < _money)
		{
			_autoHarvest = true;
			_btnAutoHarvest.interactable = false;
		}
	}
	public void UpgradeAutoPlant()
	{
		int _money = (int)_SceneReferences.GetProgramVariable("_currentMoney");
		int _cost = (int)_SceneReferences.GetProgramVariable("_costUpgradeAutoPlants");
		if (_cost < _money)
		{
			_autoPlant = true;
			_btnAutoPlant.interactable = false;
		}
	}

	// set crop type tags
	public void SetCropTypeCrop1()
	{
		_cropTag = "crop1";
		_label.text = _cropTag;
	}
	public void SetCropTypeCrop2()
	{
		_cropTag = "crop2";
		_label.text = _cropTag;
	}
}
