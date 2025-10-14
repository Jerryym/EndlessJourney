using FSM.Enums;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
	public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
	{
		base.stateEnum = PlayerStateEnum.Idle;
	}

	public override void OnPhysicsUpdate()
	{
		stateMachine.Controller.SetVelocity(Vector2.zero);
	}
}
