using FSM.Enums;
using System.Collections.Generic;

/// <summary>
/// 角色状态机
/// </summary>
public class PlayerStateMachine : StateMachine
{
	public PlayerController Controller => m_controller;

	/// <summary>
	/// 角色控制器
	/// </summary>
	private PlayerController m_controller;
	/// <summary>
	/// 状态字典
	/// </summary>
	private Dictionary<PlayerStateEnum, PlayerState> m_stateDic;

	public PlayerStateMachine(PlayerController controller)
	{
		this.m_controller = controller;
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
