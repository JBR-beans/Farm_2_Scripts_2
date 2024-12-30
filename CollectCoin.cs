
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class CollectCoin : UdonSharpBehaviour
{
    public int _collectedMoney;
    public UdonBehaviour _SceneReferences;
	public AudioSource _sfxSource;
	public AudioClip _sfxClip;

	public void OnParticleCollision(GameObject other)
	{
		if (other.layer == 22)
		{
			Collect();
		}
	}
	public void Collect()
    {
		_SceneReferences.SetProgramVariable("_currentMoney", (int)_SceneReferences.GetProgramVariable("_currentMoney") + _collectedMoney);
		_sfxSource.PlayOneShot(_sfxClip);
	}
}
