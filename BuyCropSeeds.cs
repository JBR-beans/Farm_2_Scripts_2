
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class BuyCropSeeds : UdonSharpBehaviour
{
	public string _cropTag;

	private int _amountseeds;

	public UdonBehaviour _SceneReferences;
	public TextMeshProUGUI _viewSeedAmount;
	public AudioSource _shareduiaudiosource;
	public AudioClip _sfxUISoundIncreaseAmount;
	public AudioClip _sfxUISoundDecreaseAmount;
	public AudioClip _sfxUISoundBuy;
	public AudioClip _sfxUISoundBuyMax;
	public float _sfxUIvolume;


	public void IncreaseSeedAmount()
    {
		_shareduiaudiosource = (AudioSource)_SceneReferences.GetProgramVariable("_sfxSharedUIAudioSource");

		int _cropCost = (int)_SceneReferences.GetProgramVariable("_cropCost");
		int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");

		if (_cropTag == "crop1")
		{			
			int _costSeedCrop1 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop1") + _cropCost;
			int _totalCost = _costSeedCrop1 * (_amountseeds + 1);


			if (_currentMoney >= _totalCost)
			{
				_amountseeds++;
				_shareduiaudiosource.PlayOneShot(_sfxUISoundIncreaseAmount, _sfxUIvolume);
			}
		}

		if (_cropTag == "crop2")
		{
			int _costSeedCrop2 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop2") + _cropCost;
			int _totalCost = _costSeedCrop2 * (_amountseeds + 1);

			if (_currentMoney >= _totalCost)
			{
				_amountseeds++;
				_shareduiaudiosource.PlayOneShot(_sfxUISoundIncreaseAmount, _sfxUIvolume);
			}
		}
		_viewSeedAmount.text = _amountseeds.ToString();
	}

	public void DecreaseSeedAmount()
	{
		_shareduiaudiosource = (AudioSource)_SceneReferences.GetProgramVariable("_sfxSharedUIAudioSource");
		if (_amountseeds > 0)
		{
			_amountseeds--;
			_shareduiaudiosource.PlayOneShot(_sfxUISoundDecreaseAmount, _sfxUIvolume);
		}

		_viewSeedAmount.text = _amountseeds.ToString();
	}

	public void BuySeeds()
	{
		if (_amountseeds > 0)
		{
			
			int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");
			int _cropCost = (int)_SceneReferences.GetProgramVariable("_cropCost");

			if (_cropTag == "crop1")
			{
				int _costSeedCrop1 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop1") + _cropCost;
				int _totalCost = _costSeedCrop1 * _amountseeds;

				if (_currentMoney >= _totalCost)
				{
					_SceneReferences.SetProgramVariable("_seedCrop1", (int)_SceneReferences.GetProgramVariable("_seedCrop1")+_amountseeds);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _totalCost);

					_amountseeds = 0;
					_viewSeedAmount.text = "0";

					_shareduiaudiosource.PlayOneShot(_sfxUISoundBuy, _sfxUIvolume);
				}
			}

			if (_cropTag == "crop2")
			{
				int _costSeedCrop2 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop2") + _cropCost;
				int _totalCost = _costSeedCrop2 * _amountseeds;

				if (_currentMoney >= _totalCost)
				{
					_SceneReferences.SetProgramVariable("_seedCrop2", (int)_SceneReferences.GetProgramVariable("_seedCrop2") + _amountseeds);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _totalCost);

					_amountseeds = 0;
					_viewSeedAmount.text = "0";

					_shareduiaudiosource.PlayOneShot(_sfxUISoundBuy, _sfxUIvolume);
				}
				
			}
		}
	}

	public void BuyMax()
	{
		int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");
		int _cropCost = (int)_SceneReferences.GetProgramVariable("_cropCost");

		if (_cropTag == "crop1")
		{
			int _costSeedCrop1 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop1") + _cropCost;

			int _totalSeeds = (int)Mathf.Floor(_currentMoney / _costSeedCrop1);

			int _totalCost = _totalSeeds * _costSeedCrop1;


			if (_totalCost <= _currentMoney)
			{
				_SceneReferences.SetProgramVariable("_seedCrop1", (int)_SceneReferences.GetProgramVariable("_seedCrop1") +_totalSeeds);
				_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _totalCost);
			}
		}

		if (_cropTag == "crop2")
		{

			int _costSeedCrop2 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop2") + _cropCost;

			int _totalSeeds = (int)Mathf.Floor(_currentMoney / _costSeedCrop2);

			int _totalCost = _totalSeeds * _costSeedCrop2;


			if (_totalCost <= _currentMoney)
			{
				_SceneReferences.SetProgramVariable("_seedCrop2", (int)_SceneReferences.GetProgramVariable("_seedCrop2") + _totalSeeds);
				_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _totalCost);
			}
		}
	}
}
