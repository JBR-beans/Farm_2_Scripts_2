
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class BGSFXHandler : UdonSharpBehaviour
{
	public UdonBehaviour _SceneReferences;
    public AudioSource _BGSFX;
    public AudioClip[] _bgmClips;
	private AudioClip _currentClip;
	private bool _changeClip = false;
	public void Update()
	{
		if (_BGSFX.isPlaying == false)
		{
			_changeClip = true;

			if (_changeClip == true)
			{

				RandomClip();

				_BGSFX.Play();

			}
		}
	}

	private void RandomClip()
	{
		// prevent previously played clip from being selected more than once after randomizing

		_currentClip = _bgmClips[Random.Range(0, _bgmClips.Length)];

		if (_BGSFX.clip != _currentClip)
		{
			_BGSFX.clip = _currentClip;
			_changeClip = false;
		}
	}
}
