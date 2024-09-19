
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
		switch (_cropTag)
		{
			case "crop1":

				int _costSeedCrop1 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop1") + _cropCost;
				int _totalCost1 = _costSeedCrop1 * (_amountseeds + 1);


				if (_currentMoney >= _totalCost1)
				{
					_amountseeds++;
					_shareduiaudiosource.PlayOneShot(_sfxUISoundIncreaseAmount, _sfxUIvolume);
				}

				break;

			case "crop2":

				int _costSeedCrop2 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop2") + _cropCost;
				int _totalCost2 = _costSeedCrop2 * (_amountseeds + 1);


				if (_currentMoney >= _totalCost2)
				{
					_amountseeds++;
					_shareduiaudiosource.PlayOneShot(_sfxUISoundIncreaseAmount, _sfxUIvolume);
				}

				break;

			case "crop3":

				int _costSeedCrop3 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop3") + _cropCost;
				int _totalCost3 = _costSeedCrop3 * (_amountseeds + 1);


				if (_currentMoney >= _totalCost3)
				{
					_amountseeds++;
					_shareduiaudiosource.PlayOneShot(_sfxUISoundIncreaseAmount, _sfxUIvolume);
				}

				break;

			case "crop4":

				int _costSeedCrop4 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop4") + _cropCost;
				int _totalCost4 = _costSeedCrop4 * (_amountseeds + 1);


				if (_currentMoney >= _totalCost4)
				{
					_amountseeds++;
					_shareduiaudiosource.PlayOneShot(_sfxUISoundIncreaseAmount, _sfxUIvolume);
				}

				break;

			case "crop5":

				int _costSeedCrop5 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop5") + _cropCost;
				int _totalCost5 = _costSeedCrop5 * (_amountseeds + 1);


				if (_currentMoney >= _totalCost5)
				{
					_amountseeds++;
					_shareduiaudiosource.PlayOneShot(_sfxUISoundIncreaseAmount, _sfxUIvolume);
				}

				break;

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

			switch (_cropTag)
			{
				case "crop1":

					int _costSeedCrop1 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop1") + _cropCost;
					int _totalCost1 = _costSeedCrop1 * _amountseeds;

					if (_currentMoney >= _totalCost1)
					{
						_SceneReferences.SetProgramVariable("_seedCrop1", (int)_SceneReferences.GetProgramVariable("_seedCrop1") + _amountseeds);
						_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _totalCost1);

						_amountseeds = 0;
						_viewSeedAmount.text = "0";

						_shareduiaudiosource.PlayOneShot(_sfxUISoundBuy, _sfxUIvolume);
					}

					break;

				case "crop2":

					int _costSeedCrop2 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop2") + _cropCost;
					int _totalCost2 = _costSeedCrop2 * _amountseeds;

					if (_currentMoney >= _totalCost2)
					{
						_SceneReferences.SetProgramVariable("_seedCrop2", (int)_SceneReferences.GetProgramVariable("_seedCrop2") + _amountseeds);
						_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _totalCost2);

						_amountseeds = 0;
						_viewSeedAmount.text = "0";

						_shareduiaudiosource.PlayOneShot(_sfxUISoundBuy, _sfxUIvolume);
					}

					break;

				case "crop3":

					int _costSeedCrop3 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop3") + _cropCost;
					int _totalCost3 = _costSeedCrop3 * _amountseeds;

					if (_currentMoney >= _totalCost3)
					{
						_SceneReferences.SetProgramVariable("_seedCrop3", (int)_SceneReferences.GetProgramVariable("_seedCrop3") + _amountseeds);
						_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _totalCost3);

						_amountseeds = 0;
						_viewSeedAmount.text = "0";

						_shareduiaudiosource.PlayOneShot(_sfxUISoundBuy, _sfxUIvolume);
					}

					break;

				case "crop4":

					int _costSeedCrop4 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop4") + _cropCost;
					int _totalCost4 = _costSeedCrop4 * _amountseeds;

					if (_currentMoney >= _totalCost4)
					{
						_SceneReferences.SetProgramVariable("_seedCrop4", (int)_SceneReferences.GetProgramVariable("_seedCrop4") + _amountseeds);
						_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _totalCost4);

						_amountseeds = 0;
						_viewSeedAmount.text = "0";

						_shareduiaudiosource.PlayOneShot(_sfxUISoundBuy, _sfxUIvolume);
					}

					break;

				case "crop5":

					int _costSeedCrop5 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop5") + _cropCost;
					int _totalCost5 = _costSeedCrop5 * _amountseeds;

					if (_currentMoney >= _totalCost5)
					{
						_SceneReferences.SetProgramVariable("_seedCrop5", (int)_SceneReferences.GetProgramVariable("_seedCrop5") + _amountseeds);
						_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _totalCost5);

						_amountseeds = 0;
						_viewSeedAmount.text = "0";

						_shareduiaudiosource.PlayOneShot(_sfxUISoundBuy, _sfxUIvolume);
					}

					break;

			}
		}
	}

	public void BuyOne()
	{
		_shareduiaudiosource = (AudioSource)_SceneReferences.GetProgramVariable("_sfxSharedUIAudioSource");

		int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");
		int _cropCost = (int)_SceneReferences.GetProgramVariable("_cropCost");

		switch (_cropTag)
		{
			case "crop1":

				int _costSeedCrop1 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop1") + _cropCost;

				int _totalSeeds1 = 1;

				int _totalCost1 = _totalSeeds1 * _costSeedCrop1;


				if (_totalCost1 <= _currentMoney)
				{
					_SceneReferences.SetProgramVariable("_seedCrop1", (int)_SceneReferences.GetProgramVariable("_seedCrop1") + _totalSeeds1);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _totalCost1);
				}

				break;

			case "crop2":

				int _costSeedCrop2 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop2") + _cropCost;

				int _totalSeeds2 = 1;

				int _totalCost2 = _totalSeeds2 * _costSeedCrop2;


				if (_totalCost2 <= _currentMoney)
				{
					_SceneReferences.SetProgramVariable("_seedCrop2", (int)_SceneReferences.GetProgramVariable("_seedCrop2") + _totalSeeds2);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _totalCost2);
				}

				break;

			case "crop3":

				int _costSeedCrop3 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop3") + _cropCost;

				int _totalSeeds3 = 1;

				int _totalCost3 = _totalSeeds3 * _costSeedCrop3;


				if (_totalCost3 <= _currentMoney)
				{
					_SceneReferences.SetProgramVariable("_seedCrop3", (int)_SceneReferences.GetProgramVariable("_seedCrop3") + _totalSeeds3);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _totalCost3);
				}

				break;

			case "crop4":

				int _costSeedCrop4 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop4") + _cropCost;

				int _totalSeeds4 = 1;

				int _totalCost4 = _totalSeeds4 * _costSeedCrop4;


				if (_totalCost4 <= _currentMoney)
				{
					_SceneReferences.SetProgramVariable("_seedCrop4", (int)_SceneReferences.GetProgramVariable("_seedCrop4") + _totalSeeds4);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _totalCost4);
				}

				break;

			case "crop5":

				int _costSeedCrop5 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop5") + _cropCost;

				int _totalSeeds5 = 1;

				int _totalCost5 = _totalSeeds5 * _costSeedCrop5;


				if (_totalCost5 <= _currentMoney)
				{
					_SceneReferences.SetProgramVariable("_seedCrop5", (int)_SceneReferences.GetProgramVariable("_seedCrop5") + _totalSeeds5);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _totalCost5);
				}

				break;

		}
	}

	public void BuyFive()
	{
		_shareduiaudiosource = (AudioSource)_SceneReferences.GetProgramVariable("_sfxSharedUIAudioSource");

		int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");
		int _cropCost = (int)_SceneReferences.GetProgramVariable("_cropCost");

		switch (_cropTag)
		{
			case "crop1":

				int _costSeedCrop1 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop1") + _cropCost;

				int _totalSeeds1 = 5;

				int _totalCost1 = _totalSeeds1 * _costSeedCrop1;


				if (_totalCost1 <= _currentMoney)
				{
					_SceneReferences.SetProgramVariable("_seedCrop1", (int)_SceneReferences.GetProgramVariable("_seedCrop1") + _totalSeeds1);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _totalCost1);
				}

				break;

			case "crop2":

				int _costSeedCrop2 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop2") + _cropCost;

				int _totalSeeds2 = 5;

				int _totalCost2 = _totalSeeds2 * _costSeedCrop2;


				if (_totalCost2 <= _currentMoney)
				{
					_SceneReferences.SetProgramVariable("_seedCrop2", (int)_SceneReferences.GetProgramVariable("_seedCrop2") + _totalSeeds2);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _totalCost2);
				}

				break;

			case "crop3":

				int _costSeedCrop3 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop3") + _cropCost;

				int _totalSeeds3 = 5;

				int _totalCost3 = _totalSeeds3 * _costSeedCrop3;


				if (_totalCost3 <= _currentMoney)
				{
					_SceneReferences.SetProgramVariable("_seedCrop3", (int)_SceneReferences.GetProgramVariable("_seedCrop3") + _totalSeeds3);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _totalCost3);
				}

				break;

			case "crop4":

				int _costSeedCrop4 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop4") + _cropCost;

				int _totalSeeds4 = 5;

				int _totalCost4 = _totalSeeds4 * _costSeedCrop4;


				if (_totalCost4 <= _currentMoney)
				{
					_SceneReferences.SetProgramVariable("_seedCrop4", (int)_SceneReferences.GetProgramVariable("_seedCrop4") + _totalSeeds4);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _totalCost4);
				}

				break;

			case "crop5":

				int _costSeedCrop5 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop5") + _cropCost;

				int _totalSeeds5 = 5;

				int _totalCost5 = _totalSeeds5 * _costSeedCrop5;


				if (_totalCost5 <= _currentMoney)
				{
					_SceneReferences.SetProgramVariable("_seedCrop5", (int)_SceneReferences.GetProgramVariable("_seedCrop5") + _totalSeeds5);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _totalCost5);
				}

				break;

		}
	}


	public void BuyMax()
	{
		int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");
		int _cropCost = (int)_SceneReferences.GetProgramVariable("_cropCost");

		switch (_cropTag)
		{
			case "crop1":

				int _costSeedCrop1 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop1") + _cropCost;

				int _totalSeeds1 = (int)Mathf.Floor(_currentMoney / _costSeedCrop1);

				int _totalCost1 = _totalSeeds1 * _costSeedCrop1;


				if (_totalCost1 <= _currentMoney)
				{
					_SceneReferences.SetProgramVariable("_seedCrop1", (int)_SceneReferences.GetProgramVariable("_seedCrop1") + _totalSeeds1);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _totalCost1);
				}
		
				break;

			case "crop2":

				int _costSeedCrop2 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop2") + _cropCost;

				int _totalSeeds2 = (int)Mathf.Floor(_currentMoney / _costSeedCrop2);

				int _totalCost2 = _totalSeeds2 * _costSeedCrop2;


				if (_totalCost2 <= _currentMoney)
				{
					_SceneReferences.SetProgramVariable("_seedCrop2", (int)_SceneReferences.GetProgramVariable("_seedCrop2") + _totalSeeds2);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _totalCost2);
				}

				break;

			case "crop3":

				int _costSeedCrop3 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop3") + _cropCost;

				int _totalSeeds3 = (int)Mathf.Floor(_currentMoney / _costSeedCrop3);

				int _totalCost3 = _totalSeeds3 * _costSeedCrop3;


				if (_totalCost3 <= _currentMoney)
				{
					_SceneReferences.SetProgramVariable("_seedCrop3", (int)_SceneReferences.GetProgramVariable("_seedCrop3") + _totalSeeds3);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _totalCost3);
				}

				break;

			case "crop4":

				int _costSeedCrop4 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop4") + _cropCost;

				int _totalSeeds4 = (int)Mathf.Floor(_currentMoney / _costSeedCrop4);

				int _totalCost4 = _totalSeeds4 * _costSeedCrop4;


				if (_totalCost4 <= _currentMoney)
				{
					_SceneReferences.SetProgramVariable("_seedCrop4", (int)_SceneReferences.GetProgramVariable("_seedCrop4") + _totalSeeds4);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _totalCost4);
				}

				break;

			case "crop5":

				int _costSeedCrop5 = (int)_SceneReferences.GetProgramVariable("_costSeedCrop5") + _cropCost;

				int _totalSeeds5 = (int)Mathf.Floor(_currentMoney / _costSeedCrop5);

				int _totalCost5 = _totalSeeds5 * _costSeedCrop5;


				if (_totalCost5 <= _currentMoney)
				{
					_SceneReferences.SetProgramVariable("_seedCrop5", (int)_SceneReferences.GetProgramVariable("_seedCrop5") + _totalSeeds5);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _totalCost5);
				}

				break;

		}
	}
}
