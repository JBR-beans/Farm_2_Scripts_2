
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SceneReferences : UdonSharpBehaviour
{
    // fields with "total" count lifetime, for example as achievement or to unlock things
    // duplicate crop to make more different plants

	[Header("upgrade costs")]
	public int _costUpgradeAutoWater;
	public int _costUpgradeAutoHarvest;
	public int _costUpgradeAutoPlants;
	public int _costUpgradeCheaperCrops;
	public int _costUpgradeCropValue;
	public int _costUpgradeYield;

	[Header("Unlock costs")]
	public int _costUnlockCrop2Plot;

	public void FixedUpdate()
	{

		string b = string.Format("Total Crop1: {0}", _totalCrop1);
		string b2 = string.Format("Total Crop2: {0}", _totalCrop2);

		string d = string.Format("Total Money: {0}", _totalMoney);
		string e = string.Format("Total Crops Planted: {0}", _CropsPlanted);

		string f = string.Format("Global Crop Cost Modifier: {0}", _cropCost);
		string g = string.Format("Global Crop Value Modifier: {0}", _valueCrops);

		_hudCurrentCrop1.text = _currentCrop1.ToString();
		_hudCurrentCrop2.text = _currentCrop2.ToString();

		_hudCurrentCrop1Seeds.text = _seedCrop1.ToString();
		_hudCurrentCrop2Seeds.text = _seedCrop2.ToString();

		_hudCrop1Value.text = (_valueCrop1 + _valueCrops).ToString();
		_hudCrop2Value.text = (_valueCrop2 + _valueCrops).ToString();

		_hudCostSeedsCrop1.text = (_costSeedCrop1+_cropCost).ToString();
		_hudCostSeedsCrop2.text = (_costSeedCrop2 + _cropCost).ToString();

		_hudTotalCrop1.text = b;
		_hudTotalCrop2.text = b2;

		_hudCurrentMoney.text = _currentMoney.ToString(); ;

		_hudTotalMoney.text = d;
		_hudCropsPlanted.text = e;

		_hudCropsCost.text = f;
		_hudCropsValue.text = g;
	}

	// global upgrades applied to all crops at once
	public void UpgradeCheaperCrops()
	{
		if (_currentMoney >= _costUpgradeCheaperCrops && _cropCost >= 0)
		{
			_cropCost--;
			_currentMoney -= _costUpgradeCheaperCrops;
		}
	}

	public void UpgradeCropValue()
	{
		if (_currentMoney >= _costUpgradeCropValue && _valueCrops <= 5)
		{
			_valueCrops++;
			_currentMoney -= _costUpgradeCropValue;
		}
	}

	[Header("crop1")]
	public string _tagCrop1 = "crop1";
	public int _currentCrop1 = 0;
	public int _totalCrop1 = 0;
	public int _costSeedCrop1 = 0;
	public int _seedCrop1 = 0;
	public int _valueCrop1 = 0;

	[Header("crop2")]
	public string _tagCrop2 = "crop2";
	public int _currentCrop2 = 0;
	public int _totalCrop2 = 0;
	public int _costSeedCrop2 = 0;
	public int _seedCrop2 = 0;
	public int _valueCrop2 = 0;


	[Header("HUD")]
	public TextMeshProUGUI _hudCurrentCrop1;
	public TextMeshProUGUI _hudCurrentCrop2;

	public TextMeshProUGUI _hudCurrentCrop1Seeds;
	public TextMeshProUGUI _hudCurrentCrop2Seeds;

	public TextMeshProUGUI _hudTotalCrop1;
	public TextMeshProUGUI _hudTotalCrop2;

	public TextMeshProUGUI _hudCostSeedsCrop1;
	public TextMeshProUGUI _hudCostSeedsCrop2;

	public TextMeshProUGUI _hudCrop1Value;
	public TextMeshProUGUI _hudCrop2Value;

	public TextMeshProUGUI _hudCurrentMoney;
	public TextMeshProUGUI _hudTotalMoney;
	public TextMeshProUGUI _hudCropsPlanted;

	public TextMeshProUGUI _hudCropsCost;
	public TextMeshProUGUI _hudCropsValue;

	[Header("Game World")]
	public AudioSource _sfxSharedUIAudioSource;

	[Header("INTERNAL")]
	[Header("global stats")]
	public int _currentCrop = 0;
	public int _totalCrop = 0;
	public int _cropCost = 0;
	public int _valueCrops = 0;
	[Header("player stats")]
	public int _currentMoney = 0;
	[Header("totals")]
	public int _totalMoney = 0;
	public int _CropsPlanted = 0;
}
