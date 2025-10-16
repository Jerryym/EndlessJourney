using FSM.Enums;

public class PlayerAttackState : PlayerState
{
	public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
	{
		base.stateEnum = PlayerStateEnum.Attack;
	}

	public override void OnEnter()
	{
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
	}

	public override void OnPhysicsUpdate()
	{
	}
}
