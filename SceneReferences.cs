
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SceneReferences : UdonSharpBehaviour
{
	// fields with "total" count lifetime, for example as achievement or to unlock things
	// duplicate crop to make more different plants

	public void HUDSetCurrentCropTotals()
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
	}
	public void Update()
	{

		HUDSetCurrentCropTotals();

		_hudCurrentMoney.text = _currentMoney.ToString();
		_hudTotalMoney.text = _totalMoney.ToString();
		_hudCropsPlanted.text = _totalCrop.ToString();

		_hudCropsValue.text = _valueCrops.ToString();
	}
	[Header("Persistence Configuration")]
	public UdonBehaviour _persistence;
	[Header("Game World")]
	public AudioSource _sfxSharedUIAudioSource;
	public AudioClip _sfxBuy1;
	public AudioClip _sfxBoughtItem;
	public TextMeshProUGUI _hudCheaperCropsUpgradeBought;
	public int _totalAutoBot = 0;

	[Header("HUD")]
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

	[Header("Current money")]
	public TextMeshProUGUI _hudCurrentMoney;
	[Header("Total money")]
	public TextMeshProUGUI _hudTotalMoney;
	[Header("Total crops planted")]
	public TextMeshProUGUI _hudCropsPlanted;
	[Header("modifiers")]
	public TextMeshProUGUI _hudCropsValue;

	[Header("< INTERNAL | handled through script")]
	[Header("handled through script")]
	[Header("global modifiers")]
	public int _currentCrop = 0;
	public int _totalCrop = 0;
	public int _valueCrops = 0;
	[Header("player stats")]
	public int _currentMoney = 0;
	[Header("totals")]
	public int _totalMoney = 0;
	public int _CropsPlanted = 0;
	[Header("Unlockables")]
	public bool _unlockedAutoBot = false;

	[Header("Crop references")]
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
	public int _valueCrop10 = 0;

}

