
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class CollectCandy : UdonSharpBehaviour
{
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
		_SceneReferences.SetProgramVariable("_currentCandy", (int)_SceneReferences.GetProgramVariable("_currentCandy") + 1);
		_sfxSource.PlayOneShot(_sfxClip);
	}
}
