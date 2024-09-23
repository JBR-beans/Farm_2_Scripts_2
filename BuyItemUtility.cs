
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class BuyItemUtility : UdonSharpBehaviour
{
    public Button _button;
    public TextMeshProUGUI _textSold;
    public UdonBehaviour _SceneReferences;
    public bool _useInteractEvent;

    
    public int _Cost;
    public GameObject _Item;
    public bool _useArray;
    public GameObject[] _Items;

    [Header("INTERNAL")]
    public AudioSource _sfxSharedUIAudioSource;
    public AudioClip _sfxBuy1;

	public void BuyItem()
    {
        _sfxSharedUIAudioSource = (AudioSource)_SceneReferences.GetProgramVariable("_sfxSharedUIAudioSource");
		_sfxBuy1 = (AudioClip)_SceneReferences.GetProgramVariable("_sfxBuy1");
		if (_useArray == true)
        {
            int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");

            if (_currentMoney >= _Cost)
            {
                foreach (GameObject go in _Items)
                {
					go.SetActive(true);
				}
				_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _Cost);
                _textSold.text = "Sold!";
				_button.interactable = false;
				_sfxSharedUIAudioSource.PlayOneShot(_sfxBuy1);
			}
        }
        else
        {
			int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");

			if (_currentMoney >= _Cost)
			{
				_Item.SetActive(true);
                _SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _Cost);
				_textSold.text = "Sold!";
				_button.interactable = false;
				_sfxSharedUIAudioSource.PlayOneShot(_sfxBuy1);
			}
		}
    }

	public override void Interact()
	{
		if (_useInteractEvent == true)
        {
			_sfxSharedUIAudioSource = (AudioSource)_SceneReferences.GetProgramVariable("_sfxSharedUIAudioSource");
			_sfxBuy1 = (AudioClip)_SceneReferences.GetProgramVariable("_sfxBuy1");
			if (_useArray == true)
			{
				int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");

				if (_currentMoney >= _Cost)
				{
					foreach (GameObject go in _Items)
					{
						go.SetActive(true);
					}
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _Cost);
					//_textSold.text = "Sold!";
					//_button.interactable = false;
					_sfxSharedUIAudioSource.PlayOneShot(_sfxBuy1);
					this.gameObject.SetActive(false);
				}
			}
			else
			{
				int _currentMoney = (int)_SceneReferences.GetProgramVariable("_currentMoney");

				if (_currentMoney >= _Cost)
				{
					_Item.SetActive(true);
					_SceneReferences.SetProgramVariable("_currentMoney", _currentMoney - _Cost);
					//_textSold.text = "Sold!";
					//_button.interactable = false;
					_sfxSharedUIAudioSource.PlayOneShot(_sfxBuy1);
					this.gameObject.SetActive(false);
				}
			}
		}
	}
}
