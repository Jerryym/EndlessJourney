using FSM.Enums;
using System.Collections.Generic;

/// <summary>
/// Player状态机
/// </summary>
public class PlayerStateMachine : StateMachine
{
	/// <summary>
	/// Player控制器
	/// </summary>
	public PlayerController Controller => m_controller;
	private PlayerController m_controller;

	/// <summary>
	/// Player动画控制器
	/// </summary>
	public PlayerAnimationController AnimationController => m_animationController;
	private PlayerAnimationController m_animationController;

	/// <summary>
	/// 状态字典
	/// </summary>
	private Dictionary<PlayerStateEnum, PlayerState> m_stateDic;

	public PlayerStateMachine(PlayerController controller, PlayerAnimationController animationController)
	{
		this.m_controller = controller;
		this.m_animationController = animationController;
		m_stateDic = new Dictionary<PlayerStateEnum, PlayerState>();
	}

	/// <summary>
	/// 添加状态
	/// </summary>
	/// <param name="stateEnum"></param>
	/// <param name="playerState"></param>
	public void AddState(PlayerStateEnum stateEnum, PlayerState playerState)
	{
		if (m_stateDic.ContainsKey(stateEnum) != true)
		{
			m_stateDic.Add(stateEnum, playerState);
		}
	}

	public void SwitchState(PlayerStateEnum state)
	{
		if (m_stateDic.TryGetValue(state, out var playerState) != true)
		{
			return;
		}

		//切换状态
		base.SwitchState(playerState);
	}
}
