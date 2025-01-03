﻿
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Wrapper.Modules;

public class CollectCrop : UdonSharpBehaviour
{
	public UdonBehaviour _SceneReferences;
	public UdonBehaviour _LocalReferences;
	public UdonBehaviour _QuestsHandler;
	public GameObject _collider1;
	public GameObject _collider2;
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
			_collider1.SetActive(!_collider1.activeSelf);
			_collider2.SetActive(!_collider2.activeSelf);
		}
	}

	public void LinkReferences()
	{
		_SceneReferences = (UdonBehaviour)_LocalReferences.GetProgramVariable("_SceneReferences");
		_QuestsHandler = (UdonBehaviour)_SceneReferences.GetProgramVariable("_QuestsHandler");
		_collider1 = (GameObject)_SceneReferences.GetProgramVariable("_collider1");
		_collider2 = (GameObject)_SceneReferences.GetProgramVariable("_collider2");
		
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
		if ((bool)_LocalReferences.GetProgramVariable("_isQuest") == true)
		{
			_QuestsHandler.SetProgramVariable("_currentQuestItemCount", (int)_QuestsHandler.GetProgramVariable("_currentQuestItemCount") + 1);
		}

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
