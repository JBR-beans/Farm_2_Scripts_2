
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class BuySupplyCrateItem : UdonSharpBehaviour
{
    public GameObject _supplyCrate;
    public GameObject _PS1;
    public GameObject _PS2;
    public void ActivateSupplyCrate()
    {
        _PS1.SetActive(false);
        _PS2.SetActive(false);
        _supplyCrate.SetActive(true);
    }
}
