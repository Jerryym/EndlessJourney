using FSM.Enums;
using System.Collections.Generic;

/// <summary>
/// 敌人状态机
/// </summary>
public class EnemyStateMachine : StateMachine
{
	/// <summary>
	/// 敌人控制器
	/// </summary>
	public EnemyController Controller => m_controller;
	private EnemyController m_controller;

	/// <summary>
	/// 动画控制器
	/// </summary>
	public EnemyAnimationController AnimationController => m_animationController;
	private EnemyAnimationController m_animationController;

	/// <summary>
	/// 状态字典
	/// </summary>
	private Dictionary<EnemyStateEnum, EnemyState> m_stateDic;

	public EnemyStateMachine(EnemyController controller, EnemyAnimationController animationController)
	{
		this.m_controller = controller;
		this.m_animationController = animationController;
		m_stateDic = new Dictionary<EnemyStateEnum, EnemyState>();
	}

	public void AddState(EnemyStateEnum stateEnum, EnemyState enemyState)
	{
		if (m_stateDic.ContainsKey(stateEnum) != true)
		{
			m_stateDic.Add(stateEnum, enemyState);
		}
	}

	public void SwitchState(EnemyStateEnum stateEnum)
	{
		if (m_stateDic.TryGetValue(stateEnum, out var state) != true)
		{
			return;
		}

		//切换状态
		base.SwitchState(state);
	}
}
