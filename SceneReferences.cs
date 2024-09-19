
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
	//public int _costUpgradeCheaperCrops;
	public int _costUpgradeCropValue;
	public int _costUpgradeYield;

	public void FixedUpdate()
	{

		_hudCurrentCrop1.text = _currentCrop1.ToString();
		_hudCurrentCrop2.text = _currentCrop2.ToString();
		_hudCurrentCrop3.text = _currentCrop3.ToString();
		_hudCurrentCrop4.text = _currentCrop4.ToString();
		_hudCurrentCrop5.text = _currentCrop5.ToString();
/*
		_hudCurrentCrop1Seeds.text = _seedCrop1.ToString();
		_hudCurrentCrop2Seeds.text = _seedCrop2.ToString();
		_hudCurrentCrop3Seeds.text = _seedCrop3.ToString();
		_hudCurrentCrop4Seeds.text = _seedCrop4.ToString();
		_hudCurrentCrop5Seeds.text = _seedCrop5.ToString();*/

		_hudCrop1Value.text = (_valueCrop1 + _valueCrops).ToString();
		_hudCrop2Value.text = (_valueCrop2 + _valueCrops).ToString();
		_hudCrop3Value.text = (_valueCrop3 + _valueCrops).ToString();
		_hudCrop4Value.text = (_valueCrop4 + _valueCrops).ToString();
		_hudCrop5Value.text = (_valueCrop5 + _valueCrops).ToString();
/*
		_hudCostSeedsCrop1.text = (_costSeedCrop1+_cropCost).ToString();
		_hudCostSeedsCrop2.text = (_costSeedCrop2 + _cropCost).ToString();
		_hudCostSeedsCrop3.text = (_costSeedCrop3 + _cropCost).ToString();
		_hudCostSeedsCrop4.text = (_costSeedCrop4 + _cropCost).ToString();
		_hudCostSeedsCrop5.text = (_costSeedCrop5 + _cropCost).ToString();*/

		_hudTotalCrop1.text = _totalCrop1.ToString();
		_hudTotalCrop2.text = _totalCrop2.ToString();
		_hudTotalCrop3.text = _totalCrop3.ToString();
		_hudTotalCrop4.text = _totalCrop4.ToString();
		_hudTotalCrop5.text = _totalCrop5.ToString();

		_hudCurrentMoney.text = _currentMoney.ToString();

		_hudTotalMoney.text = _totalMoney.ToString();
		_hudCropsPlanted.text = _totalCrop.ToString();

		//_hudCropsCost.text = _cropCost.ToString();
		_hudCropsValue.text = _valueCrops.ToString();
	}

	public AudioClip _sfxBoughtItem;
	public TextMeshProUGUI _hudCheaperCropsUpgradeBought;
	// global upgrades applied to all crops at once
	/*public void UpgradeCheaperCrops()
	{
		if (_currentMoney >= _costUpgradeCheaperCrops && _cropCost > 0)
		{
			_sfxSharedUIAudioSource.PlayOneShot(_sfxBoughtItem, 0.5f);
			_cropCost--;
			_currentMoney -= _costUpgradeCheaperCrops;
			_hudCheaperCropsUpgradeBought.text = "Sold!";
		}
	}*/
	public TextMeshProUGUI _hudMoreValuableCropsUpgradeBought;
	public void UpgradeCropValue()
	{
		if (_currentMoney >= _costUpgradeCropValue && _valueCrops < 1)
		{
			_sfxSharedUIAudioSource.PlayOneShot(_sfxBoughtItem, 0.5f);
			_valueCrops++;
			_currentMoney -= _costUpgradeCropValue;
			_hudMoreValuableCropsUpgradeBought.text = "Sold!";
		}
	}

	[Header("crop1")]
	public string _tagCrop1 = "crop1";
	public int _currentCrop1 = 0;
	public int _totalCrop1 = 0;
	//public int _costSeedCrop1 = 0;
	//public int _seedCrop1 = 0;
	public int _valueCrop1 = 0;

	[Header("crop2")]
	public string _tagCrop2 = "crop2";
	public int _currentCrop2 = 0;
	public int _totalCrop2 = 0;
	//public int _costSeedCrop2 = 0;
	//public int _seedCrop2 = 0;
	public int _valueCrop2 = 0;

	[Header("crop3")]
	public string _tagCrop3 = "crop3";
	public int _currentCrop3 = 0;
	public int _totalCrop3 = 0;
	//public int _costSeedCrop3 = 0;
	//public int _seedCrop3 = 0;
	public int _valueCrop3 = 0;

	[Header("crop4")]
	public string _tagCrop4 = "crop4";
	public int _currentCrop4 = 0;
	public int _totalCrop4 = 0;
	//public int _costSeedCrop4 = 0;
	//public int _seedCrop4 = 0;
	public int _valueCrop4 = 0;

	[Header("crop5")]
	public string _tagCrop5 = "crop5";
	public int _currentCrop5 = 0;
	public int _totalCrop5 = 0;
	//public int _costSeedCrop5 = 0;
	//public int _seedCrop5 = 0;
	public int _valueCrop5 = 0;

	[Header("HUD")]
	[Header("Display current crop amounts")]
	public TextMeshProUGUI _hudCurrentCrop1;
	public TextMeshProUGUI _hudCurrentCrop2;
	public TextMeshProUGUI _hudCurrentCrop3;
	public TextMeshProUGUI _hudCurrentCrop4;
	public TextMeshProUGUI _hudCurrentCrop5;

	/*	public TextMeshProUGUI _hudCurrentCrop1Seeds;
		public TextMeshProUGUI _hudCurrentCrop2Seeds;
		public TextMeshProUGUI _hudCurrentCrop3Seeds;
		public TextMeshProUGUI _hudCurrentCrop4Seeds;
		public TextMeshProUGUI _hudCurrentCrop5Seeds;*/
	[Header("Display total crop amounts")]
	public TextMeshProUGUI _hudTotalCrop1;
	public TextMeshProUGUI _hudTotalCrop2;
	public TextMeshProUGUI _hudTotalCrop3;
	public TextMeshProUGUI _hudTotalCrop4;
	public TextMeshProUGUI _hudTotalCrop5;

	/*	public TextMeshProUGUI _hudCostSeedsCrop1;
		public TextMeshProUGUI _hudCostSeedsCrop2;
		public TextMeshProUGUI _hudCostSeedsCrop3;
		public TextMeshProUGUI _hudCostSeedsCrop4;
		public TextMeshProUGUI _hudCostSeedsCrop5;*/

	[Header("Display crop values")]
	public TextMeshProUGUI _hudCrop1Value;
	public TextMeshProUGUI _hudCrop2Value;
	public TextMeshProUGUI _hudCrop3Value;
	public TextMeshProUGUI _hudCrop4Value;
	public TextMeshProUGUI _hudCrop5Value;

	[Header("Current money")]
	public TextMeshProUGUI _hudCurrentMoney;
	[Header("Total money")]
	public TextMeshProUGUI _hudTotalMoney;
	[Header("Total crops planted")]
	public TextMeshProUGUI _hudCropsPlanted;


	//public TextMeshProUGUI _hudCropsCost;
	[Header("Affects every crops value")]
	public TextMeshProUGUI _hudCropsValue;

	[Header("Game World")]
	public AudioSource _sfxSharedUIAudioSource;
	public AudioClip _sfxBuy1;

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
	[Header("Unlockables")]
	public bool _unlockedAutoBot = false;
}
