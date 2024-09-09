using System;
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;

public class PlantGrowing : UdonSharpBehaviour
{
	// crop tag is set immediately before reseting plant
	// if changed, will apply on next growth cycle

	[Header("configure crop")]
	public float _maxGrowTime;
	public float _growSpeedMultiplier;
	public int _maxGrowthPhase;
	public int _amountHarvested;
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
	public Image _imgGrowingVisual;

	[Header("INTERNAL")]
	public string _cropTag;
	public int _growthPhase = 0;
	private float _currentgrowtime = 0;
	public bool _autoWater = false;
	public bool _autoHarvest = false;
	public bool _autoPlant = false;

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
				
				//HarvestPlant();
			}
		}
	}

	private double ConvertFrom_Range1_Input_To_Range2_Output(double _input_range_min,
		double _input_range_max, double _output_range_min,
		double _output_range_max, double _input_value_tobe_converted)
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

		// either enabling the button to harvest, or checking if the upgrade is bought
		// button is hidden once harvested automatically
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

		//int _cropCost = (int)_SceneReferences.GetProgramVariable("_cropCost");
		//int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");

		if (_cropTag == "crop1")
		{
			int _seedCrop1 = (int)_SceneReferences.GetProgramVariable("_seedCrop1");
			if (_seedCrop1 > 0)
			{
				_SceneReferences.SetProgramVariable("_seedCrop1", _seedCrop1 - 1);
				StartGrowing();
			}
		}

		if (_cropTag == "crop2")
		{

			int _seedCrop2 = (int)_SceneReferences.GetProgramVariable("_seedCrop2");
			if (_seedCrop2 > 0)
			{
				_SceneReferences.SetProgramVariable("_seedCrop2", _seedCrop2 - 1);
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
