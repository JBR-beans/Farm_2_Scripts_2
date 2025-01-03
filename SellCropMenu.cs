
using JetBrains.Annotations;
using System;
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Persistence;
using VRC.SDKBase;
using VRC.Udon;

public class SellCropMenu : UdonSharpBehaviour
{
	//public string _cropTag;
	public ParticleSystem _particleFX;
    public UdonBehaviour _SceneReferences;

    private int total;
	public AudioSource _sfxSharedUIAudioSource;
	public AudioClip _sfxBuy1;

	/*	public void IncreaseCropAmount()
		{
			_sfxSharedUIAudioSource = (AudioSource)_SceneReferences.GetProgramVariable("_sfxSharedUIAudioSource");



			switch (_cropTag)
			{
				case "crop1":

					int _currentCrop1 = (int)_SceneReferences.GetProgramVariable("_currentCrop1");
					if (_currentCrop1 > _amountCrop)
					{
						_amountCrop++;
						_sfxSharedUIAudioSource.PlayOneShot(_sfxUISoundIncreaseAmount, _sfxUIvolume);
					}

					break;

				case "crop2":

					int _currentCrop2 = (int)_SceneReferences.GetProgramVariable("_currentCrop2");
					if (_currentCrop2 > _amountCrop)
					{
						_amountCrop++;
						_sfxSharedUIAudioSource.PlayOneShot(_sfxUISoundIncreaseAmount, _sfxUIvolume);
					}

					break;

				case "crop3":

					int _currentCrop3 = (int)_SceneReferences.GetProgramVariable("_currentCrop3");
					if (_currentCrop3 > _amountCrop)
					{
						_amountCrop++;
						_sfxSharedUIAudioSource.PlayOneShot(_sfxUISoundIncreaseAmount, _sfxUIvolume);
					}

					break;

				case "crop4":

					int _currentCrop4 = (int)_SceneReferences.GetProgramVariable("_currentCrop4");
					if (_currentCrop4 > _amountCrop)
					{
						_amountCrop++;
						_sfxSharedUIAudioSource.PlayOneShot(_sfxUISoundIncreaseAmount, _sfxUIvolume);
					}

					break;

				case "crop5":

					int _currentCrop5 = (int)_SceneReferences.GetProgramVariable("_currentCrop5");
					if (_currentCrop5 > _amountCrop)
					{
						_amountCrop++;
						_sfxSharedUIAudioSource.PlayOneShot(_sfxUISoundIncreaseAmount, _sfxUIvolume);
					}

					break;

			}

			_viewCropAmount.text = _amountCrop.ToString();
		}
		public void DecreaseCropAmount()
		{
			_sfxSharedUIAudioSource = (AudioSource)_SceneReferences.GetProgramVariable("_sfxSharedUIAudioSource");
			if (_amountCrop > 0)
			{
				_amountCrop--;
				_sfxSharedUIAudioSource.PlayOneShot(_sfxUISoundDecreaseAmount, _sfxUIvolume);
			}
			_viewCropAmount.text = _amountCrop.ToString();
		}
		public void SellCrop()
		{
			_sfxSharedUIAudioSource = (AudioSource)_SceneReferences.GetProgramVariable("_sfxSharedUIAudioSource");
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

						_sfxSharedUIAudioSource.PlayOneShot(_sfxUISoundSell, _sfxUIvolume);
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

						_sfxSharedUIAudioSource.PlayOneShot(_sfxUISoundSell, _sfxUIvolume);
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

						_sfxSharedUIAudioSource.PlayOneShot(_sfxUISoundSell, _sfxUIvolume);
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

						_sfxSharedUIAudioSource.PlayOneShot(_sfxUISoundSell, _sfxUIvolume);
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

						_sfxSharedUIAudioSource.PlayOneShot(_sfxUISoundSell, _sfxUIvolume);
					}

					break;

			}

			_amountCrop = 0;
			_viewCropAmount.text = "0";
		}

		public void SellMax()
		{
			_sfxSharedUIAudioSource = (AudioSource)_SceneReferences.GetProgramVariable("_sfxSharedUIAudioSource");
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

						_sfxSharedUIAudioSource.PlayOneShot(_sfxUISoundSell, _sfxUIvolume);
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

						_sfxSharedUIAudioSource.PlayOneShot(_sfxUISoundSell, _sfxUIvolume);
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

						_sfxSharedUIAudioSource.PlayOneShot(_sfxUISoundSell, _sfxUIvolume);

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

						_sfxSharedUIAudioSource.PlayOneShot(_sfxUISoundSell, _sfxUIvolume);

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

						_sfxSharedUIAudioSource.PlayOneShot(_sfxUISoundSell);

					}

					break;

			}


			_amountCrop = 0;
			_viewCropAmount.text = "0";
		}*/

