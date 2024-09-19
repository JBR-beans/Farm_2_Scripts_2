
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

	

		switch (_cropTag)
		{
			case "crop1":

				int _currentCrop1 = (int)_SceneReferences.GetProgramVariable("_currentCrop1");
				if (_currentCrop1 > _amountCrop)
				{
					_amountCrop++;
					_shareduiaudiosource.PlayOneShot(_sfxUISoundIncreaseAmount, _sfxUIvolume);
				}

				break;

			case "crop2":

				int _currentCrop2 = (int)_SceneReferences.GetProgramVariable("_currentCrop2");
				if (_currentCrop2 > _amountCrop)
				{
					_amountCrop++;
					_shareduiaudiosource.PlayOneShot(_sfxUISoundIncreaseAmount, _sfxUIvolume);
				}

				break;

			case "crop3":

				int _currentCrop3 = (int)_SceneReferences.GetProgramVariable("_currentCrop3");
				if (_currentCrop3 > _amountCrop)
				{
					_amountCrop++;
					_shareduiaudiosource.PlayOneShot(_sfxUISoundIncreaseAmount, _sfxUIvolume);
				}

				break;

			case "crop4":

				int _currentCrop4 = (int)_SceneReferences.GetProgramVariable("_currentCrop4");
				if (_currentCrop4 > _amountCrop)
				{
					_amountCrop++;
					_shareduiaudiosource.PlayOneShot(_sfxUISoundIncreaseAmount, _sfxUIvolume);
				}

				break;

			case "crop5":

				int _currentCrop5 = (int)_SceneReferences.GetProgramVariable("_currentCrop5");
				if (_currentCrop5 > _amountCrop)
				{
					_amountCrop++;
					_shareduiaudiosource.PlayOneShot(_sfxUISoundIncreaseAmount, _sfxUIvolume);
				}

				break;

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

		switch (_cropTag)
		{
			case "crop1":

				int _currentCrop1 = (int)_SceneReferences.GetProgramVariable("_currentCrop1");

				if (_currentCrop1 > 0)
				{
					int _valueCrop1 = (int)_SceneReferences.GetProgramVariable("_valueCrop1");
					_valueCrop1 += _valueCrops;

					int _moneyEarned = _valueCrop1 * _amountCrop;
					_currentMoney += _moneyEarned;
					_totalMoney += _moneyEarned;

					_currentCrop1 -= _amountCrop;

					_SceneReferences.SetProgramVariable("_currentCrop1", _currentCrop1);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney);
					_SceneReferences.SetProgramVariable("_totalMoney", _totalMoney);

					_shareduiaudiosource.PlayOneShot(_sfxUISoundSell, _sfxUIvolume);
				}

				break;

			case "crop2":

				int _currentCrop2 = (int)_SceneReferences.GetProgramVariable("_currentCrop2");

				if (_currentCrop2 > 0)
				{
					int _valueCrop2 = (int)_SceneReferences.GetProgramVariable("_valueCrop2");
					_valueCrop2 += _valueCrops;

					int _moneyEarned = _valueCrop2 * _amountCrop;
					_currentMoney += _moneyEarned;
					_totalMoney += _moneyEarned;

					_currentCrop2 -= _amountCrop;

					_SceneReferences.SetProgramVariable("_currentCrop2", _currentCrop2);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney);
					_SceneReferences.SetProgramVariable("_totalMoney", _totalMoney);

					_shareduiaudiosource.PlayOneShot(_sfxUISoundSell, _sfxUIvolume);
				}

				break;

			case "crop3":

				int _currentCrop3 = (int)_SceneReferences.GetProgramVariable("_currentCrop3");

				if (_currentCrop3 > 0)
				{
					int _valueCrop3 = (int)_SceneReferences.GetProgramVariable("_valueCrop3");
					_valueCrop3 += _valueCrops;

					int _moneyEarned = _valueCrop3 * _amountCrop;
					_currentMoney += _moneyEarned;
					_totalMoney += _moneyEarned;

					_currentCrop3 -= _amountCrop;

					_SceneReferences.SetProgramVariable("_currentCrop3", _currentCrop3);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney);
					_SceneReferences.SetProgramVariable("_totalMoney", _totalMoney);

					_shareduiaudiosource.PlayOneShot(_sfxUISoundSell, _sfxUIvolume);
				}

				break;

			case "crop4":

				int _currentCrop4 = (int)_SceneReferences.GetProgramVariable("_currentCrop4");

				if (_currentCrop4 > 0)
				{
					int _valueCrop4 = (int)_SceneReferences.GetProgramVariable("_valueCrop4");
					_valueCrop4 += _valueCrops;

					int _moneyEarned = _valueCrop4 * _amountCrop;
					_currentMoney += _moneyEarned;
					_totalMoney += _moneyEarned;

					_currentCrop4 -= _amountCrop;

					_SceneReferences.SetProgramVariable("_currentCrop4", _currentCrop4);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney);
					_SceneReferences.SetProgramVariable("_totalMoney", _totalMoney);

					_shareduiaudiosource.PlayOneShot(_sfxUISoundSell, _sfxUIvolume);
				}

				break;

			case "crop5":

				int _currentCrop5 = (int)_SceneReferences.GetProgramVariable("_currentCrop5");

				if (_currentCrop5 > 0)
				{
					int _valueCrop5 = (int)_SceneReferences.GetProgramVariable("_valueCrop5");
					_valueCrop5 += _valueCrops;

					int _moneyEarned = _valueCrop5 * _amountCrop;
					_currentMoney += _moneyEarned;
					_totalMoney += _moneyEarned;

					_currentCrop5 -= _amountCrop;

					_SceneReferences.SetProgramVariable("_currentCrop5", _currentCrop5);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney);
					_SceneReferences.SetProgramVariable("_totalMoney", _totalMoney);

					_shareduiaudiosource.PlayOneShot(_sfxUISoundSell, _sfxUIvolume);
				}

				break;

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

		switch (_cropTag)
		{
			case "crop1":

				int _currentCrop1 = (int)_SceneReferences.GetProgramVariable("_currentCrop1");
				_amountCrop = _currentCrop1;

				if (_currentCrop1 > 0)
				{
					int _valueCrop1 = (int)_SceneReferences.GetProgramVariable("_valueCrop1");
					_valueCrop1 += _valueCrops;

					int _moneyEarned = _valueCrop1 * _amountCrop;
					_currentMoney += _moneyEarned;
					_totalMoney += _moneyEarned;

					_SceneReferences.SetProgramVariable("_currentCrop1", 0);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney);
					_SceneReferences.SetProgramVariable("_totalMoney", _totalMoney);

					_shareduiaudiosource.PlayOneShot(_sfxUISoundSell, _sfxUIvolume);
				}

				break;

			case "crop2":

				int _currentCrop2 = (int)_SceneReferences.GetProgramVariable("_currentCrop2");
				_amountCrop = _currentCrop2;

				if (_currentCrop2 > 0)
				{
					int _valueCrop2 = (int)_SceneReferences.GetProgramVariable("_valueCrop2");
					_valueCrop2 += _valueCrops;

					int _moneyEarned = _valueCrop2 * _amountCrop;
					_currentMoney += _moneyEarned;
					_totalMoney += _moneyEarned;

					_SceneReferences.SetProgramVariable("_currentCrop2", 0);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney);
					_SceneReferences.SetProgramVariable("_totalMoney", _totalMoney);

					_shareduiaudiosource.PlayOneShot(_sfxUISoundSell, _sfxUIvolume);
				}

				break;

			case "crop3":

				int _currentCrop3 = (int)_SceneReferences.GetProgramVariable("_currentCrop3");
				_amountCrop = _currentCrop3;

				if (_currentCrop3 > 0)
				{
					int _valueCrop3 = (int)_SceneReferences.GetProgramVariable("_valueCrop3");
					_valueCrop3 += _valueCrops;

					int _moneyEarned = _valueCrop3 * _amountCrop;
					_currentMoney += _moneyEarned;
					_totalMoney += _moneyEarned;

					_SceneReferences.SetProgramVariable("_currentCrop3", 0);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney);
					_SceneReferences.SetProgramVariable("_totalMoney", _totalMoney);

					_shareduiaudiosource.PlayOneShot(_sfxUISoundSell, _sfxUIvolume);
				
				}

				break;

			case "crop4":

				int _currentCrop4 = (int)_SceneReferences.GetProgramVariable("_currentCrop4");
				_amountCrop = _currentCrop4;

				if (_currentCrop4 > 0)
				{
					int _valueCrop4 = (int)_SceneReferences.GetProgramVariable("_valueCrop4");
					_valueCrop4 += _valueCrops;

					int _moneyEarned = _valueCrop4 * _amountCrop;
					_currentMoney += _moneyEarned;
					_totalMoney += _moneyEarned;

					_SceneReferences.SetProgramVariable("_currentCrop4", 0);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney);
					_SceneReferences.SetProgramVariable("_totalMoney", _totalMoney);

					_shareduiaudiosource.PlayOneShot(_sfxUISoundSell, _sfxUIvolume);
				
				}

				break;

			case "crop5":

				int _currentCrop5 = (int)_SceneReferences.GetProgramVariable("_currentCrop5");
				_amountCrop = _currentCrop5;

				if (_currentCrop5 > 0)
				{
					int _valueCrop5 = (int)_SceneReferences.GetProgramVariable("_valueCrop5");
					_valueCrop5 += _valueCrops;

					int _moneyEarned = _valueCrop5 * _amountCrop;
					_currentMoney += _moneyEarned;
					_totalMoney += _moneyEarned;

					_SceneReferences.SetProgramVariable("_currentCrop5", 0);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney);
					_SceneReferences.SetProgramVariable("_totalMoney", _totalMoney);

					_shareduiaudiosource.PlayOneShot(_sfxUISoundSell, _sfxUIvolume);
					
				}

				break;

		}

		
		_amountCrop = 0;
		_viewCropAmount.text = "0";
	}
	public void SellAll()
	{
		_shareduiaudiosource = (AudioSource)_SceneReferences.GetProgramVariable("_sfxSharedUIAudioSource");

		int _currentCrop1 = (int)_SceneReferences.GetProgramVariable("_currentCrop1");
		int _currentCrop2 = (int)_SceneReferences.GetProgramVariable("_currentCrop2");
		int _currentCrop3 = (int)_SceneReferences.GetProgramVariable("_currentCrop3");
		int _currentCrop4 = (int)_SceneReferences.GetProgramVariable("_currentCrop4");
		int _currentCrop5 = (int)_SceneReferences.GetProgramVariable("_currentCrop5");

		_amountCrop =
			_currentCrop1
			+ _currentCrop2
			+ _currentCrop3
			+ _currentCrop4
			+ _currentCrop5;

		if (_amountCrop > 0)
		{
			int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");
			int _totalMoney = (int)_SceneReferences.GetProgramVariable("_totalMoney");

			int _valueCrops = (int)_SceneReferences.GetProgramVariable("_valueCrops");

			int _valueCrop1 = (int)_SceneReferences.GetProgramVariable("_valueCrop1");
			_valueCrop1 += _valueCrops;
			_valueCrop1 *= _currentCrop1;

			int _valueCrop2 = (int)_SceneReferences.GetProgramVariable("_valueCrop2");
			_valueCrop2 += _valueCrops;
			_valueCrop2 *= _currentCrop2;

			int _valueCrop3 = (int)_SceneReferences.GetProgramVariable("_valueCrop3");
			_valueCrop3 += _valueCrops;
			_valueCrop3 *= _currentCrop3;

			int _valueCrop4 = (int)_SceneReferences.GetProgramVariable("_valueCrop4");
			_valueCrop4 += _valueCrops;
			_valueCrop4 *= _currentCrop4;

			int _valueCrop5 = (int)_SceneReferences.GetProgramVariable("_valueCrop5");
			_valueCrop5 += _valueCrops;
			_valueCrop5 *= _currentCrop5;

			_currentMoney +=
				_valueCrop1
				+ _valueCrop2
				+ _valueCrop3
				+ _valueCrop4
				+ _valueCrop5;

			_totalMoney +=
				_valueCrop1
				+ _valueCrop2
				+ _valueCrop3
				+ _valueCrop4
				+ _valueCrop5;

			_SceneReferences.SetProgramVariable("_currentCrop1", 0);
			_SceneReferences.SetProgramVariable("_currentCrop2", 0);
			_SceneReferences.SetProgramVariable("_currentCrop3", 0);
			_SceneReferences.SetProgramVariable("_currentCrop4", 0);
			_SceneReferences.SetProgramVariable("_currentCrop5", 0);

			_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney);
			_SceneReferences.SetProgramVariable("_totalMoney", _totalMoney);

			_amountCrop = 0;

			_shareduiaudiosource.PlayOneShot(_sfxUISoundSellAll, _sfxUIvolume);

			
		}
	}
}
