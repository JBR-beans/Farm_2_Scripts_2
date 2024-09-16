
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SupplyDropHitGround : UdonSharpBehaviour
{
	public GameObject _Item;
	public GameObject _PS1;
	public GameObject _PS2;
	public void OnCollisionEnter(Collision collision)
	{
		_PS1.transform.transform.position = transform.position;
		_PS2.transform.transform.position = transform.position;
		_Item.SetActive(true);
		_PS1.SetActive(true);
		_PS2.SetActive(true);
		this.gameObject.SetActive(false);
	}
}
