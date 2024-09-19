
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class HoverAndRotate : UdonSharpBehaviour
{
	public Vector3 axis;
	public float angle;

	public void Update()
	{
		this.transform.Rotate(axis, angle);

		Transform t = transform;
	}
}
