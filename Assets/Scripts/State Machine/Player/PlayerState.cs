using FSM.Enums;

/// <summary>
/// Player状态基类
/// </summary>
public abstract class PlayerState : IState
{
	/// <summary>
	/// Player状态机
	/// </summary>
	protected PlayerStateMachine stateMachine;
	/// <summary>
	/// Player状态枚举值
	/// </summary>
	protected PlayerStateEnum stateEnum = PlayerStateEnum.None;

	public PlayerState(PlayerStateMachine stateMachine)
	{
		this.stateMachine = stateMachine;
	}

	#region 接口
	public virtual void OnEnter() { }
	public virtual void OnLogicUpdate() { }
	public virtual void OnPhysicsUpdate() { }
	public virtual void OnExit() { }
	#endregion
}
