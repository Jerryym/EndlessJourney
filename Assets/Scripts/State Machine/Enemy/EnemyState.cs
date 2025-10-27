using FSM.Enums;

/// <summary>
/// 敌人状态基类
/// </summary>
public abstract class EnemyState : IState
{
	/// <summary>
	/// 状态机
	/// </summary>
	protected EnemyStateMachine stateMachine;
	/// <summary>
	/// 敌人状态枚举值
	/// </summary>
	protected EnemyStateEnum stateEnum = EnemyStateEnum.None;

	public EnemyState(EnemyStateMachine stateMachine)
	{
		this.stateMachine = stateMachine;
	}

	#region 接口
	public abstract void OnEnter();
	public abstract void OnLogicUpdate();
	public abstract void OnPhysicsUpdate();
	public abstract void OnExit();
	#endregion
}
