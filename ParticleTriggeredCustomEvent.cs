
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ParticleTriggeredCustomEvent : UdonSharpBehaviour
{
	public string _event;
	public int _layer;
	public UdonBehaviour _localBehavior;
	public UdonBehaviour _EventTarget;
	public void OnParticleCollision(GameObject other)
	{
		if (other.gameObject.layer == _layer)
		{
			_localBehavior = (UdonBehaviour)other.gameObject.GetComponent<UdonBehaviour>().GetProgramVariable("_script");
			if (_localBehavior != null)
			{
				_localBehavior.SendCustomEvent(_event);
			}
		}
	}
}
