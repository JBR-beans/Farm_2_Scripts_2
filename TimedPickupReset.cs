
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class TimedPickupReset : UdonSharpBehaviour
{
    public float _maxTime;

    private float _ticker;

    private Transform _transform;


    public void Start()
    {
        _transform.position = transform.position;

        _transform.rotation = transform.rotation;
    }

	public override void OnDrop()
	{
        _ticker += Time.fixedDeltaTime;

        if (_ticker > _maxTime)
        {
            transform.SetPositionAndRotation(_transform.position, _transform.rotation);

            _ticker = 0;

		}
	}
}
