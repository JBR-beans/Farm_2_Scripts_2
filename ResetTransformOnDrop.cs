
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ResetTransformOnDrop : UdonSharpBehaviour
{
    public Transform _transform;

	public override void OnDrop()
	{
		transform.SetPositionAndRotation(_transform.position, _transform.rotation);
	}
}
