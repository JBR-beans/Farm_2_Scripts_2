
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class QuestsHandler : UdonSharpBehaviour
{
    public UdonBehaviour _SceneReferences;
    public int _currentQuestItemCount;
    public int _currentQuestItemGoal;
    public UdonBehaviour[] _crops;
    private bool _choosing;
    public UdonBehaviour _chosenCrop;
    public TextMeshProUGUI _displayQuestData;
    public string _questData;
    void Start()
    {
        _crops = (UdonBehaviour[])_SceneReferences.GetProgramVariable("_crops");

        

	}

	public void Update()
	{
        if (_choosing == true)
        {
            ChooseCrop();
		}
        if (_chosenCrop != null)
        {
			_questData = string.Format(
			"Total crops found: {0} " +
			"\n " +
			"Is Choosing: {1}" +
			"\n" +
			"Chosen Crop ID: {2}" +
			"\n" +
			"Quest Item Goal: {3}" +
			"\n" +
			"Quest Item Collect: {4}",
			_crops.Length.ToString(),
			_choosing.ToString(),
			_chosenCrop.GetProgramVariable("_cropID").ToString(),
			_currentQuestItemGoal.ToString(),
            _currentQuestItemCount.ToString()
			);
		}
		if (_choosing == false)
        {
            if (_currentQuestItemCount == _currentQuestItemGoal)
            {
                QuestFinished();
			}
        }
		_displayQuestData.text = _questData;
	}

    public void RerollQuest()
    {
        _choosing = true;
    }
	public void ChooseCrop()
    {
        if (_chosenCrop != null)
        {
			_chosenCrop.SetProgramVariable("_isQuest", false);
		}
        int ran = Random.Range(0, _crops.Length);

        if ((bool)_crops[ran].GetProgramVariable("_boughtCrop") == true && (bool)_crops[ran].GetProgramVariable("_isAutoBotActive") == false)
        {
            _chosenCrop = _crops[ran];
            _chosenCrop.SetProgramVariable("_isQuest", true);
            _choosing = false;
        }
    }

    public void QuestFinished()
    {
		_chosenCrop.SetProgramVariable("_isQuest", false);
	}
}
