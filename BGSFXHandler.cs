
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class BGSFXHandler : UdonSharpBehaviour
{
    public AudioSource _BGSFX;
    public AudioClip[] _bgmClips;
	private bool _changeClip = false;
	public void Update()
	{
		if (!_BGSFX.isPlaying)
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
		_BGSFX.clip = _bgmClips[Random.Range(0, _bgmClips.Length)];
		_changeClip = false;
	}
}