	public void SellAll()
	{
		UdonBehaviour[] _crops = (UdonBehaviour[])_SceneReferences.GetProgramVariable("_crops");
		int money = (int)_SceneReferences.GetProgramVariable("_currentMoney");
		int[] current = (int[])_SceneReferences.GetProgramVariable("_currentCrops");
		int[] value = (int[])_SceneReferences.GetProgramVariable("_valueCrops");
		int[] calculated = new int[current.Length];

		for (int i = 0; i < current.Length; i++)
		{
			// check if the crop at that index position is the current quest target
			// if it is, then skip calculating it and dont remove the current crops

			if ((bool)_crops[i].GetProgramVariable("_isQuest") == false)
			{
				calculated[i] = current[i] * value[i];
				current[i] = 0;
			}
			else
			{
				calculated[i] = 0;
			}

			total += calculated[i];

		}

		_SceneReferences.SetProgramVariable("_currentMoney", money + total);

		SellFX();

		_SceneReferences.SetProgramVariable("_currentCrops", current);
		total = 0;

	}
	public void SellFX()
	{
		_sfxSharedUIAudioSource = (AudioSource)_SceneReferences.GetProgramVariable("_sfxSharedUIAudioSource");
		_sfxBuy1 = (AudioClip)_SceneReferences.GetProgramVariable("_sfxBuy1");

		var emission = _particleFX.emission;
		if (total < 100)
		{
			emission.SetBursts(
		new ParticleSystem.Burst[]
		{
					new ParticleSystem.Burst(0.0f, (Single)total)
		});
		}


		if (total > 100)
		{
			Single s = 100;
			emission.SetBursts(
		new ParticleSystem.Burst[]
		{
					new ParticleSystem.Burst(0.0f, s)
		});
		}

		_sfxSharedUIAudioSource.PlayOneShot(_sfxBuy1);
		_particleFX.Play();
	}
	/*	public void SellAll()
		{
			_sfxSharedUIAudioSource = (AudioSource)_SceneReferences.GetProgramVariable("_sfxSharedUIAudioSource");
			_sfxBuy1 = (AudioClip)_SceneReferences.GetProgramVariable("_sfxBuy1");

			int _currentCrop1 = (int)_SceneReferences.GetProgramVariable("_currentCrop1");
			int _currentCrop2 = (int)_SceneReferences.GetProgramVariable("_currentCrop2");
			int _currentCrop3 = (int)_SceneReferences.GetProgramVariable("_currentCrop3");
			int _currentCrop4 = (int)_SceneReferences.GetProgramVariable("_currentCrop4");
			int _currentCrop5 = (int)_SceneReferences.GetProgramVariable("_currentCrop5");
			int _currentCrop6 = (int)_SceneReferences.GetProgramVariable("_currentCrop6");
			int _currentCrop7 = (int)_SceneReferences.GetProgramVariable("_currentCrop7");
			int _currentCrop8 = (int)_SceneReferences.GetProgramVariable("_currentCrop8");
			int _currentCrop9 = (int)_SceneReferences.GetProgramVariable("_currentCrop9");
			int _currentCrop10 = (int)_SceneReferences.GetProgramVariable("_currentCrop10");


			_amountCrop =
				_currentCrop1
				+ _currentCrop2
				+ _currentCrop3
				+ _currentCrop4
				+ _currentCrop5
				+ _currentCrop6
				+ _currentCrop7
				+ _currentCrop8
				+ _currentCrop9
				+ _currentCrop10;

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

				int _valueCrop6 = (int)_SceneReferences.GetProgramVariable("_valueCrop6");
				_valueCrop6 += _valueCrops;
				_valueCrop6 *= _currentCrop6;

				int _valueCrop7 = (int)_SceneReferences.GetProgramVariable("_valueCrop7");
				_valueCrop7 += _valueCrops;
				_valueCrop7 *= _currentCrop7;

				int _valueCrop8 = (int)_SceneReferences.GetProgramVariable("_valueCrop8");
				_valueCrop8 += _valueCrops;
				_valueCrop8 *= _currentCrop8;

				int _valueCrop9 = (int)_SceneReferences.GetProgramVariable("_valueCrop9");
				_valueCrop9 += _valueCrops;
				_valueCrop9 *= _currentCrop9;

				int _valueCrop10 = (int)_SceneReferences.GetProgramVariable("_valueCrop10");
				_valueCrop10 += _valueCrops;
				_valueCrop10 *= _currentCrop10;

				int _totalvalue = 0;

				_totalvalue +=
					_valueCrop1
					+ _valueCrop2
					+ _valueCrop3
					+ _valueCrop4
					+ _valueCrop5
					+ _valueCrop6
					+ _valueCrop7
					+ _valueCrop8
					+ _valueCrop9
					+ _valueCrop10;

				_currentMoney += _totalvalue;

				_totalMoney +=
					_valueCrop1
					+ _valueCrop2
					+ _valueCrop3
					+ _valueCrop4
					+ _valueCrop5
					+ _valueCrop6
					+ _valueCrop7
					+ _valueCrop8
					+ _valueCrop9
					+ _valueCrop10;

				_SceneReferences.SetProgramVariable("_currentCrop1", 0);
				_SceneReferences.SetProgramVariable("_currentCrop2", 0);
				_SceneReferences.SetProgramVariable("_currentCrop3", 0);
				_SceneReferences.SetProgramVariable("_currentCrop4", 0);
				_SceneReferences.SetProgramVariable("_currentCrop5", 0);
				_SceneReferences.SetProgramVariable("_currentCrop6", 0);
				_SceneReferences.SetProgramVariable("_currentCrop7", 0);
				_SceneReferences.SetProgramVariable("_currentCrop8", 0);
				_SceneReferences.SetProgramVariable("_currentCrop9", 0);
				_SceneReferences.SetProgramVariable("_currentCrop10", 0);

				_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney);
				_SceneReferences.SetProgramVariable("_totalMoney", _totalMoney);

				var emission = _particleFX.emission;
				if (_totalvalue < 100)
				{
					emission.SetBursts(
				new ParticleSystem.Burst[]
				{
					new ParticleSystem.Burst(0.0f, (Single)_totalvalue)
				});
				}


				if (_totalvalue > 100)
				{
					Single s = 100;
					emission.SetBursts(
				new ParticleSystem.Burst[]
				{
					new ParticleSystem.Burst(0.0f, s)
				});
				}

				_amountCrop = 0;

				for (int i = 1; i < 11;  i++)
				{
					string _key = "_currentCrop" + i.ToString();
					PlayerData.SetInt(_key, 0);
				}

				UdonBehaviour _persistence = (UdonBehaviour)_SceneReferences.GetProgramVariable("_persistence");

				_persistence.SendCustomEvent("PersistData_Save");

				_sfxSharedUIAudioSource.PlayOneShot(_sfxBuy1);
				_particleFX.Play();
			}*/

}
