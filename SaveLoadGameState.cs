
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SaveLoadGameState : UdonSharpBehaviour
{
    public UdonBehaviour _persistence;

    public void Save()
    {
        _persistence.SendCustomEvent("Save_Data");

	}

    public void Load()
    {
		_persistence.SendCustomEvent("Load_Data");
	}
}
