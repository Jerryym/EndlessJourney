using FSM.Enums;

/// <summary>
/// 角色状态基类
/// </summary>
public abstract class PlayerState : IState
{
	/// <summary>
	/// 角色状态机
	/// </summary>
	protected PlayerStateMachine stateMachine;
	/// <summary>
	/// 角色状态枚举值
	/// </summary>
	protected PlayerStateEnum stateEnum = PlayerStateEnum.None;

	public PlayerState(PlayerStateMachine stateMachine)
	{
		this.stateMachine = stateMachine;
	}

	#region 接口
	public virtual void OnEnter() { }
	public virtual void OnUpdate() { }
	public virtual void OnPhysicsUpdate() { }
	public virtual void OnExit() { }
	#endregion
}
