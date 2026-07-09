using FSM.Enums;
using System.Collections;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
	/// <summary>
	/// 当前连击段数
	/// </summary>
	private int m_comboIndex = 0;
	/// <summary>
	/// 最大连击段数
	/// </summary>
	private const int m_maxComboCount = 3;
	private bool m_canCombo = false;

	/// <summary>
	/// Combo窗口计时协程
	/// </summary>
	private Coroutine m_comboWindowCoroutine;

	public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
	{
		base.stateEnum = PlayerStateEnum.Attack;
	}

	public override void OnEnter()
	{
		Debug.Log("进入攻击状态!");
		stateMachine.Controller.SetVelocity(Vector2.zero);

		//触发动画
		m_comboIndex++;
		stateMachine.AnimationController.TriggerAttack();
		stateMachine.Controller.IsAttack = false;

		//启动combo窗口期
		StartComboWindow();

		Debug.Log("进入攻击状态!");
	}

	public override void OnExit()
	{
		//退出协程
		if (m_comboWindowCoroutine != null)
		{
			stateMachine.Controller.StopCoroutine(m_comboWindowCoroutine);
			m_comboWindowCoroutine = null;
		}
		
		//重置combo状态
		m_canCombo = false;
		m_comboIndex = 0;
		stateMachine.Controller.IsAttack = false;

		Debug.Log("退出攻击状态!");
	}

	public override void OnLogicUpdate()
	{
		if (stateMachine.Controller.IsAttack && m_canCombo)
		{
			m_canCombo = false;

			//触发动画
			stateMachine.AnimationController.TriggerAttack();

			//启动combo窗口期
			StartComboWindow();
		}
	}

	public override void OnPhysicsUpdate()
	{
	}

	/// <summary>
	/// 启动combo窗口期
	/// </summary>
	private void StartComboWindow()
	{
		if (m_comboWindowCoroutine != null)
		{
			stateMachine.Controller.StopCoroutine(m_comboWindowCoroutine);
		}
		m_comboWindowCoroutine = stateMachine.Controller.StartCoroutine(ComboWindowCoroutine());
	}

	/// <summary>
	/// 协程：combo窗口
	/// </summary>
	/// <returns></returns>
	private IEnumerator ComboWindowCoroutine()
	{
		//启动连击窗口
		m_canCombo = true;

		//关闭窗口
		yield return new WaitForSeconds(0.5f);
		m_canCombo = false;

		//切换状态
		if (!stateMachine.Controller.IsAttack || m_comboIndex > m_maxComboCount)
		{
			stateMachine.SwitchState(PlayerStateEnum.Idle);
		}
	}
}
