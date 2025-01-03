
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class BGSFXHandler : UdonSharpBehaviour
{
	public UdonBehaviour _SceneReferences;
    public AudioSource _BGSFX;
    public AudioClip[] _bgmClips;
	private int _index = 0;

	public void Start()
	{
		_bgmClips = (AudioClip[])_SceneReferences.GetProgramVariable("_bgmClipLibrary");
	}

	public void Update()
	{
		if (_BGSFX.isPlaying == false)
		{

			_index++;
			if (_index > _bgmClips.Length)
			{
				_index = 0;
			}

			_BGSFX.clip = _bgmClips[_index];


			_BGSFX.Play();
		}
	}

	private void RandomClip()
	{
		// prevent previously played clip from being selected more than once after randomizing

		

	}
}
