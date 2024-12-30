
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PlayerTracker : UdonSharpBehaviour
{
    public void Update()
    {
        this.transform.SetPositionAndRotation(Networking.LocalPlayer.GetPosition(), Networking.LocalPlayer.GetRotation());
    }
}
