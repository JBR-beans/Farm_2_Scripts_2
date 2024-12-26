
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
	//public int[] _currentCrops;

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
		//_cropID = (int)_LocalReferences.GetProgramVariable("_cropID");
	}

	public void Collect()
	{

		int[] _currentCrops = (int[])_SceneReferences.GetProgramVariable("_currentCrops");
		_cropID = (int)_LocalReferences.GetProgramVariable("_cropID");
		int current = _currentCrops[_cropID];
		current++;
		_currentCrops[_cropID] = current;
		_SceneReferences.SetProgramVariable("_currentCrops", _currentCrops);

		/*for (int i = 0; i < _currentCrops.Length; i++)
		{
			if (_currentCrops[i] == _cropID)
			{
				_currentCrops[i] += 1;
			}
		}

		_SceneReferences.SetProgramVariable("_currentCrops", _currentCrops);*/

		CollectionFX();
	}
	
	public void CollectionFX()
	{
		AudioSource _sfxSource = (AudioSource)_LocalReferences.GetProgramVariable("_sfxSource");
		AudioClip _sfxClipCollect = (AudioClip)_LocalReferences.GetProgramVariable("_sfxClipCollect");

		_sfxSource.PlayOneShot(_sfxClipCollect);
	}
}
