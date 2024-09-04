
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class GameMenu : UdonSharpBehaviour
{
	public string _cropTag;

    public UdonBehaviour _SceneReferences;

    public int _amountCrop;

	public TextMeshProUGUI _viewCropAmount;
    public void IncreaseCropAmount()
    {
		if (_cropTag == (string)_SceneReferences.GetProgramVariable("_tagCrop1"))
		{
			int _currentCrop = (int)_SceneReferences.GetProgramVariable("_currentCrop1");
			if (_currentCrop > _amountCrop)
			{
				_amountCrop++;
			}
		}
		if (_cropTag == (string)_SceneReferences.GetProgramVariable("_tagCrop2"))
		{
			int _currentCrop = (int)_SceneReferences.GetProgramVariable("_currentCrop2");
			if (_currentCrop > _amountCrop)
			{
				_amountCrop++;
			}
		}
		_viewCropAmount.text = _amountCrop.ToString();
	}
	public void DecreaseCropAmount()
	{
		if (_amountCrop > 0)
		{
			_amountCrop--;
		}
		_viewCropAmount.text = _amountCrop.ToString();
	}
	public void SellCrop()
	{
		int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");
		int _totalMoney = (int)_SceneReferences.GetProgramVariable("_totalMoney");

		if (_cropTag == "crop1")
		{
			int _currentCrop1 = (int)_SceneReferences.GetProgramVariable("_currentCrop1");
			int _valueCrops = (int)_SceneReferences.GetProgramVariable("_valueCrops");
			int _valueCrop1 = (int)_SceneReferences.GetProgramVariable("_valueCrop1") + _valueCrops;
			int _moneyEarned = _valueCrop1 * _amountCrop;

			_SceneReferences.SetProgramVariable("_currentCrop1", _currentCrop1 - _amountCrop);
			_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney + _moneyEarned);
			_SceneReferences.SetProgramVariable("_totalMoney", _totalMoney + _moneyEarned);
		}

		if (_cropTag == "crop2")
		{
			int _currentCrop2 = (int)_SceneReferences.GetProgramVariable("_currentCrop2");
			int _valueCrops = (int)_SceneReferences.GetProgramVariable("_valueCrops");
			int _valueCrop2 = (int)_SceneReferences.GetProgramVariable("_valueCrop2") + _valueCrops;
			int _moneyEarned = _valueCrop2 * _amountCrop;

			_SceneReferences.SetProgramVariable("_currentCrop2", _currentCrop2 - _amountCrop);
			_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney + _moneyEarned);
			_SceneReferences.SetProgramVariable("_totalMoney", _totalMoney + _moneyEarned);
		}

		_amountCrop = 0;
		_viewCropAmount.text = "0";
	}
	public void UpgradeSellPrice()
	{

	}
}
