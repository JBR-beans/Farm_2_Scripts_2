
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class AutoBotController : UdonSharpBehaviour
{
    
    public string _animStateName;

    public Animator _animator;

	public void Start()
	{
        _animator = GetComponent<Animator>();
	}
	public void ResetParameters()
	{
		foreach (AnimatorControllerParameter p in _animator.parameters)
		{
			_animator.SetBool(p.name, false);
		}
	}
	public void PlayIdle()
	{
		ResetParameters();
		_animator.SetBool("isIdle", true);
	}

	public void PlayPlanting()
	{
		ResetParameters();
		_animator.SetBool("isPlanting", true);
	}

	public void PlayWatering()
	{
		ResetParameters();
		_animator.SetBool("isWatering", true);
	}
	public void PlayHarvesting()
	{
		ResetParameters();
		_animator.SetBool("isHarvesting", true);
	}



	public void PlayBreakTime()
	{
		ResetParameters();
		_animator.SetBool("isBreakTime", true);
	}

	
}
