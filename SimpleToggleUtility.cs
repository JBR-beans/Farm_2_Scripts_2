
using Cysharp.Threading.Tasks.Triggers;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SimpleToggleUtility : UdonSharpBehaviour
{

    public GameObject _target;
    public GameObject[] _targets;
    public  void ToggleArray()
    {
        foreach (GameObject target in _targets)
        {
			target.SetActive(!target.activeSelf);
        }
    }
    public void ToggleSingle()
    {
        _target.SetActive(!_target.activeSelf);
    }
}
