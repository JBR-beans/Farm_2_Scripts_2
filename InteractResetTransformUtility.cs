
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class InteractResetTransformUtility : UdonSharpBehaviour
{
	public Transform _resetTarget;
	public bool _useArray;
	public Transform[] _resetTargets;
	public Transform[] _resetInputs;
	public override void Interact()
	{
		if (_useArray == false)
		{
			this.transform.position = _resetTarget.position;
			this.transform.rotation = _resetTarget.rotation;
		}
	
		if (_useArray == true)
		{
			for(int i = 0;  i < _resetTargets.Length; i++)
			{
				_resetInputs[i].position = _resetTargets[i].position;
				_resetInputs[i].rotation = _resetTargets[i].rotation;
			}
		}
	
	}
}
