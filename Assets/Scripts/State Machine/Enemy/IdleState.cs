using FSM.Enums;

/// <summary>
/// 空闲状态
/// </summary>
public class IdleState : EnemyState
{
	public IdleState(EnemyStateMachine stateMachine) : base(stateMachine)
	{
		base.stateEnum = EnemyStateEnum.Idle;
	}

	public override void OnEnter() { }

	public override void OnExit() { }

	public override void OnLogicUpdate() { }

	public override void OnPhysicsUpdate() { }
}