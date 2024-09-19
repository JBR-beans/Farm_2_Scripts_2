
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ActivateSupplyCrateButton : UdonSharpBehaviour
{
	public GameObject _supplyCrate;
	public GameObject _PS1;
	public GameObject _PS2;

	public override void Interact()
	{
		_PS1.SetActive(false);
		_PS2.SetActive(false);
		_supplyCrate.SetActive(true);
		this.gameObject.SetActive(false);

	}
}
