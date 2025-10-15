using FSM.Enums;

public class PlayerSlideState : PlayerState
{
	public PlayerSlideState(PlayerStateMachine stateMachine) : base(stateMachine)
	{
		base.stateEnum = PlayerStateEnum.Slide;
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
