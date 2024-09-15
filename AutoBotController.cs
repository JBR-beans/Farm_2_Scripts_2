
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

	public void PlayAnim()
    {
		_animator.Play(_animStateName);
	}

	public void PlayIdle()
	{
		//_animator.Play("test");
		ResetParameters();
		_animator.SetBool("isIdle", true);
	}

	public void PlayPlanting()
	{
		//_animator.Play("TestPlant");
		ResetParameters();
		_animator.SetBool("isPlanting", true);
	}

	public void PlayBreakTime()
	{
		//_animator.Play("BreakTime");
		ResetParameters();
		_animator.SetBool("isBreakTime", true);
	}

	public void ResetParameters()
	{
		foreach(AnimatorControllerParameter p in _animator.parameters)
		{
			_animator.SetBool(p.name, false);
		}
	}
}
