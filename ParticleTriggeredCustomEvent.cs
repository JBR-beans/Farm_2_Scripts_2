
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ParticleTriggeredCustomEvent : UdonSharpBehaviour
{
	public string _EventName;
	public int _layer;
	public UdonBehaviour _localbehavior;
	public UdonBehaviour _EventTarget;
	public void OnParticleCollision(GameObject other)
	{
		if (other.gameObject.layer == _layer)
		{
			_localbehavior = other.GetComponent<UdonBehaviour>();
		}

		if (_localbehavior != null)
		{
			_EventTarget = (UdonBehaviour)_localbehavior.GetProgramVariable("_EventTarget");
		}

		Debug.Log(_EventTarget.ToString());


		_EventTarget.SendCustomEvent(_EventName);


	}
}
