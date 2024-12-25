
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CollectCrop : UdonSharpBehaviour
{
	public UdonBehaviour _SceneReferences;
	public UdonBehaviour _LocalReferences;
	public int _cropID;
	public int[] _currentCrops;

	public void Start()
	{
		LinkReferences();
	}
	public void OnParticleCollision(GameObject other)
	{
		if (other.gameObject.layer == 22)
		{
			Collect();
		}
	}

	public void LinkReferences()
	{
		_SceneReferences = (UdonBehaviour)_LocalReferences.GetProgramVariable("_SceneReferences");
	}

	public void Collect()
	{

		_currentCrops = (int[])_SceneReferences.GetProgramVariable("_currentCrops");
		int c = _currentCrops[_cropID];
		_currentCrops[_cropID] = c + 1;

		_SceneReferences.SetProgramVariable("_currentCrops", _currentCrops);

		CollectionFX();
	}
	
	public void CollectionFX()
	{
		AudioSource _sfxSource = (AudioSource)_LocalReferences.GetProgramVariable("_sfxSource");
		AudioClip _sfxClipCollect = (AudioClip)_LocalReferences.GetProgramVariable("_sfxClipCollect");

		_sfxSource.PlayOneShot(_sfxClipCollect);
	}
}
