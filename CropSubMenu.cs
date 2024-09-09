
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class CropSubMenu : UdonSharpBehaviour
{
    public GameObject _buttonSubMenuOn;
    public GameObject _buttonSubMenuOff;

    public GameObject _subMenu;
    public UdonBehaviour _crop;

    public void CloseSubMenu()
    {
        _buttonSubMenuOn.SetActive(true);
        _buttonSubMenuOff.SetActive(false);
		_subMenu.SetActive(false);

	}
    public void OpenSubMenu()
    {
		_buttonSubMenuOn.SetActive(false);
		_buttonSubMenuOff.SetActive(true);
		_subMenu.SetActive(true);
	}

    public void UpgradeAutoWater()
    {
        _crop.SendCustomEvent("UpgradeAutoWater");
    }
    public void UpgradeAutoHarvest()
    {
		_crop.SendCustomEvent("UpgradeAutoHarvest");
	}
    public void UpgradeAutoPlant()
    {
        _crop.SendCustomEvent("UpgradeAutoPlant");
    }
    public void UpgradeYield()
    {
		_crop.SendCustomEvent("UpgradeYield");
	}
}
