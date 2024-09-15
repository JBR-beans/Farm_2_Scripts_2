
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class TriggerCustomEventUtility : UdonSharpBehaviour
{
	public UdonBehaviour _EventTarget;
	public string _EventName;
	[Header("Optional")]
	public bool _useLayerID;
	public int _layerID;
	public void OnTriggerEnter(Collider other)
	{
		if (_useLayerID == true)
		{
			if (other.gameObject.layer == _layerID)
			{
				_EventTarget.SendCustomEvent(_EventName);
			}
		}

		if (_useLayerID == false)
		{
			_EventTarget.SendCustomEvent(_EventName);
		}
	}
}
