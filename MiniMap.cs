
using Newtonsoft.Json.Linq;
using UdonSharp;
using Unity.Mathematics;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class MiniMap : UdonSharpBehaviour
{

	public Transform _minimapCamera;
	public GameObject _minimapDisplay;
	public Transform _minimapPlayer;
	public bool _isOn;

	public void Update()
	{
		Vector3 t = Networking.LocalPlayer.GetPosition();
		Vector3 tt = new Vector3(t.x, 0, t.z);

		Quaternion q = Networking.LocalPlayer.GetRotation();
		float YtoZ = q.eulerAngles.y;

		_minimapPlayer.rotation = Quaternion.Euler(90, 0, -YtoZ);

		_minimapCamera.position = tt;

		_minimapCamera.gameObject.SetActive(_isOn);
		_minimapDisplay.SetActive(_isOn);
	}

	public void OnPickupUseDown()
	{
		_isOn = !_isOn;
	}

}
