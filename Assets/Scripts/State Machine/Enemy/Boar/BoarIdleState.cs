using FSM.Enums;

public class BoarIdleState : EnemyState
{
	public BoarIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
	{
		base.stateEnum = EnemyStateEnum.Idle;
	}
}
