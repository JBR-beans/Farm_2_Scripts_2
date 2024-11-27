
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class OnTriggerEnterCustomEvent : UdonSharpBehaviour
{
	public int _layer;
	public UdonBehaviour _localBehavior;
	public string _event;
	public void OnTriggerEnter(Collider other)
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
