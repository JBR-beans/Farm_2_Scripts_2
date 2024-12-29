
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PickupLocationIndicator : UdonSharpBehaviour
{
    public Transform _indicator;
    public Transform _target;
	public bool _isHeld;
	public void FixedUpdate()
	{
		_indicator.gameObject.SetActive(!_isHeld);
		if (_isHeld == false)
		{
			_indicator.position = _target.position;
		}
	}

	public void OnDrop()
	{
		
		_isHeld = false;
	}

	public void OnPickup()
	{
		_isHeld = true;
	}
}
