
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class CustomEventArrayUtility : UdonSharpBehaviour
{
    public UdonBehaviour[] _Behaviors;
    public string _EventName;
    public string _EventName2;
    public void SendEventsToBehaviors()
    {
        foreach (UdonBehaviour behaviour in _Behaviors)
        {
            behaviour.SendCustomEvent(_EventName);
        }
    }

    public void SendEventsToBehaviors2()
    {
		foreach (UdonBehaviour behaviour in _Behaviors)
		{
			behaviour.SendCustomEvent(_EventName2);
		}
	}
}
