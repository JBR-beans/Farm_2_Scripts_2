
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class CollectCoin : UdonSharpBehaviour
{
    public int _collectedMoney;
    public UdonBehaviour _SceneReferences;
    public void Collect()
    {

		_SceneReferences.SetProgramVariable("_currentMoney", (int)_SceneReferences.GetProgramVariable("_currentMoney") + _collectedMoney);

	}
}
