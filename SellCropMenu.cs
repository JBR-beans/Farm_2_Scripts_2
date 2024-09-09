
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SellCropMenu : UdonSharpBehaviour
{
	public string _cropTag;

    public UdonBehaviour _SceneReferences;

    private int _amountCrop;

	public TextMeshProUGUI _viewCropAmount;

	public AudioClip _sfxUISoundIncreaseAmount;
	public AudioClip _sfxUISoundDecreaseAmount;
	public AudioClip _sfxUISoundSell;
	public AudioClip _sfxUISoundSellAll;
	public float _sfxUIvolume;

	public AudioSource _shareduiaudiosource;

	

	public void IncreaseCropAmount()
    {
		_shareduiaudiosource = (AudioSource)_SceneReferences.GetProgramVariable("_sfxSharedUIAudioSource");
		
		if (_cropTag == (string)_SceneReferences.GetProgramVariable("_tagCrop1"))
		{
			int _currentCrop = (int)_SceneReferences.GetProgramVariable("_currentCrop1");
			if (_currentCrop > _amountCrop)
			{
				_amountCrop++;
				_shareduiaudiosource.PlayOneShot(_sfxUISoundIncreaseAmount, _sfxUIvolume);
			}
		}

		if (_cropTag == (string)_SceneReferences.GetProgramVariable("_tagCrop2"))
		{
			int _currentCrop = (int)_SceneReferences.GetProgramVariable("_currentCrop2");
			if (_currentCrop > _amountCrop)
			{
				_amountCrop++;
				_shareduiaudiosource.PlayOneShot(_sfxUISoundIncreaseAmount, _sfxUIvolume);
			}
		}
		_viewCropAmount.text = _amountCrop.ToString();
	}
	public void DecreaseCropAmount()
	{
		_shareduiaudiosource = (AudioSource)_SceneReferences.GetProgramVariable("_sfxSharedUIAudioSource");
		if (_amountCrop > 0)
		{
			_amountCrop--;
			_shareduiaudiosource.PlayOneShot(_sfxUISoundDecreaseAmount, _sfxUIvolume);
		}
		_viewCropAmount.text = _amountCrop.ToString();
	}
	public void SellCrop()
	{
		_shareduiaudiosource = (AudioSource)_SceneReferences.GetProgramVariable("_sfxSharedUIAudioSource");
		int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");
		int _totalMoney = (int)_SceneReferences.GetProgramVariable("_totalMoney");
		int _valueCrops = (int)_SceneReferences.GetProgramVariable("_valueCrops");

		if (_cropTag == "crop1")
		{
			int _currentCrop1 = (int)_SceneReferences.GetProgramVariable("_currentMoney");

			if (_currentCrop1 > 0)
			{
				int _valueCrop1 = (int)_SceneReferences.GetProgramVariable("_valueCrop1") + _valueCrops;
				int _moneyEarned = _valueCrop1 * _amountCrop;

				_SceneReferences.SetProgramVariable("_currentCrop1", _currentCrop1 - _amountCrop);
				_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney + _moneyEarned);
				_SceneReferences.SetProgramVariable("_totalMoney", _totalMoney + _moneyEarned);

				_shareduiaudiosource.PlayOneShot(_sfxUISoundSell, _sfxUIvolume);
			}
			
		}

		if (_cropTag == "crop2")
		{
			int _currentCrop2 = (int)_SceneReferences.GetProgramVariable("_currentCrop2");

			if (_currentCrop2 > 0)
			{
				int _valueCrop2 = (int)_SceneReferences.GetProgramVariable("_valueCrop2") + _valueCrops;
				int _moneyEarned = _valueCrop2 * _amountCrop;

				_SceneReferences.SetProgramVariable("_currentCrop2", _currentCrop2 - _amountCrop);
				_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney + _moneyEarned);
				_SceneReferences.SetProgramVariable("_totalMoney", _totalMoney + _moneyEarned);

				_shareduiaudiosource.PlayOneShot(_sfxUISoundSell, _sfxUIvolume);
			}
		}

		_amountCrop = 0;
		_viewCropAmount.text = "0";
	}

	public void SellMax()
	{
		_shareduiaudiosource = (AudioSource)_SceneReferences.GetProgramVariable("_sfxSharedUIAudioSource");
		int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");
		int _totalMoney = (int)_SceneReferences.GetProgramVariable("_totalMoney");
		int _valueCrops = (int)_SceneReferences.GetProgramVariable("_valueCrops");

		if (_cropTag == "crop1")
		{
			int _currentCrop1 = (int)_SceneReferences.GetProgramVariable("_currentCrop1");
			_amountCrop = _currentCrop1;
			if (_currentCrop1 > 0)
			{
				int _valueCrop1 = (int)_SceneReferences.GetProgramVariable("_valueCrop1") + _valueCrops;
				int _moneyEarned = _valueCrop1 * _amountCrop;
				

				_SceneReferences.SetProgramVariable("_currentCrop1", 0);
				_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney + _moneyEarned);
				_SceneReferences.SetProgramVariable("_totalMoney", _totalMoney + _moneyEarned);

				_shareduiaudiosource.PlayOneShot(_sfxUISoundSell, _sfxUIvolume);
			}

		}

		if (_cropTag == "crop2")
		{
			int _currentCrop2 = (int)_SceneReferences.GetProgramVariable("_currentCrop2");
			_amountCrop = _currentCrop2;

			if (_currentCrop2 > 0)
			{
				int _valueCrop2 = (int)_SceneReferences.GetProgramVariable("_valueCrop2") + _valueCrops;
				int _moneyEarned = _valueCrop2 * _amountCrop;
				
				_SceneReferences.SetProgramVariable("_currentCrop2",0);
				_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney + _moneyEarned);
				_SceneReferences.SetProgramVariable("_totalMoney", _totalMoney + _moneyEarned);

				_shareduiaudiosource.PlayOneShot(_sfxUISoundSell, _sfxUIvolume);
			}
		}
		_amountCrop = 0;
		_viewCropAmount.text = "0";
	}
	public void SellAll()
	{
		_shareduiaudiosource = (AudioSource)_SceneReferences.GetProgramVariable("_sfxSharedUIAudioSource");
		int _currentCrop1 = (int)_SceneReferences.GetProgramVariable("_currentCrop1");
		int _currentCrop2 = (int)_SceneReferences.GetProgramVariable("_currentCrop2");

		_amountCrop = _currentCrop2 + _currentCrop2;

		if (_amountCrop > 0)
		{
			int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");

			int _totalMoney = (int)_SceneReferences.GetProgramVariable("_totalMoney");
			int _valueCrops = (int)_SceneReferences.GetProgramVariable("_valueCrops");

			int _valueCrop1 = (int)_SceneReferences.GetProgramVariable("_valueCrop1") + _valueCrops;
			int _valueCrop2 = (int)_SceneReferences.GetProgramVariable("_valueCrop2") + _valueCrops;

			int _moneyEarned = (_valueCrop1 * _currentCrop1) + (_valueCrop2 * _currentCrop2);

			_SceneReferences.SetProgramVariable("_currentCrop2", 0);
			_SceneReferences.SetProgramVariable("_currentCrop1", 0);

			_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney + _moneyEarned);
			_SceneReferences.SetProgramVariable("_totalMoney", _totalMoney + _moneyEarned);

			_amountCrop = 0;

			_shareduiaudiosource.PlayOneShot(_sfxUISoundSellAll, _sfxUIvolume);
		}
	}
}
