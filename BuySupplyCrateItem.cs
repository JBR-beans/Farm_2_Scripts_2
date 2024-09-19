
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class BuySupplyCrateItem : UdonSharpBehaviour
{
    public GameObject _supplyCrate;
    public int _Cost;
    public AudioClip _sfxClip;
    public GameObject _PS1;
    public GameObject _PS2;
    public UdonBehaviour _SceneReferences;
    public TextMeshProUGUI _title;

    [Header("INTERNAL")]
    public AudioSource _sfxSharedUIAudioSource;

	public void ActivateSupplyCrate()
    {
        int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");

        if (_currentMoney >= _Cost)
        {
			_PS1.SetActive(false);
			_PS2.SetActive(false);
			_supplyCrate.SetActive(true);
			this.GetComponent<Button>().interactable = false;
			_title.text = "Sold!";

            _sfxSharedUIAudioSource = (AudioSource)_SceneReferences.GetProgramVariable("_sfxSharedUIAudioSource");
            _sfxSharedUIAudioSource.PlayOneShot(_sfxClip, 0.5f);
            _currentMoney -= _Cost;
            _SceneReferences.SetProgramVariable("_currentMoney", _currentMoney);
            //SetUnlockedStatus();
		}
	}
}
