
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Data;
using VRC.SDKBase;
using VRC.Udon;

public class ParticleTriggeredCustomEvent : UdonSharpBehaviour
{
	public bool _isExternal;
	public string _event;
	public int _layer;
	public UdonBehaviour _internalScript;
	[Header("Handled in script")]
	public UdonBehaviour _localBehavior;
	public UdonBehaviour _EventTarget;

	public void OnParticleCollision(GameObject other)
	{
		if (_isExternal == true)
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
		if (_isExternal == false)
		{
			if (other.gameObject.layer == _layer)
			{

				_internalScript.SendCustomEvent(_event);
			}
		}
		
		
		
	}
}
