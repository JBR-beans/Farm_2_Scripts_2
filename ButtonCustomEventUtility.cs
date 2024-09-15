
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ButtonCustomEventUtility : UdonSharpBehaviour
{
	public UdonBehaviour _BehaviorReference;
	public string _EventName;
	public override void Interact()
	{
		_BehaviorReference.SendCustomEvent(_EventName);
	}
}
