
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class UpgradesMenu : UdonSharpBehaviour
{
    public GameObject _plotCrop2;
    public UdonBehaviour _SceneReferences;
    public void UnlockCrop2Plot()
    {
        int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");
        int _costUnlockCrop2Plot = (int)_SceneReferences.GetProgramVariable("_costUnlockCrop2Plot");

        if (_currentMoney >= _costUnlockCrop2Plot)
        {
            _plotCrop2.SetActive(true);
            _SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _costUnlockCrop2Plot);
        }
    }
}
